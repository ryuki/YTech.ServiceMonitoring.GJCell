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
    public class UnitTypeViewModel
    {
        [Required]
        [DisplayName("Kode Tipe Unit")]
        public string TypeID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Tipe Unit")]
        public string TypeName
        {
            get;
            set;
        }

        [DisplayName("Merk")]
        [UIHint("UnitMerk")]
        public string MerkId
        {
            get;
            set;
        }

        [DisplayName(" ")]
        [HiddenInput()]
        public string MerkName
        {
            get;
            set;
        }

        [DisplayName("Status")]
        public string TypeStatus
        {
            get;
            set;
        }

        [DisplayName("Keterangan")]
        public string TypeDesc
        {
            get;
            set;
        }
    }
}