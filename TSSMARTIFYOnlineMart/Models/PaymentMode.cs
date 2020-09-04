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
    public class PaymentMode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
       
        public int PaymentModeID { get; set; }
        [DisplayName(" Payment Mode")]
        [Required(ErrorMessage = " Payment Mode should not be blank")]
        public string PaymentModeName { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}