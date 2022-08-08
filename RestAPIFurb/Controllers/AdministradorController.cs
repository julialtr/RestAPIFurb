using Microsoft.AspNetCore.Mvc;
using RestAPIFurb.DTOs;
using RestAPIFurb.Models;
using RestAPIFurb.Repositorys;
using RestAPIFurb.Repositorys.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Helpers;

namespace RestAPIFurb.Controllers
{
    [Route("RestAPIFurb/")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAdministradorRepository _administradorRepository;

        public AdministradorController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _administradorRepository = new AdministradorRepository(context);
        }

        /// <summary> 
        /// Faz o cadastro de um novo administrador a partir do login e da senha
        /// </summary>
        [HttpPost]
        [Route("administradores/cadastro")]
        public ActionResult<string> Cadastrar(AdministradorDto administradorDto)
        {
            try
            {
                var administradorDB = _administradorRepository.GetByLogin(administradorDto.Login);

                if (administradorDB != null)
                    return Conflict("Administrador já cadastrado.");

                administradorDB = new Administrador()
                {
                    Login = administradorDto.Login,
                    Senha = Crypto.HashPassword(administradorDto.Senha)
                };

                _administradorRepository.Insert(administradorDB);
                _administradorRepository.Save();

                return Ok("Administrador cadastrado com sucesso.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao cadastrar o administrador.");
            }
        }

        /// <summary> 
        /// Faz o login e a autenticação do administrador a partir do login e da senha
        /// </summary>
        [HttpPost]
        [Route("administradores/login")]
        public ActionResult<string> Logar(AdministradorDto administradorDto)
        {
            try
            {
                var administradorDB = _administradorRepository.GetByLogin(administradorDto.Login);

                if (administradorDB == null)
                    return NotFound("Administrador não encontrado.");

                if (!Crypto.VerifyHashedPassword(administradorDB.Senha, administradorDto.Senha))
                    return BadRequest("Senha incorreta.");

                return Ok(GetToken(administradorDB));
            }
            catch (Exception)
            {
                return BadRequest("Erro ao realizar o login do administrador.");
            }
        }

        private string GetToken(Administrador administrador)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, administrador.Login)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credencial);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
