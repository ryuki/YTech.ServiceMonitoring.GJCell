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
    public class EmpViewModel
    {
        //[ScaffoldColumn(false)]
        [DisplayName("Kode Karyawan")]
        public string EmpID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Karyawan")]
        public string EmpName
        {
            get;
            set;
        }

        [DisplayName("Telp / HP")]
        public string EmpPhone
        {
            get;
            set;
        }

        [DisplayName("Alamat")]
        public string EmpAddress
        {
            get;
            set;
        }

        [DisplayName("Tgl Mulai Bekerja")]
        [UIHint("Date")]
        public DateTime? EmpWorkSince
        {
            get;
            set;
        }

        [DisplayName("Jenis Kelamin")]
        public string EmpGender
        {
            get;
            set;
        }

        [DisplayName("Status")]
        public string EmpStatus
        {
            get;
            set;
        }

        [DisplayName("Komisi")]
        [UIHint("Currency")]
        public decimal? EmpCommission
        {
            get;
            set;
        }

        [DisplayName("Agama")]
        public string EmpReligion
        {
            get;
            set;
        }

        [DisplayName("Bagian")]
        public string EmpDepartment
        {
            get;
            set;
        }

        [DisplayName("Keterangan")]
        public string EmpDesc
        {
            get;
            set;
        }
    }

  
}