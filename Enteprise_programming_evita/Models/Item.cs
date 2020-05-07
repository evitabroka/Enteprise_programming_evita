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
        public int ItemId { get; set; }

       
      
        public int ItemTypeId { get; set; }

       
        public ItemType ItemType { get; set; }

        [Required]
        [Range(1, 10000000, ErrorMessage = "The quantity  needs to be positive")]
        public int Quantity { get; set; }

       
        public int QualityId { get; set; }
        public Quality Quality { get; set; }

        [Required]
        [Range(1.0, 10000000.0, ErrorMessage = "The price for a property needs to be positive")]
        public decimal Price { get; set; }

        public string Owner { get; set; }


       

    }
}