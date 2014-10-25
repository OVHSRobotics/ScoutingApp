using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FRCScout.Models
{
    public class Picture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PictureId { get; set; }

        public string FileName { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Robot Robot { get; set; }
    }
}