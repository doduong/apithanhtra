using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITest.Models
{
    public class NoiDungKiemSoat
    {
        public int ms_ks { get; set; }

        public int ms_mnoi { get; set; }

        public int  ms_userthanhtra { get; set; }

        public int ms_tk { get; set; }

        public DateTime ngay_kiem_soat { get; set; }

        public int chi_so_cu { get; set; }

        public int chi_so_moi { get; set; }

        public int chi_so_ks { get; set; }

        public int so_tthu_ks { get; set; }

        public int ms_dk_ks { get; set; }

        public int ms_tt_ks { get; set; }

        public string ghi_chu_ks { get; set; }

        public string url_image_ks { get; set; }

        public string thu_muc { get; set; }

        public int co_chi_so { get; set; }
        public int ms_tql { get; set; }
        public int co_chi_so_moi { get; set; }
        public DateTime ngay_doc_cu { get; set; }
        public DateTime ngay_doc_moi { get; set; }
    }
}