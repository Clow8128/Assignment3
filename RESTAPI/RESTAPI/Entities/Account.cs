//Cameron Low
//Distributed Applications
//Assignment 2
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Entities
{
    public class Account
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
