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
    public class LoginType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginTypeID { get; set; }
        [DisplayName("Login Type")]
        [Required(ErrorMessage = "Login type should not be blank eg. Vendor / Customer ")]
        public string LoginTypeName { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }


    }
}