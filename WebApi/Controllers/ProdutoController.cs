using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Services;

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
        public async Task<ActionResult<List<Produtos>>> BuscarTodos()
        {
            return await _service.BuscarTodos();
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
                throw new Exception("A data de fabricação não pode ser maior que a data de validade.");
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
                throw new Exception("O produto buscado ou não exite ou foi deletado.");
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
                throw new Exception("A data de fabricação não pode ser maior que a data de validade.");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
