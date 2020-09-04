using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TSSMARTIFYOnlineMart.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name should not be blank")]
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}