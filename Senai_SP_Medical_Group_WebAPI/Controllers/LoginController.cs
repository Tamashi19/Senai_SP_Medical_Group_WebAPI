using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai_SP_Medical_Group_WebAPI.Domains;
using Senai_SP_Medical_Group_WebAPI.Interfaces;
using Senai_SP_Medical_Group_WebAPI.Repositories;
using Senai_SP_Medical_Group_WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Senai_SP_Medical_Group_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository UsuarioRepository { get; set; }

        public LoginController()
        {
            UsuarioRepository = new UsuarioRepository();
        }

        //[Authorize(Roles = "1")]
        [HttpPost]
       public IActionResult Login(LoginViewModel Login)
        {
            try
           {
                Usuario usuarioBuscado = UsuarioRepository.Login(Login.Email, Login.Senha);
               if (usuarioBuscado != null)
               {
                  var Claims = new[]
                    {
                  new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                  new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString())
              };

                  var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SP_MEDICAL_GRUOP_TAMASHI"));

                   var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

                    var meuToken = new JwtSecurityToken(
                           issuer: "Senai_SP_Medical_Group_WebAPI",
                           audience: "Senai_SP_Medical_Group_WebAPI",
                            claims: Claims,
                           expires: DateTime.Now.AddMinutes(40),
                            signingCredentials: Creds
                        );

                  return Ok(new
                    {
                       token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                  });
             }

                return NotFound("Email ou Senha Inválido");
           }
           catch (Exception ex)
           {

              return BadRequest(ex.Message);
            }

        }
    }
}