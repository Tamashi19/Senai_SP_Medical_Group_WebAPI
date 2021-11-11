using Senai_SP_Medical_Group_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Interfaces
{
    interface IConsultaRepository
    {
        
        void Cadastrar(Consultum novaConsulta);

       
        void AtualizarUrl(short id, Consultum consultaAtualizada);

      
        void Deletar(short id);

       
        List<Consultum> ListarMinhas(short id, short idTipo);

       
        void mudarDescricao(short id, string descricao);

       
        void mudarSituacao(short idConsulta, short idSituacao);

        
        Consultum BuscarPorId(short id);
    }
}
