using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace DiabeticAide.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool IsPatient { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorName { get; set; }
        public string DoctorPhone { get; set; }

    }
}
