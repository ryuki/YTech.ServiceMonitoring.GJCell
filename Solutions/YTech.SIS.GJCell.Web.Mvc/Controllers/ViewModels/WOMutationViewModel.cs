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
    public class WOMutationViewModel
    {
        [DisplayName("MutationWOId")]
        public string MutationWOId
        {
            get;
            set;
        }

        [DisplayName("Mutasi ke")]
        [UIHint("UserName")]
        [Required]
        public string UserName
        {
            get;
            set;
        }
    }
}