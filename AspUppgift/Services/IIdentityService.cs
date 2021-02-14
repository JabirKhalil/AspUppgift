using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspUppgift.Services
{
   public interface IIdentityService
    {
        Task CreateRootAccountAsync();

    }
}
