using MVCCRUD.BAL;
using MVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace MVCCRUD.Controllers
{
    public class HomeController : Controller
    {
        StudentDbEntities1 _context = new StudentDbEntities1();
        StudentLogic studentlogic = new StudentLogic();
        Student studentDC = new Student();
        public ActionResult Index()
        {
            var listofData = _context.tblStudents.ToList();
            var BaseUrl = "https://localhost:44308/" + "Content/DB_Images/User/";
            foreach( var i in listofData)
            {
                i.ProfileImage = BaseUrl+ i.ProfileImage;
            }
            return View(listofData);
        }
        [HttpGet]
        public ActionResult Create(int? id)
        {
            
            try
            {
                
                if (id > 0)
                {
                    var Editview1 = _context.tblStudents.Where(x => x.StudentId == id).FirstOrDefault();
                    studentDC.StudentId= Editview1.StudentId;   
                    studentDC.Name = Editview1.Name;
                    studentDC.Course = Editview1.Course;
                    studentDC.ProfileImage= Editview1.ProfileImage;
                    studentDC.CountryList = studentlogic.GetCountryList();
                    studentDC.StateList = studentlogic.GetStateList(Convert.ToInt32(Editview1.CountryId));
                    studentDC.CityList = studentlogic.GetCityList(Convert.ToInt32(Editview1.StateId));
                    studentDC.CountryName = Editview1.CountryName;
                    studentDC.StateName = Editview1.StateName;
                    studentDC.CityName = Editview1.CityName;    

                    studentDC.CountryId = Editview1.CountryId;
                    studentDC.StateId = Editview1.StateId;
                    studentDC.CityId = Editview1.CityId;

                }
                else
                {
                    studentDC.CountryList = studentlogic.GetCountryList();
                    studentDC.StateList = studentlogic.GetStateList(Convert.ToInt32(studentDC.CountryId));
                    studentDC.CityList = studentlogic.GetCityList(Convert.ToInt32(studentDC.StateId));

                }



            }
            catch(Exception ex) { }
            return View(studentDC);
        }
        [HttpPost]
        public ActionResult Create(tblStudent model, HttpPostedFileBase userImage)
        {
            string filename = null;
            if (userImage != null && !string.IsNullOrEmpty(userImage.FileName))
            {
                filename = Path.GetFileName(userImage.FileName);
                var PathFile = Path.Combine(Server.MapPath("~/" + ConfigurationManager.AppSettings["User"].ToString()), filename);

                userImage.SaveAs(PathFile);
                studentDC.ProfileImage = filename;

                model.ProfileImage = filename;


            }

            if (model.StudentId > 0 )
            {
                _context.tblStudents.AddOrUpdate(model);
                _context.SaveChanges();
                ViewBag.message = "Data Edit Successfully";
                
            }

            else
            {
                
                _context.tblStudents.Add(model);
                _context.SaveChanges();
                ViewBag.message = "Data Added Successfully";
            }
           
           

                return RedirectToAction("Index");
            }
            
        
        
        public ActionResult Delete(int id)
        {
            var DeleteRecord = _context.tblStudents.Where(x => x.StudentId == id).FirstOrDefault();
            _context.tblStudents.Remove(DeleteRecord);
            _context.SaveChanges();
            ViewBag.message = "Data Deleted Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            var Studentdetails = _context.tblStudents.Where(x => x.StudentId == id).FirstOrDefault();
            return View(Studentdetails);
        }

        public JsonResult DistrictByStateId(int CountryId)
        {
            StudentLogic stlogic = new StudentLogic();
            List<SelectListItem> statelist = null;

            //int langId = GetCookieLanguage();
            statelist = stlogic.GetStateList(CountryId);

            return Json(statelist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CityByStateId(int StateId)
        {
            List<SelectListItem> statelistObj = null;
            StudentLogic Stulogic = new StudentLogic();

            statelistObj = Stulogic.GetCityList(StateId);


            return Json(statelistObj, JsonRequestBehavior.AllowGet);
        }

    }
}