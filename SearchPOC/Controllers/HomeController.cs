using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SearchPOC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        //Search Code

        public JsonResult TagSearch(string term)
        { 
            var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var command = new SqlCommand("Employees_GetEmployeeNames", conn) {
                           CommandType = CommandType.StoredProcedure };
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            
                    //mylist.Add(new Tuple<int, string>(reader.GetInt32(reader.GetOrdinal("EmployeeID")), reader.GetString(reader.GetOrdinal("FullName"))));
                    string[] Employees = new string[] { };
                   List<string> myCollection = new List<string>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Instead of displaying to console this is where you would add
                            // the current item to your list
                            myCollection.Add(reader.GetString(0));
                        }
                    }



                    reader.Close();
                    conn.Close();
                    return this.Json(myCollection.Where(t => t.StartsWith(term)),
                            JsonRequestBehavior.AllowGet);
        }
    }
}