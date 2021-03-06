﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Enteprise_programming_evita.Models
{
    public class ItemType
    {

          
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

     


        [Required]
        [MinLength(2, ErrorMessage = "Your Item type name is too short!")]
        public string Name { get; set; }

       
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }

}
