﻿using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    public class CompraUsuarioController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly InterfaceCompraUsuarioApp _InterfaceCompraUsuarioApp;
        public CompraUsuarioController(UserManager<ApplicationUser> userManager, InterfaceCompraUsuarioApp interfaceCompraUsuarioApp)
        {
            _userManager = userManager;
            _InterfaceCompraUsuarioApp = interfaceCompraUsuarioApp;
        }

        [HttpPost("/api/AdicionarProdutoCarrinho")]
        public async Task<JsonResult> AdicionarProdutoCarrinho(string id, string nome, string qtd)
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario != null)
            {
                await _InterfaceCompraUsuarioApp.Add(new CompraUsuario
                {
                    IdProduto = Convert.ToInt32(id),
                    QtdCompra = Convert.ToInt32(qtd),
                    Estado = EnumEstadoCompra.Produto_Caminho,
                    UserId = usuario.Id
                });
                return Json(new { sucesso = true });
            }

            return Json(new { sucesso = false });

        }


    }
}
