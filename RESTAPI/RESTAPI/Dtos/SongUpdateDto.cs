//Cameron Low
//Distributed Applications
//Assignment 2
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Dtos
{
    public class SongUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string FilePath { get; set; }
        public bool IsPopular { get; set; }
    }
}

