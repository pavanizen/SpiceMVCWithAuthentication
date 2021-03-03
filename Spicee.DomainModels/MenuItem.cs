﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicee.DomainModels
{
   public class MenuItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public enum ESpicy { NA = 0, NOT_SPICY, SPICY, VERY_SPICY }
       // public ESpicy Spicyness { get; set; }

        //public enum ESpicy { NA = 0, NOT_SPICY, SPICY, VERY_SPICY }
        public string Spicyness { get; set; }
        public string Image { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price should be greater then ${1}")]
        public double Price { get; set; }
    }
}
