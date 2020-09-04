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
    public class Customer
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Please enter your name which should contain atleast 2 characters")]
        [DisplayName("Name")]
        [StringLength(30, MinimumLength = 2)]
       
        [RegularExpression("^[a-zA-Z][a-zA-Z\\s]+$",ErrorMessage = "Please enter correct valid name")]
        public string CustomerName { get; set; }
        
        [ForeignKey("LoginType")]
        [DisplayName("Select type")]
        [Required(ErrorMessage = "You need to select one of them")]
        public int LoginTypeID { get; set; }
        
        [DisplayName("Email")]
        [Required(ErrorMessage = "Enter Email Address")]
        [DataType(DataType.EmailAddress)]
       
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password should not be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Phone Number")]
        //(372) 587-2335
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Required(ErrorMessage = "Phone number should not be blank eg. (372) 587-2335")]
        public string Contact { get; set; }
        [DisplayName("Address")]
        [Required(ErrorMessage = "Address should not be blank")]
        public string Address { get; set; }
        [DisplayName("City")]
        [Required(ErrorMessage = "City should not be blank")]
        public string City { get; set; }
        [DisplayName("Pin Code")]
        [Required(ErrorMessage = "Pin Code should not be blank")]
        public string PinCode { get; set; }

        public virtual LoginType LoginType { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Product> Products { get; set; }


        


    }
}