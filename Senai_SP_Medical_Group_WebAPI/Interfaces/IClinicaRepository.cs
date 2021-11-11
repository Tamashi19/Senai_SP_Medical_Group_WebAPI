using Senai_SP_Medical_Group_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Interfaces
{
    interface IClinicaRepository
    {
        Clinica BucarPorID(short idClinica);
        List<Clinica> ListarTodos();
        void Cadastar(Clinica NovaClinica);
        void Atualizar(short idClinica, Clinica ClinicaAtualizada);
        void Deletar(short idClinica);

    }
}
