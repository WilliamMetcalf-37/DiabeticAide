using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabeticAide.Models
{
    public class UserHelper
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }
        public string HelperId { get; set; }
        public ApplicationUser Helper { get; set; }
    }
}
