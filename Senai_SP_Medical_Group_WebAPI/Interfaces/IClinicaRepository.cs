using Senai_SP_Medical_Group_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Interfaces
{
    interface IClinicaRepository
    {
        void CadastrarClinica(Clinica novaClinica);


        void Atualizar(int id, Clinica attClinica);


        List<Clinica> ListarTodas();


        void Deletar(int id);


        Clinica BuscarClinica(int id);

    }
}
