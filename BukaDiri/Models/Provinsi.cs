using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BukaDiri.Models
{
    [Table("Provinsi")]
    public class Provinsi
    {
        [Key]
        [MaxLength(5), MinLength(5)]
        public string kode_provinsi { get; set; }
        
        [Required]
        public string nama_provinsi { get; set; }
        
        public int jumlah_kota { get; set; } //? = allow null (boleh kosong)
        //public int? jumlah_kota { get; set; } //? = allow null (boleh kosong), Crystal report gak support null
    }
}