using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppMontaMenu : InterfaceMontaMenu
    {
        private readonly IServiceMontaMenu _menu; 
        public AppMontaMenu(IServiceMontaMenu IServiceMontaMenu) 
        {
            _menu = IServiceMontaMenu;
        }
        public async Task<List<MenuSite>> MontaMenuPorPerfil(string userID)
        {
             return await _menu.MontaMenuPorPerfil(userID);
        }
    }
}
