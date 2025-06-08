using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    public class MontaMenu : BaseController
    {
        private readonly InterfaceMontaMenu _menu;

        public MontaMenu(
            UserManager<ApplicationUser> userManager,
            InterfaceCompraUsuarioApp InterfaceCompraUsuarioApp,
            ILogger<ProdutosController> logger,
            InterfaceLogSistemaApp interfaceLogSistemaApp,
            IWebHostEnvironment environment,
            InterfaceMontaMenu InterfaceMontaMenu)
        : base(logger, userManager, interfaceLogSistemaApp)
        { 
            _menu = InterfaceMontaMenu; 
        }
        [AllowAnonymous]
        [HttpGet("/api/ListarMenu")]
        public async Task<IActionResult> ListarMenu()
        {
            var listaMenu = new List<MenuSite>();

            var userId = await RetornarIdUsuarioLogado();

            listaMenu = await _menu.MontaMenuPorPerfil(userId);

            return Json(new { listaMenu });
        }

    }
}
