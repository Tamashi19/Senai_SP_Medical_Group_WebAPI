using Senai_SP_Medical_Group_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Interfaces
{
    interface IMedicoRepository
    {
        Medico BucarPorID(short idMedico);
        List<Medico> ListarTodos();
        void Cadastar(Medico NovoMedico);
        void Atualizar(short idMedico, Medico MedicoAtualizado);
        void Deletar(short idMedico);
    }
}
