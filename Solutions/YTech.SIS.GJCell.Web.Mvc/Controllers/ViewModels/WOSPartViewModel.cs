using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Web.Mvc.Controllers.ViewModels
{
    public class WOSPartViewModel
    {
        [ScaffoldColumn(false)]
        public string WOSPartID
        {
            get;
            set;
        }

        [DisplayName(" ")]
        [HiddenInput()]
        public string WONo
        {
            get;
            set;
        }

        [DisplayName("Nama Spare Part")]
        [UIHint("SPart")]
        public string SPartId
        {
            get;
            set;
        }

        [DisplayName(" ")]
        [HiddenInput()]
        public string SPartName
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Kuantitas")]
        public decimal? WOSPartQty
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Harga")]
        public decimal? WOSPartPrice
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Diskon")]
        public decimal? WOSPartDisc
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Total")]
        public decimal? WOSPartTotal
        {
            get;
            set;
        }
    }
}