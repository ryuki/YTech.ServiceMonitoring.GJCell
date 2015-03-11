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
    public class SPartViewModel
    {
        //[ScaffoldColumn(false)]
        [DisplayName("Kode Spare Part")]
        public string SPartID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Spare Part")]
        public string SPartName
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Harga Beli")]
        [UIHint("Currency")]
        public decimal? SPartPurchasePrice
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Harga Jual")]
        [UIHint("Currency")]
        public decimal? SPartServicePrice1
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

        [DisplayName("Keterangan")]
        public string SPartDesc
        {
            get;
            set;
        }
    }

  
}