using System;
using System.ComponentModel.DataAnnotations;

namespace LackingWebSecurityCore.Models.DataLayer
{
    public class Patient
    {
        [Key]
        public string MRN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
    }
}
