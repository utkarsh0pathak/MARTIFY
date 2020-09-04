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
    public class MartifyOnlineMartDBContext : DbContext
    {
        public MartifyOnlineMartDBContext():base("name=ConStrMartifyOnlineMart")
        {

        }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<LoginType> LoginTypes { get; set; }
        public virtual DbSet<PaymentMode> PaymentModes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }

    }
}