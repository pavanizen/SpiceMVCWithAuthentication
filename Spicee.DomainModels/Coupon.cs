using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicee.DomainModels
{
  public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public enum ECouponType { Precent = 0, Dollar }
        [Required]
        public ECouponType CouponType { get; set; }

        

        [Required]
        public double Discount { get; set; }

        [Required]
        public double MinimumAmount { get; set; }

        public byte[] Picture { get; set; }

        public bool IsActive { get; set; }
    }
}
