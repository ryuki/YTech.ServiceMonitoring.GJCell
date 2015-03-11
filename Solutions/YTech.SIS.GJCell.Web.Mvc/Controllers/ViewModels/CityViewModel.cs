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
    public class CityViewModel
    {
        [Required]
        [DisplayName("Kode Kota")]
        public string CityID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Kota")]
        public string CityName
        {
            get;
            set;
        }

        [DisplayName("Status")]
        public string CityStatus
        {
            get;
            set;
        }

        [DisplayName("Keterangan")]
        public string CityDesc
        {
            get;
            set;
        }
    }
}