using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITest.Models
{
    public class ThongTinKhachHang
    {
        public int ms_mnoi { get; set; }

        public int stt_lo_trinh { get; set; }

        public string nguoi_thue { get; set; }

        public string dia_chi { get; set; }

        public decimal ms_dh { get; set; }

        public string so_seri { get; set; }

        public float chi_so_cu { get; set; }

        public float chi_so_moi { get; set; }

        public float so_tthu { get; set; }

        public DateTime ngay_doc_cu { get; set; }

        public DateTime ngay_doc_moi { get; set; }

        public int so_ho { get; set; }

        public string dien_thoai { get; set; }

        public decimal ms_tuyen { get; set; }

        public decimal ms_tk { get; set; }            

        public decimal ms_ttrang_doc { get; set; }

        public decimal ms_tt_dd { get; set; }
        public string ghi_chu { get; set; }
        public string url_image { get; set; }


    }
}