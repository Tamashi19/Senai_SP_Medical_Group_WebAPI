using Microsoft.AspNetCore.Http;
using Senai_SP_Medical_Group_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Interfaces
{
    interface IUsuarioRepository
    {
        List<Usuario> ListarUsuarios();

        void Cadastrar(Usuario novoUser);

        Usuario Login(string email, string senha);

        void Deletar(int id);

        void SalvarPerfilBD(IFormFile foto, short id);

        string ConsultarPerfilBD(short id);

        Usuario BuscarPorId(int id);

        void Atualizar(int id, Usuario userAtt);
    }
}
