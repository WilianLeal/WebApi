using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Data.Data;

namespace WebApi.Service.Services
{
    public interface IProdutoService
    {
        Task<ActionResult<List<Produtos>>> BuscarTodos(int skip, int take);
        Task<ActionResult<List<Produtos>>> BuscarEntreIdProdutos(int idInicio, int idFim);
        Task<ActionResult<List<Produtos>>> BuscarNomeProdutosTipo(string nome);
        Task<ActionResult<List<Produtos>>> BuscarIdFornecedor(int id);
        Task<ActionResult<List<Produtos>>> BuscarNomeFornecedoresTipo(string nome);
        Task<ActionResult<List<Produtos>>> BuscarEntreDataFabricacao(DateTime dataInicio, DateTime dataFim);
        Task<ActionResult<List<Produtos>>> BuscarEntreDataValidade(DateTime dataInicio, DateTime dataFim);
        Task<ActionResult<Produtos>> Create(Produtos produto);
        Task<ActionResult<Produtos>> BuscarPorId(int id);
        Task<IActionResult> Editar(int id, Produtos produto);
        Task Delete(int id);
    }
}
