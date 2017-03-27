using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.Website.Server.Models
{
    public class IndexViewModel
    {
        public string ContentMarkdown { get; set; }
        public IEnumerable<IEnumerable<NavLink>> NavMenues { get; set; }
        public string PageTitle { get; set; }
    }
}
