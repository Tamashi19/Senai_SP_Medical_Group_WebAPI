using Senai_SP_Medical_Group_WebAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Interfaces
{
    interface IPacienteRepository
    {
        Paciente BucarPorID(short idPaciente);
        List<Paciente> ListarTodos();
        void Cadastar(Paciente NovoPaciente);
        void Atualizar(short idPaciente, Paciente PacienteAtualizado);
        void Deletar(short idPaciente);
    }
}
