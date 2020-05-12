using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DiabeticAide.Models;
using DiabeticAide.Data;
using Microsoft.AspNetCore.Identity;
using DiabeticAide.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Highsoft.Web.Mvc.Charts;

namespace DiabeticAide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<IActionResult> Index()
        {

            if (_signInManager.IsSignedIn(User))
            {
                var user = await GetUserAsync();
            if(user.IsPatient == true)
            {
                var today = DateTime.Now.Date;
                var TodaysData = await _context.UserData.Where(ud => ud.UserId == user.Id && ud.DateTime.Date == today).ToListAsync();
                    var viewModel = new UserDayChartViewModel()
                    {
                        ReadingValues = new List<AreaSeriesData>(),
                        ReadingTimes = new List<string>()
                };

                foreach(var item in TodaysData)
                    {
                        var areaSeriesData = new AreaSeriesData()
                        {
                            Name = "Blood Sugar Reading",
                            Y = item.Reading
                        };

                        viewModel.ReadingValues.Add(areaSeriesData);
                        viewModel.ReadingTimes.Add(item.DateTime.ToString());

                    }


                viewModel.User = user;
                viewModel.Patient = user;


                   var jsonOutputOfViewModel =  JsonConvert.SerializeObject(viewModel);




                    return View(viewModel);

            }
            else
            {
                return RedirectToAction("Index", "Data");
            }
            }
            else
            {
                //return RedirectToAction(nameof(Privacy));
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
           
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task<ApplicationUser> GetUserAsync() => await _userManager.GetUserAsync(HttpContext.User);
    }
}
