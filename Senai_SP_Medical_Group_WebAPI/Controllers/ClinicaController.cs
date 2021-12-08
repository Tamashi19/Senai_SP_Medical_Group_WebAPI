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
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicasController : ControllerBase
    {
        private IClinicaRepository ClinicaRepository { get; set; }

        public ClinicasController()
        {
            ClinicaRepository = new ClinicaRepository();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Clinica novaClinica)
        {
            try
            {

                if (novaClinica == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Valores inválidos"
                    });
                }
                ClinicaRepository.CadastrarClinica(novaClinica);

                return StatusCode(201, new
                {
                    Mensagem = "Instituição cadastrada",
                    novaClinica
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<Clinica> lista = ClinicaRepository.ListarTodas();

                if (lista == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há instituição cadastrada"
                    });
                }

                return Ok(new
                {
                    Mensagem = $"Foram encontradas {lista.Count()} clínicas",
                    lista
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int id, Clinica attClinica)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Id inválido"
                    });
                }

                if (ClinicaRepository.BuscarClinica(id) == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma clínica com o id informado"
                    });
                }
                if (attClinica.Cnpj == null || attClinica.Endereço == null || attClinica.NomeClinica == null || attClinica.RazaoSocial == null || attClinica.Cnpj.Length != 14)
                {
                    return BadRequest(new
                    {
                        Mensagem = "As informações inseridas são inválidas"
                    });
                }

                ClinicaRepository.Atualizar(id, attClinica);
                return Ok(new
                {
                    Mensagem = "A clínica foi atualizada com sucesso",
                    attClinica
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
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Id inválido"
                    });
                }

                if (ClinicaRepository.BuscarClinica(id) == null)
                {
                    return StatusCode(404, new
                    {
                        Mensagem = "Não há nenhuma clínica com o id informado!"
                    });
                }

                ClinicaRepository.Deletar(id);
                return Ok(new
                {
                    Mensagem = "Cínica foi excluída",
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }
    }
}