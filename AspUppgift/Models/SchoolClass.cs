using AspUppgift.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspUppgift.Models
{
    public class SchoolClass
    {   [Required]
        [Key]
        public string Id { get; set; }

        public ApplicationUser Teacher { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}
