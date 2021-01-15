using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiWebApplication.ViewModels
{
    
    public class OrderViewModel
    {
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Lattitude")]
        public double Lattitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }
    }
}
