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
    public class TransactionDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrasactionID { get; set; }

        [ForeignKey("Bill")]
        public int BillID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Please enter quantity")]
        [DisplayName("Quantity")]
        public int PruchaseQTY { get; set; }
       
        [DisplayName("Purchase Amount")]
        public double PurchaseAmout { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }




    }
}