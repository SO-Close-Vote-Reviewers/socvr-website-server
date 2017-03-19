using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Models
{
    public class NavLink : IEquatable<NavLink>
    {
        public string Display { get; set; }
        public string Path { get; set; }

        /// <summary>
        /// Compares this NavLink object with the given NavLink object.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NavLink other)
        {
            return
                Display == other.Display &&
                Path == other.Path;
        }
    }
}
