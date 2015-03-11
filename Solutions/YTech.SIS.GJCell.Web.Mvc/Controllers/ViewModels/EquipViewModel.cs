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
    public class EquipViewModel
    {
        [Required]
        [DisplayName("Kode Perlengkapan")]
        public string EquipID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Perlengkapan")]
        public string EquipName
        {
            get;
            set;
        }

        [DisplayName("Status")]
        public string EquipStatus
        {
            get;
            set;
        }

        [DisplayName("Keterangan")]
        public string EquipDesc
        {
            get;
            set;
        }
    }
}