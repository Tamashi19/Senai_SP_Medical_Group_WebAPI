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
    public class MedicosController : ControllerBase
    {
        private IMedicoRepository MedicoRepository { get; set; }

        public MedicosController()
        {
            MedicoRepository = new MedicoRepository();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            List<Medico> lista = MedicoRepository.ListarTodos();

            return Ok(lista);
        }

        [HttpPost]
        public IActionResult Cadastrar(Medico novoMedico)
        {
            try
            {
                if (novoMedico.Crm == null || novoMedico.IdEspecialidade <= 0 || novoMedico.IdClinica <= 0 || novoMedico.IdUsuario <= 0)
                {
                    return BadRequest(new
                    {
                        Mensagem = "Dados inválidos!"
                    });
                }

                MedicoRepository.Cadastrar(novoMedico);

                return Ok(new
                {
                    Mensagem = "Médico cadastrado",
                    novoMedico
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}