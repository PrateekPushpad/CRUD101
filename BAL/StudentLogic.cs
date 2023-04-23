using Microsoft.Ajax.Utilities;
using MVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUD.BAL
{
    public class StudentLogic
    {
        StudentDbEntities1 _context = new StudentDbEntities1();
        public List<SelectListItem> GetCountryList()
        {
            List<SelectListItem> countrylistObj = new List<SelectListItem>();
            Student studentModel = new Student();
            try
            {
                List<tblCountry> countries = _context.tblCountries.ToList();
                countries.ForEach(x =>
                {
                    countrylistObj.Add(new SelectListItem { Text = x.CountryName, Value = x.CountryId.ToString() });
                });
                
            }
            catch (Exception ex)
            {

            }

            return countrylistObj;
        }

        public List<SelectListItem> GetStateList(int CountryId)
        {
            Student studentModel = new Student();
            List<SelectListItem> objList = null;
            try
            {
                objList = _context.tblStates.Where(x => x.CountryId == CountryId).Select(x => new SelectListItem
                
                {
                    Text = x.StateName,

                    Value = x.StateId.ToString()
                }).ToList();
            }
            catch (Exception ex)
            {
               
            }
            return objList;
        }

        public List<SelectListItem> GetCityList(int StateId)
              {
            List<SelectListItem> citylistObj = null;
            Student studentModel = new Student();
            try
            {
                citylistObj = _context.tblCities.Where(x => x.StateId == StateId).Select(x => new SelectListItem
                {
                    Text = x.CityName,
                    Value = x.CityId.ToString()
                }).ToList();
                

            }
            catch
            {

            }
            return citylistObj;
        }

        public List<tblStudent> GetStudentbyLogin(string Name, string Course)
        {
            Student stuModel = new Student();


            List<tblStudent> st = null;
            st = _context.tblStudents.Where(x => x.Name == Name && x.Course == Course).ToList();


            return st;
        }
    }
}