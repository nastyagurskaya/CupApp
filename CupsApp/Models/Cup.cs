﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CupsApp.Models
{
    public enum CupType
    {
        tea, coffee
    }
    public class Cup
    {
        [Key]
        public int CupId { get; set; }
        [Required]
        [Range(0.1, 2.0)]
        public double Capacity { get; set; }
        public CupType? CupType { get; set; }
        public int CountryID { get; set; }
        public virtual ICollection<CupImage> CupImages { get; set; }
        public virtual Country Country { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase ImageUpload { get; set; }
       

    }
}