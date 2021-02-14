using AspUppgift.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspUppgift.Models
{
    public class EditClassesViewModel
    {
        public SchoolClass CurrentClass { get; set; }
        public IEnumerable<ApplicationUser> Teachers { get; set; }

        public IEnumerable<ApplicationUser> Students { get; set; }
    }
}
