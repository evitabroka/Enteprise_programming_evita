using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enteprise_programming_evita.Models
{
    public class Item
    {
        /// <summary>
        /// Id is the primary key.. It should be named Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Quality { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Owner { get; set; }


       

    }
}