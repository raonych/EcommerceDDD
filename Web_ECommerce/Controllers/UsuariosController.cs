using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    [LogActionFilter]
    public class UsuariosController : BaseController
    {
        private readonly InterfaceUsuarioApp _InterfaceUsuarioApp;
        public UsuariosController(
            UserManager<ApplicationUser> userManager,
            ILogger<ProdutosController> Logger,
            IWebHostEnvironment environment,
            InterfaceLogSistemaApp interfaceLogSistemaApp,
            InterfaceUsuarioApp InterfaceUsuarioApp) : base(Logger, userManager, interfaceLogSistemaApp)
        {
            _InterfaceUsuarioApp = InterfaceUsuarioApp;
        }
        public async Task<IActionResult> ListarUsuarios()
        {
            return View(await _InterfaceUsuarioApp.ListarUsuariosSomenteParaAdmnistradores(await RetornarIdUsuarioLogado()));
        }

        public async Task<IActionResult> Edit(string id) 
        {
            var tipoUsuarios = new List<SelectListItem>();
            tipoUsuarios.Add(new SelectListItem { Text = Enum.GetName(typeof(TipoUsuario), TipoUsuario.Comum), Value = Convert.ToInt32(TipoUsuario.Comum).ToString() });
            tipoUsuarios.Add(new SelectListItem { Text = Enum.GetName(typeof(TipoUsuario), TipoUsuario.Administrador), Value = Convert.ToInt32(TipoUsuario.Administrador).ToString() });
            ViewBag.TipoUsuarios = tipoUsuarios;

            return View(await _InterfaceUsuarioApp.ObterUsuarioPeloID(id));
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationUser usuario)
        {
            try
            {
                await _InterfaceUsuarioApp.AtualizarTipoUsuario(usuario.Id, (TipoUsuario)usuario.Tipo);

                await LogEcommerce(EnumTipoLog.Informativo, usuario);

                return RedirectToAction(nameof(ListarUsuarios));

            }
            catch (Exception erro)
            {

                await LogEcommerce(EnumTipoLog.Erro, erro);

                return View("Edit", usuario);
            }
        }
    }
}
