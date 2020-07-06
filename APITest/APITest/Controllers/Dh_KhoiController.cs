using APITest.Extensions;
using APITest.Helper;
using APITest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace APITest.Controllers
{
    public class Dh_KhoiController : ApiController
    {
        /*
        //-----------biendoc------------
        [HttpGet]
        [Route("api/dhkhoi/getbiendoc")]
        public JsonResult<List<BienDoc>> GetBienDoc()
        {
            string ms_bd = HttpContext.Current.Request.QueryString.Get("ms_bd");
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable("select * from bien_doc where ms_bd = "+ms_bd);
            List<BienDoc> lsp = DataTableToListExtensions.DataTableToList<BienDoc>(dt);
            return Json(lsp);
        }

        //----------nhom-----------------
        [HttpGet]
        [Route("api/dhkhoi/getnhom")]
        public JsonResult<List<Nhom>> GetNhom()
        {
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable("select ms_nhom, mo_ta, ngay_thuc_te from nhom_dh_khoi ndhk inner join cky_bien_doc  ck" +
                                                    " on ndhk.ms_nhom = ck.ms_cky where ngay_thuc_te  <= CAST(GETDATE() AS DATE)");
            List<Nhom> lsp = DataTableToListExtensions.DataTableToList<Nhom>(dt);
            return Json(lsp);
        }

        //----------NHÓM TUẦN-----------------
        [HttpGet]
        [Route("api/dhkhoi/getnhomtuan")]
        public JsonResult<List<Nhom>> GetNhomTuan()
        {
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable("select ms_nhom_tuan, mo_ta from nhom_dhk_tuan");
            List<Nhom> lsp = DataTableToListExtensions.DataTableToList<Nhom>(dt);
            return Json(lsp);
        }

        //----------THONG TIN ĐẢO SỐ-----------------
        [HttpGet]
        [Route("api/dhkhoi/getthongtindaoso")]
        public JsonResult<List<ThongTinDaoSo>> GetThongTinDaoSo()
        {
            string ms_dh = HttpContext.Current.Request.QueryString.Get("ms_dh");
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable("select top 1 so_lan_dao_so, ms_dh, ms_ls_daoso FROM   dbo.lich_su_dao_so_dh where ms_dh = "+ms_dh+" order by ms_ls_daoso desc");
            List<ThongTinDaoSo> lsp = DataTableToListExtensions.DataTableToList<ThongTinDaoSo>(dt);
            return Json(lsp);
        }
        //----------THOI_KY--------------
        [HttpGet]
        [Route("api/dhkhoi/getthoiky")]
        public JsonResult<DataTable> GetThoiKy()
        {
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable($"select ms_tk, thoi_ky from thoi_ky where ms_tk = (select max(ms_tk) from thoi_ky)");
            return Json(dt);
        }

        //----------CHECK LOGIN--------------
        [HttpGet]
        [Route("api/dhkhoi/checklogin")]
        public JsonResult<DataTable> CheckLogin()
        {
            string ms_bd = HttpContext.Current.Request.QueryString.Get("ms_bd");
            string mat_khau = HttpContext.Current.Request.QueryString.Get("mat_khau");
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable($"select ms_bd , ten_bd, ghi_chu from bien_doc where ms_bd = "+ms_bd+" and mat_khau = '"+mat_khau+"'");
            return Json(dt);
        }                                       
        //----------DONG HO LIEN QUAN-----------------
        [HttpGet]
        [Route("api/dhkhoi/getdhlienquan")]
        public JsonResult<List<DongHoLienQuan>> GetDHLienQuan()
        {
            string ms_bd = HttpContext.Current.Request.QueryString.Get("ms_bd");
            string ms_nhom = HttpContext.Current.Request.QueryString.Get("ms_nhom");
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable("select lq.ms_dhk_lq, lq.ms_dhk FROM dh_khoi dhk inner join dh_khoi_lquan lq " +
                                                    "on dhk.ms_dhk = lq.ms_dhk and ms_tt_dh = 1 and ms_nhom = "+ms_nhom+" and ms_bd = "+ms_bd);
            List<DongHoLienQuan> lsp = DataTableToListExtensions.DataTableToList<DongHoLienQuan>(dt);
            return Json(lsp);
        }

        [HttpGet]
        [Route("api/dhkhoi/getall")]
        public JsonResult<List<DongHoKhoi>> GetAll()
        {
            string ms_bd = HttpContext.Current.Request.QueryString.Get("ms_bd");
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable($"Select ms_dhk,ms_dh,ms_tdhk,ms_tk,ten_dhk,so_thu_tu,so_tthu_cu1," +
                "chi_so_cu1,ngay_doc_cu1,ngay_doc_moi1 ,chi_so_moi1,s_tieu_thu1, so_tthu_cu2 ,chi_so_cu2,ngay_doc_cu2,chi_so_moi2,ngay_doc_moi2" +
                " ,s_tieu_thu2,toa_do_bac ,toa_do_dong ,ms_nhom ,ms_tt_dh,co_chi_so true,ms_bd ,ms_phuong  from dh_khoi where ms_bd = " + ms_bd);
            List<DongHoKhoi> lsp = DataTableToListExtensions.DataTableToList<DongHoKhoi>(dt);
            return Json(lsp);
        }

        [HttpGet]
        [Route("api/dhkhoi/getdhknhom")]
        public JsonResult<DataTable> GetDHKNhom()
        {
            string ms_bd = HttpContext.Current.Request.QueryString.Get("ms_bd");
            string ms_nhom = HttpContext.Current.Request.QueryString.Get("ms_nhom");
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable("WITH ds AS (SELECT * FROM dh_khoi  WHERE ms_nhom = " + ms_nhom + " and ms_bd = " + ms_bd + " and ms_tt_dh = 1 " +
                " UNION ALL " +
                " select * from dh_khoi where ms_dhk in " +
                    "(select lq.ms_dhk_lq " +
                    " FROM dh_khoi dhk inner join dh_khoi_lquan lq on dhk.ms_dhk = lq.ms_dhk " +
                    " and ms_nhom = " + ms_nhom + " and ms_bd = " + ms_bd + " and ms_tt_dh = 1 )) Select DISTINCT * from ds order by so_thu_tu ") ;
            List <DongHoKhoi> lsp = DataTableToListExtensions.DataTableToList<DongHoKhoi>(dt);
            return Json(dt);
        }

        [HttpGet]
        [Route("api/dhkhoi/searchdhk")]
        public JsonResult<DataTable> SearchDHKNhom()
        {
            string ms_bd = HttpContext.Current.Request.QueryString.Get("ms_bd");
            string ms_nhom = HttpContext.Current.Request.QueryString.Get("ms_nhom");
            string conditionField = HttpContext.Current.Request.QueryString.Get("conditionField");
            string keyWord = HttpContext.Current.Request.QueryString.Get("keyWord");
            String strwhere = "";
            if (conditionField.Equals("ms_dhk"))
            {
                strwhere = " where ms_dhk like '%" + keyWord + "%'";
            } else if (conditionField.Equals("ten_dhk"))
            {
                strwhere = " where ten_dhk like N'%" + keyWord + "%'";
            } else if (conditionField.Equals("seri_dh"))
            {
                strwhere = " where so_seri like N'%" + keyWord + "%'";
            }

            //conditionField
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable("WITH ds AS (SELECT ms_dhk, dhk.ms_dh, ms_tdhk, ms_tk, ten_dhk, so_thu_tu, so_tthu_cu1, chi_so_cu1, " +
                " ngay_doc_cu1, ngay_doc_moi1, chi_so_moi1, s_tieu_thu1, so_tthu_cu2, chi_so_cu2, " +
                " ngay_doc_cu2, chi_so_moi2, ngay_doc_moi2, s_tieu_thu2, toa_do_bac, toa_do_dong, ms_nhom," +
                " dhk.ms_tt_dh, co_chi_so,ms_bd, ms_phuong, ghi_chu, url_image, dh.so_seri FROM dh_khoi dhk left join dong_ho dh on dhk.ms_dh=dh.ms_dh " +
                " WHERE ms_nhom = " + ms_nhom + " and ms_bd = " + ms_bd + " and dhk.ms_tt_dh = 1 " +
                " UNION ALL " +
                " select ms_dhk, dhk.ms_dh, ms_tdhk, ms_tk,ten_dhk, so_thu_tu, so_tthu_cu1, chi_so_cu1, ngay_doc_cu1, ngay_doc_moi1, " +
                " chi_so_moi1, s_tieu_thu1, so_tthu_cu2, chi_so_cu2, ngay_doc_cu2, chi_so_moi2, " +
                " ngay_doc_moi2, s_tieu_thu2, toa_do_bac, toa_do_dong, ms_nhom, dhk.ms_tt_dh, co_chi_so,ms_bd, ms_phuong, ghi_chu,url_image, dh.so_seri " +
                " from dh_khoi dhk left join dong_ho dh on dhk.ms_dh=dh.ms_dh where ms_dhk in " +
                    "(select lq.ms_dhk_lq " +
                    " FROM dh_khoi dhk inner join dh_khoi_lquan lq on dhk.ms_dhk = lq.ms_dhk " +
                    " and ms_nhom = " + ms_nhom + " and ms_bd = " + ms_bd + " and dhk.ms_tt_dh = 1 )) Select DISTINCT * from ds " + strwhere+" order by so_thu_tu ");
            List<DongHoKhoi> lsp = DataTableToListExtensions.DataTableToList<DongHoKhoi>(dt);
            return Json(dt);
        }






        [HttpGet]
        [Route("api/dhkhoi/getdhkbyid")]
        public JsonResult<DataTable> GetDHKByID(int ms_dhk)
        {
            string str_ms_dhk = HttpContext.Current.Request.QueryString.Get("ms_dhk");
            ms_dhk = Convert.ToInt32(str_ms_dhk);
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable("SELECT dhk.ms_dhk " +
                                                ", dhk.ms_dh " +
                                                ", dh.so_seri " +
                                                ", dh.kha_nang_dh " +
                                                ", dh.he_so " +
                                                ", dhk.so_thu_tu " +
                                                ", dhk.ten_dhk " +
                                                ", dhk.toa_do_bac " +
                                                ", dhk.ms_nhom " +
                                                ", ndhk.mo_ta " +
                                                ", dhk.toa_do_dong " +
                                                ", dhk.ms_tdhk " +
                                                ", tdhk.ten_tdhk " +
                                                ", dhk.ms_tk " +
                                                ", dhk.ms_tt_dh " +
                                                ", dhk.ngay_doc_cu1 " +
                                                ", dhk.chi_so_cu1 " +
                                                ", dhk.so_tthu_cu1 " +
                                                ", dhk.s_tieu_thu1 " +
                                                ", dhk.ngay_doc_moi1 " +
                                                ", dhk.chi_so_moi1 " +
                                                ", dhk.ngay_doc_cu2 " +
                                                ", dhk.chi_so_cu2 " +
                                                ", dhk.so_tthu_cu2 " +
                                                ", dhk.s_tieu_thu2 " +
                                                ", dhk.chi_so_moi2 " +
                                                ", dhk.ngay_doc_moi2 " +
                                                ", dhk.co_chi_so " +
                                                ", dhk.ms_bd " +
                                                ", dhk.ms_phuong " +
                                                ", dhk.ghi_chu " +
                                                ", dhk.url_image " +
                                                ", phuong.ten_phuong " +
                                                 " FROM dh_khoi dhk left join " +
                                                 " dong_ho dh on dhk.ms_dh = dh.ms_dh left join " +
                                                 " nhom_dh_khoi ndhk on dhk.ms_nhom = ndhk.ms_nhom left join " +
                                                " tong_dh_khoi tdhk on dhk.ms_tdhk = tdhk.ms_tdhk " +
                                                 " left join bien_doc bd on dhk.ms_bd = bd.ms_bd " +
                                                 " left join phuong on dhk.ms_phuong = phuong.ms_phuong " +
                                                 " where dhk.ms_tt_dh = 1 and dhk.ms_dhk = " + ms_dhk);
            List<DongHoKhoi> lsp = DataTableToListExtensions.DataTableToList<DongHoKhoi>(dt);
            return Json(dt);
        }
        [HttpPut]
        [Route("api/dhkhoi/updatecsm/{id}")]
        public void UpdateDHKhoi1(int id, [FromBody]DHKhoiCapNhatChiSo dhkcn)
        {
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";    // modify the format depending upon input required in the column in database 
            SqlHelper _db = new SqlHelper();
            if (dhkcn.loai_chi_so == 1)
            {
                DataTable dt = _db.ExecuteSQLDataTable($"Update dh_khoi set chi_so_moi1 = " + dhkcn.chi_so_moi + ", s_tieu_thu1 = " + dhkcn.so_tieu_thu + ", ngay_doc_moi1 = N'" + time.ToString(format) + "', ghi_chu = N'"+dhkcn.ghi_chu+"' where ms_dhk = " + id);
            }else if (dhkcn.loai_chi_so == 2)
            {
                DataTable dt = _db.ExecuteSQLDataTable($"Update dh_khoi set chi_so_moi2 = " + dhkcn.chi_so_moi + ", s_tieu_thu2 = " + dhkcn.so_tieu_thu + ", ngay_doc_moi2 = N'" + time.ToString(format) + "', ghi_chu = N'" + dhkcn.ghi_chu + "' where ms_dhk = " + id);
            }

        }

        /*[HttpPut]
        [Route("api/dhkhoi/updatecsm1/{id}")]
        public void UpdateDHKhoi2(int id, [FromBody]DongHoKhoi dhk)
        {
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd";    // modify the format depending upon input required in the column in database 
            SqlHelper _db = new SqlHelper();
            dhk.chi_so_moi2 = 1039066;
            dhk.s_tieu_thu2 = dhk.chi_so_moi2 - dhk.chi_so_cu2;
            DataTable dt = _db.ExecuteSQLDataTable($"Update dh_khoi set chi_so_moi2 = " + dhk.chi_so_moi2 + ", s_tieu_thu2 = 10, ngay_doc_moi2 = N'" + time.ToString(format) + "' where ms_dhk = " + id);

        }*/
        /*
        [HttpPut]
        [Route("api/dhkhoi/changepwd/{id}")]
        public bool ChangePwd(int id, [FromBody]UpdateUserPwd userchange)
        {
            SqlHelper _db = new SqlHelper();
            try
            {
                int rowEffect = Convert.ToInt32(_db.ExecuteSQLNonQuery($"Update bien_doc set mat_khau = '" + userchange.NewPwd + "'  where ms_bd = " + id + " and mat_khau = '" + userchange.OldPwd + "'"));
                if (rowEffect == 1)
                {
                    return true;
                } else 
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;

            }

        }

        [HttpPut]
        [Route("api/dhkhoi/updatekodocduoc/{id}")]
        public void UpdateDHKhoiKhongDocDuoc(int id, [FromBody]DHKhoiKhongDocDuoc dhkkd)
        {
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";
            string format1 = "yyyyMMddHHmmss";// modify the format depending upon input required in the column in database 
            String url_path = "/ImageStorage/";
            String ImgName = id + "_" + time.ToString(format1);
            String url = url_path + ImgName + ".jpg";
            String sql = "Update dh_khoi set url_image = N'" + url + ", ngay_doc_moi2 = N'" + time.ToString(format) + "' where ms_dhk = " + id;
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable($"Update dh_khoi set url_image = N'" + url + "', ghi_chu = N'"+ dhkkd.ghi_chu + "', ngay_doc_moi2 = N'" + time.ToString(format) + "' where ms_dhk = " + id);
            SaveImage(dhkkd.base_64_image, ImgName);
        }

        /*[HttpPost]
        [Route("api/dhkhoi/insertlsds")]
        public void GetOne([FromBody]MeterResetHis his)
        {
            DateTime time = DateTime.Now;
            string format = "yyyy-MM-dd";
            SqlHelper _db = new SqlHelper();
            DataTable dt = _db.ExecuteSQLDataTable($"Insert into lich_su_dao_so_dh (ms_dh, ms_mnoi, ngay_thuc_hien, chi_so_dh, so_lan_dao_so) values (N'" + his.ms_dh + "', " + his.ms_mnoi + ", N'" + time.ToString(format) + "' , "+ his.so_lan_dao_so+ ")");

        }*/

        /*public bool SaveImage(string ImgStr, string ImgName)
        {
            ///WaterMeterImgs/2170028_636957629673844114.jpg
            String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path
            //String path = "E:/WebTest/ImageStorage";
            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = ImgName + ".jpg";

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(ImgStr);

            File.WriteAllBytes(imgPath, imageBytes);

            return true;
        }*/


        
    }
}
