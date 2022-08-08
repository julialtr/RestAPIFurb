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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(DataContext context)
        {
            _produtoRepository = new ProdutoRepository(context);
        }

        /// <summary> 
        /// Retorna todos os produtos cadastrados
        /// </summary>
        [HttpGet]
        [Route("produtos")]
        public ActionResult<List<Produto>> GetProdutos()
        {
            try
            {
                return Ok(_produtoRepository.Get());
            }
            catch (Exception)
            {
                return BadRequest("Erro ao retornar a lista de produtos.");
            }
        }

        /// <summary> 
        /// Retorna um produto a partir do id
        /// </summary>
        [HttpGet]
        [Route("produtos/{id}")]
        public ActionResult<Produto> GetProduto(int id)
        {
            try
            {
                var produtoBD = _produtoRepository.GetByID(id);

                if (produtoBD == null)
                    return NotFound("Produto não encontrado.");

                return Ok(produtoBD);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao retornar o produto.");
            }
        }

        /// <summary> 
        /// Faz o cadastro de um novo produto a partir do nome e do preço
        /// </summary>
        [HttpPost]
        [Route("produtos")]
        public ActionResult<Produto> PostProduto(ProdutoDto produtoDto)
        {
            try
            {
                if (produtoDto.Nome == String.Empty)
                    return BadRequest("Nome deve ser informado.");

                if (produtoDto.Preco == 0)
                    return BadRequest("Preço deve ser informado.");

                Produto produtoDB = new Produto()
                {
                    Nome = produtoDto.Nome,
                    Preco = produtoDto.Preco
                };

                _produtoRepository.Insert(produtoDB);
                _produtoRepository.Save();

                return Ok(produtoDB);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao cadastrar o produto.");
            }
        }

        /// <summary> 
        /// Faz a atualização dos dados de um produto a partir do ID. Serão atualizados apenas os dados informados
        /// </summary>
        [HttpPut]
        [Route("produtos/{id}")]
        public ActionResult<string> PutProduto(int id, [FromBody] ProdutoDto produtoDto)
        {
            try
            {
                var produtoDB = _produtoRepository.GetByID(id);

                if (produtoDB == null)
                    return NotFound("Produto não encontrado.");

                if (produtoDto.Nome != String.Empty)
                    produtoDB.Nome = produtoDto.Nome;

                if (produtoDto.Preco != 0)
                    produtoDB.Preco = produtoDto.Preco;

                _produtoRepository.Save();

                return Ok("Produto alterado.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao alterar o produto.");
            }
        }

        /// <summary> 
        /// Deleta o produto a partir do ID
        /// </summary>
        [HttpDelete]
        [Route("produtos/{id}")]
        public ActionResult<string> DeleteProduto(int id)
        {
            try
            {
                var produtoDB = _produtoRepository.GetByID(id);

                if (produtoDB == null)
                    return NotFound("Produto não encontrado.");

                _produtoRepository.Delete(produtoDB);
                _produtoRepository.Save();

                return Ok("Produto removido.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao remover o produto.");
            }
        }
    }
}
