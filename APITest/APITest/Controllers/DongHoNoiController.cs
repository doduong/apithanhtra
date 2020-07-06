using APITest.Extensions;
using APITest.Helper;
using APITest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace APITest.Controllers
{
    public class DongHoNoiController : ApiController
    {
        /*
        [HttpGet]
        [Route("api/GetUsePointNotRead")]
        public List<DiemDungChuaDoc> GetAll()
        {
            string ms_tuyen = HttpContext.Current.Request.QueryString.Get("ms_tuyen");
            string ms_tk = HttpContext.Current.Request.QueryString.Get("ms_tk");
            SqlHelper _db = new SqlHelper();
            string query1 = "select * from v_pointnotread where ms_tuyen = " + ms_tuyen + " and ms_tk = "+ms_tk;
            DataTable dt = _db.ExecuteSQLDataTable(query1);
            List<DiemDungChuaDoc> lsp = DataTableToListExtensions.DataTableToList<DiemDungChuaDoc>(dt);
            return lsp;
        }
        */


    }
}
