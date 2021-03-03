using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicee.DomainModels
{
    public class Category
    {
        [Key] //Id is automatically set as primary key
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Name { get; set; }
    }
}
