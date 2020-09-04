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
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [ForeignKey("Customer")]
        [DisplayName("Seller Name")]
        public int CustomerID { get; set; }
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name should not be blank")]
        public string ProductName { get; set; }
        [DisplayName("About Product ")]
        [Required(ErrorMessage = "Product Description should not be blank")]
        public string Description { get; set; }
        [DisplayName("Product Quantity ")]
        [Required(ErrorMessage = "Product Quantity should not be blank")]

        public int ProductQTY { get; set; }
        [DisplayName("Product Category ")]
        [Required(ErrorMessage = "Product Category should not be blank")]
        
        [ForeignKey("Category")]


        public int CategoryID { get; set; }
        [DisplayName("Product Price ")]
        [Required(ErrorMessage = "Product Price should not be blank")]

        public double Price { get; set; }
        [DisplayName("Product Image ")]
        [Required(ErrorMessage = "Product Image should not be blank")]
        public string ProductImage { get; set; }

        public virtual Category Category { get; set; }

        public virtual Customer Customer { get; set; }
             

        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }




    }
}