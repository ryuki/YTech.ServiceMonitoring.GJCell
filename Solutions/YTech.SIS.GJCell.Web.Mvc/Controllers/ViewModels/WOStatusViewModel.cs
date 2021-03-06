﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YTech.SIS.GJCell.Web.Mvc.Controllers.ViewModels
{
    public class WOStatusViewModel
    {
        [ScaffoldColumn(false)]
        public string WOStatusId
        {
            get;
            set;
        }

        [DisplayName("User")]
        public string WOStatusUser
        {
            get;
            set;
        }

        [DisplayName("Tanggal Update")]
        public DateTime? WOStatusDate
        {
            get;
            set;
        }

        [DisplayName("Status")]
        public string WOStatus
        {
            get;
            set;
        }

        [DisplayName("Update")]
        public string WOStatusBrokenDesc
        {
            get;
            set;
        }

        [DisplayName("Tanggal Mulai Dikerjakan")]
        public DateTime? WOStatusStartDate
        {
            get;
            set;
        }

        [DisplayName("Tanggal Selesai Dikerjakan")]
        public DateTime? WOStatusFinishDate
        {
            get;
            set;
        }
    }
}