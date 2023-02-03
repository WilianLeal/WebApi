using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using WebApi.Data.Data;

namespace WebApi.Service.Services
{
    public class ProdutoService : IProdutoService
    {
        private WebApiDbContext _context;
        public ProdutoService(WebApiDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<Produtos>>> BuscarEntreDataFabricacao(DateTime dataInicio, DateTime dataFim)
        {
            List<Produtos> produto = await _context.Produtos.AsNoTracking().Where(a => a.DataFabricacao >= dataInicio && a.DataFabricacao <= dataFim && a.SituacaoProduto == true).ToListAsync();

            return produto;
        }

        public async Task<ActionResult<List<Produtos>>> BuscarEntreDataValidade(DateTime dataInicio, DateTime dataFim)
        {
            List<Produtos> produto = await _context.Produtos.AsNoTracking().Where(a => a.DataValidade >= dataInicio && a.DataValidade <= dataFim && a.SituacaoProduto == true).ToListAsync();

            return produto;
        }

        public async Task<ActionResult<List<Produtos>>> BuscarIdFornecedor(int id)
        {
            List<Produtos> produto = await _context.Produtos.AsNoTracking().Where(a => a.IdFonecedor == id && a.SituacaoProduto == true).ToListAsync();

            return produto;
        }

        public async Task<ActionResult<List<Produtos>>> BuscarEntreIdProdutos(int idInicio, int idFim)
        {
            List<Produtos> produto = await _context.Produtos.AsNoTracking().Where(a => a.IdProduto >= idInicio && a.IdProduto <= idFim && a.SituacaoProduto == true).ToListAsync();

            return produto;
        }

        public async Task<ActionResult<List<Produtos>>> BuscarNomeFornecedoresTipo(string nome)
        {
            List<Produtos> produto = await _context.Produtos.AsNoTracking().Where(a => a.DescricaoFornecedor.Contains(nome) && a.SituacaoProduto == true).ToListAsync();

            return produto;
        }

        public async Task<ActionResult<List<Produtos>>> BuscarNomeProdutosTipo(string nome)
        {
            List<Produtos> produto = await _context.Produtos.AsNoTracking().Where(a => a.DescricaoProduto.Contains(nome) && a.SituacaoProduto == true).ToListAsync();

            return produto;
        }

        public async Task<ActionResult<Produtos>> BuscarPorId(int id)
        {
            Produtos produto = await _context.Produtos.AsNoTracking().Where(a => a.IdProduto == id && a.SituacaoProduto == true).FirstOrDefaultAsync();

            return produto;        
        }

        public async Task<ActionResult<List<Produtos>>> BuscarTodos(int skip, int take)
        {
            List<Produtos> produtos = await _context.Produtos.AsNoTracking().Skip(skip).Take(take).ToListAsync();
            return produtos;
        }

        public async Task<ActionResult<Produtos>> Create(Produtos produto)
        {
            if (produto.DataFabricacao > produto.DataValidade)
            {
                return null;
            }

            produto.SituacaoProduto = true;
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task Delete(int id)
        {
            Produtos produto = await _context.Produtos.AsNoTracking().Where(a => a.IdProduto == id).FirstOrDefaultAsync();
            produto.SituacaoProduto = false;
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> Editar(int id, Produtos produto)
        {
            Produtos produtoExistente = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(a => a.IdProduto == id);

            if (produto.DataFabricacao > produtoExistente.DataValidade)
            {
                return null;
            }

            if (produto.DescricaoProduto != null)
            {
                produtoExistente.DescricaoProduto = produto.DescricaoProduto;
            }

            if (produto.DataFabricacao != null)
            {
                produtoExistente.DataFabricacao = produto.DataFabricacao;
            }

            if (produto.DataValidade != null)
            {
                produtoExistente.DataValidade = produto.DataValidade;
            }

            if (produto.IdFonecedor != null)
            {
                produtoExistente.IdFonecedor = produto.IdFonecedor;
            }

            if (produto.DescricaoFornecedor != null)
            {
                produtoExistente.DescricaoFornecedor = produto.DescricaoFornecedor;
            }

            if (produto.CnpjFornecedor != null)
            {
                produtoExistente.CnpjFornecedor = produto.CnpjFornecedor;
            }

            _context.Produtos.Update(produtoExistente);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
