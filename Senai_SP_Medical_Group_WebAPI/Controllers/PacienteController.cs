using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai_SP_Medical_Group_WebAPI.Domains;
using Senai_SP_Medical_Group_WebAPI.Interfaces;
using Senai_SP_Medical_Group_WebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private IPacienteRepository PacienteRepository { get; set; }

        public PacientesController()
        {
            PacienteRepository = new PacienteRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<Paciente> lista = PacienteRepository.ListarTodos();

                if (lista == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Nenhum paciente cadastrado no sistema"
                    });
                }

                return Ok(new
                {
                    Mensagem = $"Pacientes {lista.Count}  encontrados ",
                    lista
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Paciente novoPaciente)
        {
            try
            {
                if (novoPaciente.Cpf == null || novoPaciente.DataNascimento > DateTime.Now || novoPaciente.Rg == null || novoPaciente.Telefone == null || novoPaciente.Endereço == null || novoPaciente.IdUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "dados inválidos"
                    });
                }

                PacienteRepository.Cadastrar(novoPaciente);

                return Ok(new
                {
                    Mensagem = "Paciente cadastrado com sucesso",
                    novoPaciente
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int id, Paciente attPaciente)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "ID inválido"
                    });
                }

                if (PacienteRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = " nenhum paciente com esse ID "
                    });
                }

                if (attPaciente.Cpf == null || attPaciente.DataNascimento > DateTime.Now || attPaciente.Rg == null || attPaciente.Telefone == null || attPaciente.Endereço == null || attPaciente.IdUsuario == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Dados inválidos"
                    });
                }

                PacienteRepository.Atualizar(id, attPaciente);
                return Ok(new
                {
                    Mensagem = "Paciente foi atualizado",
                    attPaciente
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id:int}")]
        public IActionResult Deletar(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = " ID inválido"
                });
            }

            if (PacienteRepository.BuscarPorId(id) == null)
            {
                return NotFound(new
                {
                    Mensagem = "Não há paciente com esse ID"
                });
            }

            PacienteRepository.Deletar(id);
            return Ok(new
            {
                Mensagem = "Paciente foi deletado",

            });
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id:int}")]
        public IActionResult BuscarPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Mensagem = "ID inválido!"
                });
            }

            if (PacienteRepository.BuscarPorId(id) == null)
            {
                return NotFound(new
                {
                    Mensagem = "nenhum paciente com esse ID"
                });
            }

            Paciente pacienteEncontrado = PacienteRepository.BuscarPorId(id);
            return Ok(new
            {
                Mensagem = "Paciente encontrado",
                pacienteEncontrado
            });
        }
    }
}