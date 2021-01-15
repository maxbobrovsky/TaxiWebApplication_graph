using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiWebApplication.Models
{
    public class User : IdentityUser
    {
        public string NativeCity { get; set; }

        //public List<Order> Orders { get; set; }

       
    }
}
