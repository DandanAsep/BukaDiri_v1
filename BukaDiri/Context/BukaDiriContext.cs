using BukaDiri.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BukaDiri.Context
{
    public class BukaDiriContext : DbContext
    {
        public DbSet<Provinsi> Provinsi { get; set; }
        public DbSet<Pilihan> Pilihan { get; set; }
        public DbSet<Lapak> Lapak { get; set; }
        public DbSet<Item> Item { get; set; }
    }
}