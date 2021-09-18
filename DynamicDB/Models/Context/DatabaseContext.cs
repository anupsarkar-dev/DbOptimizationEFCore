using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DynamicDB.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("ConString")
        {

        }


        public DbSet<Employee> Employees { get; set; }
    }
}