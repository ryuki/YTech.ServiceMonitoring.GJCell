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
    public class RequestWOSPartViewModel
    {
        [DisplayName("WOSPartId")]
        public string WOSPartId
        {
            get;
            set;
        }

        [DisplayName("WOId")]
        public string WOId
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

        [DisplayName("Tanggal")]
        [UIHint("Date")]
        public DateTime? WOSPartDate
        {
            get;
            set;
        }

        [DisplayName("Spare Part")]
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

        [DisplayName("Jumlah")]
        public decimal? WOSPartQty
        {
            get;
            set;
        }
        [DisplayName("Harga")]
        public decimal? WOSPartPrice
        {
            get;
            set;
        }
        [DisplayName("Diskon")]
        public decimal? WOSPartDisc
        {
            get;
            set;
        }
        [DisplayName("Total")]
        public decimal? WOSPartTotal
        {
            get;
            set;
        }

        [DisplayName("Tanggal Terima")]
        [UIHint("Date")]
        public DateTime? WOSPartDateReceived
        {
            get;
            set;
        }

        [DisplayName("Diterima Oleh")]
        [UIHint("Emp")]
        public string WOSPartReceivedBy
        {
            get;
            set;
        }

        [DisplayName("Tanggal Retur")]
        [UIHint("Date")]
        public DateTime? WOSPartDateReturn
        {
            get;
            set;
        }

        [DisplayName("Diretur Oleh")]
        [UIHint("Emp")]
        public string WOSPartReturnBy
        {
            get;
            set;
        }

        [DisplayName("Status")]
        [UIHint("WOSPartStatus")]
        public string WOSPartStatus
        {
            get;
            set;
        }

        [DisplayName("Direquest Oleh")]
        [UIHint("Emp")]
        public string WOSPartRequestBy
        {
            get;
            set;
        }

        [DisplayName("Tanggal Request")]
        [UIHint("Date")]
        public DateTime? WOSPartDateRequest
        {
            get;
            set;
        }
    }
}