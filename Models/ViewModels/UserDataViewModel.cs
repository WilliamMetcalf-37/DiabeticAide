using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabeticAide.Models.ViewModels
{
    public class UserDataViewModel
    {
       public ApplicationUser Patient { get; set; }
       public ApplicationUser User { get; set; }
       public List<UserData> UsersData { get; set; }
       
    }
}
