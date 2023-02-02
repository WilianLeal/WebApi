using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace WebApi.Services
{
    public class ProdutoService : IProdutoService
    {
        private WebApiDbContext _context;
        public ProdutoService(WebApiDbContext service)
        {
            _context = service;
        }

        public async Task<ActionResult<Produtos>> BuscarPorId(int id)
        {
            Produtos produto = await _context.Produtos.AsNoTracking().Where(a => a.IdProduto == id && a.SituacaoProduto == true).FirstOrDefaultAsync();

            return produto;        
        }

        public async Task<ActionResult<List<Produtos>>> BuscarTodos()
        {
            List<Produtos> produtos = await _context.Produtos.ToListAsync();
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
