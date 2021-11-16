using Senai_SP_Medical_Group_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Interfaces
{
    interface IPacienteRepository
    {
        List<Paciente> ListarTodos();

        void Cadastrar(Paciente novoPaciente);

        void Deletar(int id);

        void Atualizar(int id, Paciente attPaciente);

        Paciente BuscarPorId(int id);
    }
}
