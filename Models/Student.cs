using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUD.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }

        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string ProfileImage { get; set; }
        [NotMapped]
        public List<SelectListItem> CountryList { get; set; }
        [NotMapped]
        public List<SelectListItem> StateList { get; set; }
        [NotMapped]
        public List<SelectListItem> CityList { get; set; }
        
    }
}