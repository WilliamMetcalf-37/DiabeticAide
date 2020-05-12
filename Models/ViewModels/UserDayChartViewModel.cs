using Highsoft.Web.Mvc.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabeticAide.Models.ViewModels
{
    public class UserDayChartViewModel
    {
        public ApplicationUser Patient { get; set; }
        public ApplicationUser User { get; set; }
        public List<AreaSeriesData> ReadingValues { get; set; }
        public List<string> ReadingTimes { get; set; }
    }
}
