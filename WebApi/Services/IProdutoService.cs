using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IProdutoService
    {
        Task<ActionResult<List<Produtos>>> BuscarTodos();
        Task<ActionResult<Produtos>> Create(Produtos produto);
        Task<ActionResult<Produtos>> BuscarPorId(int id);
        Task<IActionResult> Editar(int id, Produtos produto);
        Task Delete(int id);
    }
}
