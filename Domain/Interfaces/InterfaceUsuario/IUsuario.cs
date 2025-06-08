using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceUsuario
{
    public interface IUsuario : IGeneric<ApplicationUser>
    {
        Task<ApplicationUser> OBterUsuarioPeloId(string userID);

        Task AtualizarTipoUsuario(string userID, TipoUsuario tipoUsuario);

        
    }
}
