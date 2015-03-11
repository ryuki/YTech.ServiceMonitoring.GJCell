using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace YTech.SIS.GJCell.Web.Mvc.Controllers.ViewModels
{
    public class UnitMerkViewModel
    {
        [Required]
        [DisplayName("Kode Merk Unit")]
        public string MerkID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Merk Unit")]
        public string MerkName
        {
            get;
            set;
        }

        [DisplayName("Status")]
        public string MerkStatus
        {
            get;
            set;
        }

        [DisplayName("Keterangan")]
        public string MerkDesc
        {
            get;
            set;
        }
    }
}