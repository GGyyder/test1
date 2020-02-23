using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST.Models;

namespace TEST.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult EmployeeList(string message="")
        {

            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }

                return View();
        }

        public ActionResult EmployeeListPartialView( int page)
        {

            List<Employees> employeesList = new List<Employees>();
            int pagecount = 10;
            int skipcount = (page - 1) * pagecount;
            int Total = 1;
            using (NorthwindEntities db = new NorthwindEntities())
            {

                employeesList = db.Employees.OrderBy(c=>c.EmployeeID).Skip(skipcount).Take(pagecount).ToList();
                Total = db.Employees.Count();
            }

            int Totalpage = 1;
            if (Total > 10)
            {
                
             Totalpage=(Total / 10) + 1;
            }
            ViewBag.Totalpage = Totalpage;
            return View(employeesList);
        }

        public ActionResult EmployeeDetailEdit(int EmployeeID)
        {
            Employees employee = new Employees();
          
            using (NorthwindEntities db = new NorthwindEntities())
            {

                employee = db.Employees.Find(EmployeeID);
                var query = db.Employees.ToList();
                ViewBag.Sales = new SelectList(query, "EmployeeID", "LastName");
            }
            


            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpEmployee(Employees employees, HttpPostedFileBase newphoto)
        {
            try
            {


                Employees exmeployees = new Employees();

                if (newphoto != null)
                {
                    string contentype = newphoto.ContentType.Replace("image/", "");
                    string up_contentype = contentype.ToUpper();
                    if (up_contentype != "JPEG" && up_contentype != "GIF" && up_contentype != "PNG") //上傳檔案類型的驗證
                    {

                        return RedirectToAction("EmployeeList", new { message = "上傳圖檔格式不符 請重新上傳" });
                    }
                }
                

                if (!ModelState.IsValid)
                {

                    return RedirectToAction("EmployeeList", new { message = "修改失敗" });
                }
                else
                {


                    if (newphoto != null)
                    {
                        using (var binaryReader = new BinaryReader(newphoto.InputStream))
                        {
                            employees.Photo = binaryReader.ReadBytes(newphoto.ContentLength);
                        }
                    }
                    using (NorthwindEntities db = new NorthwindEntities())
                    {

                        db.Entry(employees).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("EmployeeList", new { message = "修改成功" });
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("EmployeeList", new { message = "修改失敗" });
            }
        }
     
        public ActionResult EmployeeDetail(int EmployeeID)
        {
            Employees employee = new Employees();

            using (NorthwindEntities db = new NorthwindEntities())
            {

                employee = db.Employees.Find(EmployeeID);

            }



            return View(employee);
        }

   
        public ActionResult DeleteEmployee (int EmployeeID)
        {
            Employees employee = new Employees();
            try
            {
                using (NorthwindEntities db = new NorthwindEntities())
                {

                    employee = db.Employees.Find(EmployeeID);
                    
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                }



                return Content("S");
            }
            catch (Exception ex)
            {
                return Content("F");
            }
        }

        public ActionResult AddEmployeeDetail()
        {

            using (NorthwindEntities db = new NorthwindEntities())
            {

         
           var query = db.Employees.ToList();
            ViewBag.Sales = new SelectList(query, "EmployeeID", "LastName");
            }
           



            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployeeDetail(Employees employees, HttpPostedFileBase newphoto)
        {

            try
            {




                if (newphoto != null)
                {
                    string contentype = newphoto.ContentType.Replace("image/", "");
                    string up_contentype = contentype.ToUpper();
                    if (up_contentype != "JPEG" && up_contentype != "GIF" && up_contentype != "PNG") //上傳檔案類型的驗證
                    {

                        return RedirectToAction("EmployeeList", new { message = "上傳圖檔格式不符 請重新上傳" });
                    }
                }
                else
                {
                    return RedirectToAction("EmployeeList", new { message = "上傳圖檔格式不符 請重新上傳" });
                }
               
                if (!ModelState.IsValid)
                {

                    return RedirectToAction("EmployeeList", new { message = "加入失敗" });
                }
                else
                {


                    if (newphoto != null)
                    {
                        using (var binaryReader = new BinaryReader(newphoto.InputStream))
                        {
                            employees.Photo = binaryReader.ReadBytes(newphoto.ContentLength);
                        }
                    }
                    using (NorthwindEntities db = new NorthwindEntities())
                    {

                        db.Employees.Add(employees);
                      
                        db.SaveChanges();
                    }
                    return RedirectToAction("EmployeeList", new { message = "加入成功" });
                }
            }
            catch (Exception ex)
            {
                
              return RedirectToAction("EmployeeList",new {message="加入失敗"});
            }


         
        }
    }
}