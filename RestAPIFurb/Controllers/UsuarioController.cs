using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPIFurb.DTOs;
using RestAPIFurb.Models;
using RestAPIFurb.Repositorys;
using RestAPIFurb.Repositorys.Interfaces;

namespace RestAPIFurb.Controllers
{
    [Route("RestAPIFurb/")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(DataContext context)
        {
            _usuarioRepository = new UsuarioRepository(context);
        }

        /// <summary> 
        /// Retorna todos os usuários cadastrados
        /// </summary>
        [HttpGet]
        [Route("usuarios")]
        public ActionResult<List<Usuario>> GetUsuarios()
        {
            try
            {
                return Ok(_usuarioRepository.Get());
            }
            catch(Exception)
            {
                return BadRequest("Erro ao retornar a lista de usuários.");
            }
        }

        /// <summary> 
        /// Retorna um usuário a partir do id
        /// </summary>
        [HttpGet]
        [Route("usuarios/{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            try
            {
                var usuarioBD = _usuarioRepository.GetByID(id);

                if (usuarioBD == null)
                    return NotFound("Usuário não encontrado.");

                return Ok(usuarioBD);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao retornar o usuário.");
            }
        }

        /// <summary> 
        /// Faz o cadastro de um novo usuário a partir do nome e do telefone
        /// </summary>
        [HttpPost]
        [Route("usuarios")]
        public ActionResult<Usuario> PostUsuario(UsuarioDto usuarioDto)
        {
            try
            {
                if (usuarioDto.Nome == String.Empty)
                    return BadRequest("Nome deve ser informado.");

                if (usuarioDto.Telefone == String.Empty)
                    return BadRequest("Telefone deve ser informado.");

                Usuario usuarioDB = new Usuario()
                {
                    Nome = usuarioDto.Nome,
                    Telefone = usuarioDto.Telefone
                };

                _usuarioRepository.Insert(usuarioDB);
                _usuarioRepository.Save();

                return Ok(usuarioDB);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao cadastrar o usuário.");
            }
        }

        /// <summary> 
        /// Faz a atualização dos dados de um usuário a partir do ID. Serão atualizados apenas os dados informados
        /// </summary>
        [HttpPut]
        [Route("usuarios/{id}")]
        public ActionResult<string> PutUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                var usuarioDB = _usuarioRepository.GetByID(id);

                if (usuarioDB == null)
                    return NotFound("Usuário não encontrado.");

                if (usuarioDto.Nome != String.Empty)
                    usuarioDB.Nome = usuarioDto.Nome;

                if (usuarioDto.Telefone != String.Empty)
                    usuarioDB.Telefone = usuarioDto.Telefone;

                _usuarioRepository.Save();

                return Ok("Usuário alterado.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao alterar o usuário.");
            }
        }

        /// <summary> 
        /// Deleta o usuário a partir do ID
        /// </summary>
        [HttpDelete]
        [Route("usuarios/{id}")]
        public ActionResult<string> DeleteUsuario(int id)
        {
            try
            {
                var usuarioDB = _usuarioRepository.GetByID(id);

                if (usuarioDB == null)
                    return NotFound("Usuário não encontrado.");

                _usuarioRepository.Delete(usuarioDB);
                _usuarioRepository.Save();

                return Ok("Usuário removido.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao remover o usuário.");
            }
        }
    }
}
