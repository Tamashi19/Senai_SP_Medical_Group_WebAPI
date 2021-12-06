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
    [Produces("applicaion/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        public string JwtRegisteredClaimTypes { get; private set; }

        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

 
        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Listar()
        {
            if (_usuarioRepository.ListarUsuarios() == null)
            {
                return NotFound(new
                {
                    Mensagem = "Usuario não cadastrado"
                });
            }

            return Ok(_usuarioRepository.ListarUsuarios());
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Cadastrar(Usuario novoUser)
        {
            try
            {
                if (novoUser == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "É necessário informar mais dados"
                    });
                }
                _usuarioRepository.Cadastrar(novoUser);
                return StatusCode(201, new
                {
                    Mensagem = "Usuario cadastrado",
                    novoUser
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpPut("att/{id}")]
        public IActionResult Atualizar(int id, Usuario userAtt)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "ID invalido"
                    });
                }

                if (_usuarioRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há usuário com este ID"
                    });
                }
                if (userAtt == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "É necessário informar mais dados"
                    });
                }
                _usuarioRepository.Atualizar(id, userAtt);
                return StatusCode(200, new
                {
                    Mensagem = "Usuario atualizado",
                    userAtt
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpDelete("delete/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe ID válido"
                    });
                }

                if (_usuarioRepository.BuscarPorId(id) == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há usuário com este ID"
                    });
                }

                _usuarioRepository.Deletar(id);

                return StatusCode(200, new
                {
                    Mensagem = "Usuario deletado"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [Authorize(Roles = "1")]
        [HttpPost("imagem/bd/{idUsuario}")]
        public IActionResult postBD(IFormFile arquivo, short idUsuario)
        {
            try
            {
                if (arquivo == null)
                {
                    return BadRequest(new { mensagem = "É necessario uma foto .png" });
                }
                if (arquivo.Length > 5000)
                {
                    return BadRequest(new { mensagem = "O tamanho máximo da imagem é 5mb" });
                }

                string extensao = arquivo.FileName.Split('.').Last();

                if (extensao != "png" || extensao != "jpg")
                {
                    return BadRequest(new { mensagem = "Apenas .png ou .jpg " });
                }



                _usuarioRepository.SalvarPerfilBD(arquivo, idUsuario);

                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }


        }
        [Authorize(Roles = "1")]
        [HttpGet("imagem/bd/{idUsuario}")]
        public IActionResult getBd(short idUsuario)
        {
            try
            {
                string base64 = _usuarioRepository.ConsultarPerfilBD(idUsuario);
                return Ok(base64);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario userBuscado = _usuarioRepository.BuscarPorId(id);

                if (id <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Informe um ID válido!"
                    });
                }

                if (userBuscado == null)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Não há  usuário com esse ID "
                    });
                }

                return StatusCode(201, new
                {
                    Mensagem = "Usuário encontrado",
                    userBuscado
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}