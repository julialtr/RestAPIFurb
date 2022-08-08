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
    public class ComandaController : ControllerBase
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProdutoRepository _produtoRepository;

        public ComandaController(DataContext context)
        {
            _comandaRepository = new ComandaRepository(context);
            _usuarioRepository = new UsuarioRepository(context);
            _produtoRepository = new ProdutoRepository(context);
        }

        /// <summary> 
        /// Retorna todas as comandas cadastradas
        /// </summary>
        [HttpGet]
        [Route("comandas")]
        public ActionResult<object> GetComandas()
        {
            try
            {
                return Ok(_comandaRepository.Get());
            }
            catch (Exception)
            {
                return BadRequest("Erro ao retornar a lista de comandas.");
            }
        }

        /// <summary> 
        /// Retorna uma comanda a partir do id
        /// </summary>
        [HttpGet]
        [Route("comandas/{id}")]
        public ActionResult<object> GetComanda(int id)
        {
            try
            {
                var retornoJson = _comandaRepository.GetJsonByID(id);

                if (retornoJson == null)
                    return NotFound("Comanda não encontrada");

                return Ok(retornoJson);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao retornar a comanda.");
            }
        }

        /// <summary> 
        /// Faz o cadastro de uma nova comanda a partir de um id de usuário e uma lista de ids de produtos
        /// </summary>
        [HttpPost]
        [Route("comandas")]
        public ActionResult<object> PostComanda(ComandaDto comandaDto)
        {
            try
            {
                var usuarioBD = _usuarioRepository.GetByID(comandaDto.IdUsuario);

                if (usuarioBD == null)
                    return NotFound("Usuário não encontrado.");

                Comanda comandaDB = new Comanda();

                comandaDB.UsuarioId = comandaDto.IdUsuario;
                comandaDB.Produtos = new List<ComandaProduto>();

                foreach (int idProduto in comandaDto.Produtos)
                {
                    var produtoBD = _produtoRepository.GetByID(idProduto);

                    if (produtoBD == null)
                        return NotFound("Produto não encontrado.");

                    ComandaProduto comandaProduto = new ComandaProduto()
                    {
                        IdProduto = idProduto
                    };

                    comandaDB.Produtos.Add(comandaProduto);
                }

                _comandaRepository.Insert(comandaDB);
                _comandaRepository.Save();

                return Ok(_comandaRepository.GetJsonByID(comandaDB.Id));
            }
            catch (Exception)
            {
                return BadRequest("Erro ao cadastrar a comanda.");
            }
        }

        /// <summary> 
        /// Faz a atualização dos dados de uma comanda a partir do ID. Serão atualizados apenas os dados informados
        /// </summary>
        [HttpPut]
        [Route("comandas/{id}")]
        public ActionResult<string> PutComanda(int id, [FromBody] ComandaDto comandaDto)
        {
            try
            {
                var comandaDB = _comandaRepository.GetByID(id);

                if (comandaDB == null)
                    return NotFound("Comanda não encontrada.");

                if (comandaDto.IdUsuario != 0)
                {
                    var usuarioBD = _usuarioRepository.GetByID(comandaDto.IdUsuario);

                    if (usuarioBD == null)
                        return NotFound("Usuário não encontrado.");

                    comandaDB.UsuarioId = comandaDto.IdUsuario;
                }

                bool produtoVazio = comandaDto.Produtos.Count == 1 && comandaDto.Produtos[0] == 0;

                if (comandaDto.Produtos.Count != 0 && !produtoVazio)
                {
                    comandaDB.Produtos = new List<ComandaProduto>();

                    _comandaRepository.DeleteRange(id);

                    foreach (int idProduto in comandaDto.Produtos)
                    {
                        if (idProduto == 0)
                            return NotFound("Produto não encontrado.");

                        var produtoBD = _produtoRepository.GetByID(idProduto);

                        if (produtoBD == null)
                            return NotFound("Produto não encontrado.");

                        ComandaProduto comandaProduto = new ComandaProduto()
                        {
                            IdProduto = idProduto
                        };

                        comandaDB.Produtos.Add(comandaProduto);
                    }
                }

                _comandaRepository.Save();

                return Ok("Comanda alterada.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao alterar a comanda.");
            }
        }

        /// <summary> 
        /// Deleta a comanda a partir do ID
        /// </summary>
        [HttpDelete]
        [Route("comandas/{id}")]
        public ActionResult<string> DeleteComanda(int id)
        {
            try
            {
                var comandaDB = _comandaRepository.GetByID(id);

                if (comandaDB == null)
                    return NotFound("Comanda não encontrada.");

                _comandaRepository.Delete(comandaDB);
                _comandaRepository.Save();

                return Ok("comanda removida");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao remover a comanda.");
            }
        }
    }
}
