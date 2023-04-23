using MVCCRUD.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUD.Controllers
{
    public class StudentLoginController : Controller
    {
        StudentLogic stlogic = new StudentLogic();
        // GET: StudentLogin
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection ormCollection)
        {
            if(ormCollection == null)
            {
                string name = ormCollection["Name"];
                string course = ormCollection["Course"];

                List<tblStudent> tblstudentObj = stlogic.GetStudentbyLogin(name, course);

            }
            return View();
        }
    }
}