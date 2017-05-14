using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Libellus.Managers
{
    public class RoleCustomManager : RoleManager<IdentityRole>
    {
        public RoleCustomManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
    }
}