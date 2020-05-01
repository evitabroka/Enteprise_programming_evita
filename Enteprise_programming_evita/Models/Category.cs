using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enteprise_programming_evita.Models
{
    public class Category
    {

        /// <summary>
        /// Id is the primary key.. It should be named Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Your category name is too short!")]
        public string Name { get; set; }

        /// <summary>
        /// Since we are using the virtual keyword, the Properties for a locality are ONLY loaded when needed
        /// </summary>
        public virtual ICollection<ItemType> ItemTypes { get; set; }

    }
}