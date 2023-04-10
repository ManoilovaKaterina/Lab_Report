using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace ookplab3
{
    public interface IItem
    {
        int Id { get; set; }
    }
    public class Item : IItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime SupplyDate { get; set; }
        public double Weight { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
    }

    public class ItemsContext : DbContext
    {
        public ItemsContext() : base("DefaultConnection") { }
        public DbSet<Item> Items { get; set; }
    }
}
