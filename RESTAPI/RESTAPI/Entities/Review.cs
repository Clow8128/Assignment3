//Cameron Low
//Distributed Applications
//Assignment 2
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Entities
{
    public class Review
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [ForeignKey("SongId")]
        public Guid SongId { get; set; }
    }
}
