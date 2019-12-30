using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BukaDiri.Models
{
    [Table("Pilihan")]
    public class Pilihan
    {
        [Key]
        [MaxLength(5)]
        public string kode_pilihan { get; set; }
        public string nama_pilihan { get; set; }
        public int diskon_pilihan { get; set; }
    }
}