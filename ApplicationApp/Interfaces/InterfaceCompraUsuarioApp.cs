﻿using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceCompraUsuarioApp : InterfaceGenericApp<CompraUsuario>
    {
        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        public Task<CompraUsuario> CarrinhoCompras(string userId);

        public Task<CompraUsuario> ProdutosComprados(string userId, int? idCompra = null);

        public Task<List<CompraUsuario>> MinhasCompras(string userId);

        public Task AdicionaProdutoCarrinho(string userId, CompraUsuario compraUsuario);

        public Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);
    }
}
