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
    public class Cart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }
        
        [ForeignKey("Customer")]
        [DisplayName("Booking Person")]
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Select Product")]
        [DisplayName("Product")]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Enter Quantity to purchase ")]
        [DisplayName("Quantity to purchase")]
        public int PurchaseQTY { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }





    }
}