using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai_SP_Medical_Group_WebAPI.Domains;
using Senai_SP_Medical_Group_WebAPI.Interfaces;
using Senai_SP_Medical_Group_WebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private IConsultaRepository ConsultaRepository { get; set; }

        public ConsultasController()
        {
            ConsultaRepository = new ConsultaRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {
                List<Consultum> listaConsultas = ConsultaRepository.ListarTodas();
                if (listaConsultas.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma consulta cadastrada no sistema!"
                    });
                }
                return Ok(listaConsultas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Consultum novaConsulta)
        {
            try
            {

                if (novaConsulta.IdMedico == null || novaConsulta.IdPaciente == null || novaConsulta.DataConsulta < DateTime.Now)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Os dados informados são inválidos ou estão vazios!"
                    });
                }
                ConsultaRepository.CadastrarConsulta(novaConsulta);

                return StatusCode(201, new
                {
                    Mensagem = "A consulta foi cadastrada!",
                    novaConsulta
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpPatch("Cancelar/{id}")]
        public IActionResult Deletar(int id)
                  


        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe um ID válido!"
                    });
                }

                if (ConsultaRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há nenhuma consulta com o ID informado!"
                    });
                }
                ConsultaRepository.CancelarConsulta(id);

                return StatusCode(204, new
                {
                    Mensagem = "A consulta foi cancelada!"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "2, 3")]
        [HttpGet("Lista/Minhas")]
        public IActionResult ListarMinhas()
        {
            try
            {

                int id = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
                int idTipo = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value);
                List<Consultum> listaConsulta = ConsultaRepository.ListarMinhasConsultas(id, idTipo);

                if (listaConsulta.Count == 0)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não há consulta com o usuário"
                    });
                }

                if (idTipo == 2)
                {
                    return Ok(new
                    {
                        Mensagem = $"O paciente buscado tem {ConsultaRepository.ListarMinhasConsultas(id, idTipo).Count} consultas",
                        listaConsulta
                    });
                }
                if (idTipo == 3)
                {
                    return Ok(new
                    {
                        Mensagem = $"O médico buscado tem {ConsultaRepository.ListarMinhasConsultas(id, idTipo).Count} consultas",
                        listaConsulta
                    });
                }
                return null;

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "2")]
        [HttpPatch("AlterarDescricao/{id}")]
        public IActionResult AlterarDescricao(Consultum consultaAtt, int id)
        {
            try
            {
                Consultum consultaBuscada = ConsultaRepository.BuscarPorId(id);
                int idMedico = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
                if (consultaAtt.Descricao == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "informe descrição"
                    });
                }




                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "ID inválido"
                    });
                }

                if (ConsultaRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não há consulta com o ID informado"
                    });
                }

                if (consultaBuscada.IdMedico != id)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Somente o médico pode fazer alterações na descrição"
                    });
                }
                ConsultaRepository.AlterarDescricao(consultaAtt.Descricao, id);
                return StatusCode(200, new
                {
                    Mensagem = "Descrição da consulta foi alterada ",
                    consultaBuscada
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "1")]
        [HttpDelete("Remover/{id}")]
        public IActionResult RemoverConsultaSistema(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "ID inválido!"
                    });
                }

                if (ConsultaRepository.BuscarPorId(id) == null)
                {
                    return NotFound(new
                    {
                        Mensagem = "Não há consulta com o ID informado"
                    });
                }

                ConsultaRepository.RemoverConsultaSistema(id);

                return StatusCode(200, new
                {
                    Mensagem = "Consulta removida do sistema"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}