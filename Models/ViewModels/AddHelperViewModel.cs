﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabeticAide.Models.ViewModels
{
    public class AddHelperViewModel
    {
        public string HelperId { get; set; }
        public List<UserViewModel> CurrentHelpers { get; set; }
        public string NewHelperId { get; set; }
        public List<SelectListItem> PotentialHelpers { get; set; }
    }
}
