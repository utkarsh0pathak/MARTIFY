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
    public class Bill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = " Date should not be blank")]
        public DateTime BillDate { get; set; }
        [DisplayName("Total Amount")]

        public double TotalAmount { get; set; }

        [ForeignKey("PaymentMode")]
        [DisplayName("Mode Of Payment")]
        //[Required(ErrorMessage = " Payment Mode should not be blank")]
        public int PaymentModeID { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual PaymentMode PaymentMode { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }


    }
}