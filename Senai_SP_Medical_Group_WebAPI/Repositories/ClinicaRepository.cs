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
    public class ClinicaRepository : IClinicaRepository
    {
        SP_MedicalContext ctx = new SP_MedicalContext();

        public void Atualizar(int id, Clinica attClinica)
        {
            Clinica clinicaBuscada = BuscarClinica(id);
            if (attClinica.Endereço != null || attClinica.Cnpj != null || attClinica.NomeClinica!= null || attClinica.RazaoSocial != null)
            {
                clinicaBuscada.Endereço = attClinica.Endereço;
                clinicaBuscada.Cnpj = attClinica.Cnpj;
                clinicaBuscada.NomeClinica = attClinica.NomeClinica;
                clinicaBuscada.RazaoSocial = attClinica.RazaoSocial;

                ctx.Clinicas.Update(clinicaBuscada);

                ctx.SaveChanges();
            }
        }

        public Clinica BuscarClinica(int id)
        {
            return ctx.Clinicas.FirstOrDefault(c => c.IdClinica == id);
        }

        public void CadastrarClinica(Clinica novaClinica)
        {
            ctx.Clinicas.Add(novaClinica);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.Clinicas.Remove(BuscarClinica(id));

            ctx.SaveChanges();
        }

        public List<Clinica> ListarTodas()
        {
            return ctx.Clinicas
                    .AsNoTracking()
                    .Include(c => c.Medicos)
                    .ToList();
        }
    }
}