﻿using SOCVR.Website.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Services
{
    interface INavigationMenusProvider
    {
        IEnumerable<IEnumerable<NavLink>> GetNavigationMenus(string path);
    }
}
