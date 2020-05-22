using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xunit;
using Xunit.Sdk;

namespace Enteprise_programming_evita.Models
{
    public class Item 
    {
        [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }

        [Index("IX_itemt", 1, IsUnique = true)]
        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        [Required]
        [Range(1, 10000000, ErrorMessage = "The quantity  needs to be positive")]
        [Index("IX_itemt", 4, IsUnique = true)]
        public int Quantity { get; set; }

        [Index("IX_itemt", 2, IsUnique = true)]
        public int QualityId { get; set; }
        public Quality Quality { get; set; }

        [Required]
        [Index("IX_itemt", 3, IsUnique = true)]
      
        [Range(1.0, 10000000.0, ErrorMessage = "The price for a property needs to be positive")]
        public decimal Price { get; set; }
        public DateTime AddingDate { get; set; }


        
      
        




    }
}