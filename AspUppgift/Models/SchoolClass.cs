using AspUppgift.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspUppgift.Models
{
    public class SchoolClass
    {
        [Required]
        [Key]
        public string Id { get; set; }

#nullable enable
        public ApplicationUser? Teacher { get; set; }
        public virtual ICollection<ApplicationUser>? Students { get; set; }
#nullable disable

        public SchoolClass(string id)
        {
            Id = id;
        }

        public SchoolClass()
        {

        }
    }
}
