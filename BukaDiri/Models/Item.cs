using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BukaDiri.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public string kode_item { get; set; }
        public string nama_item { get; set; }
        public int harga_item { get; set; }
        
        [MaxLength(5)]
        public string kode_provinsi { get; set; }
        public Provinsi Provinsi { get; set; }

        [MaxLength(5)]
        public string kode_pilihan{ get; set; }
        public Pilihan Pilihan { get; set; }

        [MaxLength(5)]
        public string kode_lapak { get; set; }
        public Lapak Lapak { get; set; }
    }
}