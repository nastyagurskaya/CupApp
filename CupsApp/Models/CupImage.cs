using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CupsApp.Models
{
    public class CupImage
    {
        [Key]
        public int CupImgID { get; set; }
        public byte[] Image { get; set; }
        public int CupId { get; set; }
        public virtual Cup Cup { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        
    }
}