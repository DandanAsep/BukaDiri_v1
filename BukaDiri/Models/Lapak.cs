using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BukaDiri.Models
{
    [Table("Lapak")]
    public class Lapak
    {
        [Key]
        [MaxLength(5)]
        public string kode_lapak { get; set; }
        public string nama_lapak { get; set; }
        public int peringkat_lapak { get; set; }
    }
}