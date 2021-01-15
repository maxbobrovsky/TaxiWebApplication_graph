using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiWebApplication.Models
{
    public class Order
    {

        public Guid Id { get; set; }

        //public bool State { get; set; } = false;
        public string Address { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        //public User User { get; set; }

        public DateTime OrderCreated { get; set; } = DateTime.Now;

    }


}
