using Senai_SP_Medical_Group_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Interfaces
{
    interface IUsuarioRepository
    {
        Usuario BucarPorID(short idUsuario);
        List<Usuario> ListarTodos();
        void Cadastar(Usuario NovoUsuario);
        void Atualizar(short idUsuario, Usuario UsuarioAtualizado);
        void Deletar(short idUsuario);
    }
}
