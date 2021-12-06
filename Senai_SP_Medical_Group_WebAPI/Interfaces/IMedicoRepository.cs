using Senai_SP_Medical_Group_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Interfaces
{
    interface IMedicoRepository
    {
        List<Medico> ListarTodos();

        void Cadastrar(Medico novoMedico);
    }
}
