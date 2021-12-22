using Microsoft.EntityFrameworkCore;
using Senai_SP_Medical_Group_WebAPI.Contexts;
using Senai_SP_Medical_Group_WebAPI.Domains;
using Senai_SP_Medical_Group_WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        SP_MedicalContext ctx = new SP_MedicalContext();
        public void AlterarDescricao(string descricao, int id)
        {
            Consultum consultaBuscado = BuscarPorId(id);

            if (descricao != null)
            {
                consultaBuscado.Descricao = descricao;

                ctx.Consulta.Update(consultaBuscado);

                ctx.SaveChanges();
            };

        }

        public void CadastrarConsulta(Consultum novaConsulta)
        {

            novaConsulta.Descricao = "";
            novaConsulta.IdSituacao = 2;

            ctx.Consulta.Add(novaConsulta);

            ctx.SaveChanges();

        }

        public Consultum BuscarPorId(int id)
        {
            return ctx.Consulta.FirstOrDefault(u => u.IdConsulta == id);
        }

        public void CancelarConsulta(int Id)
        {
            Consultum consultaBuscada = BuscarPorId(Id);

            consultaBuscada.IdSituacao = 3;
            consultaBuscada.Descricao = "Consulta Cancelada!";

            ctx.Consulta.Update(consultaBuscada);
            ctx.SaveChanges();

        }

        public List<Consultum> ListarMinhasConsultas(int id, int idTipo)
        {
            if (idTipo == 2)
            {
                Medico medico = ctx.Medicos.FirstOrDefault(u => u.IdUsuario == id);

                int idMedico = medico.IdMedico;

                return ctx.Consulta
                                .Where(c => c.IdMedico == idMedico)
                                .AsNoTracking()
                                .Select(p => new Consultum()
                                {
                                    DataConsulta = p.DataConsulta,
                                    IdConsulta = p.IdConsulta,
                                    IdMedicoNavigation = new Medico()
                                    {
                                        Crm = p.IdMedicoNavigation.Crm,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Email = p.IdMedicoNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdPacienteNavigation = new Paciente()
                                    {
                                        Cpf = p.IdPacienteNavigation.Cpf,
                                        Telefone = p.IdPacienteNavigation.Telefone,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Email = p.IdPacienteNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdSituacaoNavigation = new Situacao()
                                    {
                                        Situacao1 = p.IdSituacaoNavigation.Situacao1
                                    }


                                })
                                .ToList();
            }
            else if (idTipo == 3)
            {
                Paciente paciente = ctx.Pacientes.FirstOrDefault(u => u.IdUsuario == id);

                int idPaciente = paciente.IdPaciente;
                return ctx.Consulta
                                .Where(c => c.IdConsulta == idPaciente)
                                .AsNoTracking()
                                .Select(p => new Consultum()
                                {
                                    DataConsulta = p.DataConsulta,
                                    IdConsulta = p.IdConsulta,
                                    IdMedicoNavigation = new Medico()
                                    {
                                        Crm = p.IdMedicoNavigation.Crm,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Email = p.IdMedicoNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdPacienteNavigation = new Paciente()
                                    {
                                        Cpf = p.IdPacienteNavigation.Cpf,
                                        Telefone = p.IdPacienteNavigation.Telefone,
                                        IdUsuarioNavigation = new Usuario()
                                        {
                                            Email = p.IdPacienteNavigation.IdUsuarioNavigation.Email
                                        }
                                    },
                                    IdSituacaoNavigation = new Situacao()
                                    {
                                        Situacao1 = p.IdSituacaoNavigation.Situacao1
                                    }


                                })
                                .ToList();
            }

            return null;

        }



        public List<Consultum> ListarTodas()
        {
            return ctx.Consulta
                .Select(p => new Consultum()
                {
                    DataConsulta = p.DataConsulta,
                    IdConsulta = p.IdConsulta,
                    IdMedicoNavigation = new Medico()
                    {
                        Crm = p.IdMedicoNavigation.Crm,
                        IdUsuarioNavigation = new Usuario()
                        {
                            Email = p.IdMedicoNavigation.IdUsuarioNavigation.Email
                        }
                    },
                    IdPacienteNavigation = new Paciente()
                    {
                        Cpf = p.IdPacienteNavigation.Cpf,
                        Telefone = p.IdPacienteNavigation.Telefone,
                        IdUsuarioNavigation = new Usuario()
                        {
                            Email = p.IdPacienteNavigation.IdUsuarioNavigation.Email
                        }
                    },
                    IdSituacaoNavigation = new Situacao()
                    {
                        Situacao1 = p.IdSituacaoNavigation.Situacao1
                    }


                })
                .ToList();
        }

        public void RemoverConsultaSistema(int id)
        {
            ctx.Consulta.Remove(BuscarPorId(id));
            ctx.SaveChanges();
        }

    }
}