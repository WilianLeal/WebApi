using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Service.Services;
using WebApi.Data.Data;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("v1/produtos")]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _service;
        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Produtos>>> BuscarTodos([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return await _service.BuscarTodos(skip, take);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Produtos>> Create(Produtos produto)
        {
            var validacao = await _service.Create(produto);

            if (validacao != null)
            {
                return validacao;
            }
            else
            {
                return StatusCode(500, "A data de fabricação não pode ser maior que a data de validade.");
            }            
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Produtos>> BuscarPorId(int id)
        {
            var resutado = await _service.BuscarPorId(id);

            if (resutado != null)
            {
                return resutado;
            }
            else
            {
                return StatusCode(500, "O produto buscado ou não exite ou foi deletado.");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] Produtos produto)
        {
            var validacao = await _service.Editar(id, produto);

            if (validacao != null)
            {
                return validacao;
            }
            else
            {
                return StatusCode(500, "A data de fabricação não pode ser maior que a data de validade.");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("{idInicio:int}/{idFim:int}")]
        public async Task<ActionResult<List<Produtos>>> BuscarEntreIdProdutos(int idInicio, int idFim)
        {
            ActionResult<List<Produtos>> resutado = await _service.BuscarEntreIdProdutos(idInicio, idFim);

            if (resutado != null)
            {
                return resutado;
            }
            else
            {
                return StatusCode(500, "Os produtos buscados não exitem ou foram deletados.");
            }
        }

        [HttpGet]
        [Route("fornecedores/{id:int}")]
        public async Task<ActionResult<List<Produtos>>> BuscarPorIdFornecedor(int id)
        {
            ActionResult<List<Produtos>> resutado = await _service.BuscarIdFornecedor(id);

            if (resutado != null)
            {
                return resutado;
            }
            else
            {
                return StatusCode(500, "Os produtos buscados não exitem ou foram deletados.");
            }
        }

        [HttpGet]
        [Route("fornecedores/{nome}")]
        public async Task<ActionResult<List<Produtos>>> BuscarPorNomeFornecedor(string nome)
        {
            ActionResult<List<Produtos>> resutado = await _service.BuscarNomeFornecedoresTipo(nome);

            if (resutado != null)
            {
                return resutado;
            }
            else
            {
                return StatusCode(500, "Os produtos buscados não exitem ou foram deletados.");
            }
        }

        [HttpGet]
        [Route("{nome}")]
        public async Task<ActionResult<List<Produtos>>> BuscarPorNomeProduto(string nome)
        {
            ActionResult<List<Produtos>> resutado = await _service.BuscarNomeProdutosTipo(nome);

            if (resutado != null)
            {
                return resutado;
            }
            else
            {
                return StatusCode(500, "Os produtos buscados não exitem ou foram deletados.");
            }
        }

        [HttpGet]
        [Route("fabricacao")]
        public async Task<ActionResult<List<Produtos>>> BuscarEntreDatasFabricacaoProdutos([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            ActionResult<List<Produtos>> resutado = await _service.BuscarEntreDataFabricacao(inicio, fim);

            if (resutado != null)
            {
                return resutado;
            }
            else
            {
                return StatusCode(500, "Os produtos buscados não exitem ou foram deletados.");
            }
        }

        [HttpGet]
        [Route("validade")]
        public async Task<ActionResult<List<Produtos>>> BuscarEntreDatasValidadeProdutos([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            ActionResult<List<Produtos>> resutado = await _service.BuscarEntreDataValidade(inicio, fim);

            if (resutado != null)
            {
                return resutado;
            }
            else
            {
                return StatusCode(500, "Os produtos buscados não exitem ou foram deletados.");
            }
        }
    }
}
