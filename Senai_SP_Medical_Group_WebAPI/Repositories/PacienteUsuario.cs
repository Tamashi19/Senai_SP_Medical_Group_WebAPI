using Senai_SP_Medical_Group_WebAPI.Contexts;
using Senai_SP_Medical_Group_WebAPI.Domains;
using Senai_SP_Medical_Group_WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Repositories
{
    public class PacienteUsuario : IPacienteRepository
    {
        public void Atualizar(int id, Paciente attPaciente)
        {
            SP_MedicalContext ctx = new SP_MedicalContext();

            Paciente pacienteBuscado = BuscarPorId(id);

            if (attPaciente.Cpf != null || attPaciente.Rg != null || attPaciente.Telefone != null || attPaciente.Endereço != null || attPaciente.DataNascimento < DateTime.Now)
            {
                pacienteBuscado.Rg = attPaciente.Rg;
                pacienteBuscado.IdUsuario = attPaciente.IdUsuario;
                pacienteBuscado.Cpf = attPaciente.Cpf;
                pacienteBuscado.Telefone = attPaciente.Telefone;
                pacienteBuscado.Endereço = attPaciente.Endereço;
                pacienteBuscado.DataNascimento = attPaciente.DataNascimento;

                ctx.Pacientes.Update(pacienteBuscado);

                ctx.SaveChanges();
            }
        }

        public Paciente BuscarPorId(int id)
        {
            return ctx.Pacientes.FirstOrDefault(p => p.IdPaciente == id);
        }

        public void Cadastrar(Paciente novoPaciente)
        {
            ctx.Pacientes.Add(novoPaciente);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.Pacientes.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        public List<Paciente> ListarTodos()
        {
            return ctx.Pacientes
                        .AsNoTracking()
                        .Include(p => p.IdUsuarioNavigation)
                        .ToList();

        }
    }
}