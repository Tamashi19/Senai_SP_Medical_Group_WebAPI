using Senai_SP_Medical_Group_WebAPI.Contexts;
using Senai_SP_Medical_Group_WebAPI.Domains;
using Senai_SP_Medical_Group_WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Repositories
{
    public class MedicoRepositorycs : IMedicoRepository
    {
        SP_MedicalContext ctx = new SP_MedicalContext();

        public List<Medico> ListarTodos()
        {
            return ctx.Medicos.ToList();
        }

        public void Cadastrar(Medico novoMedico)
        {
            ctx.Medicos.Add(novoMedico);

            ctx.SaveChanges();
        }
    }
}