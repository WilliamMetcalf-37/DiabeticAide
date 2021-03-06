﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiabeticAide.Data;
using DiabeticAide.Models;
using DiabeticAide.Models.ViewModels;
using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

namespace DiabeticAide.Controllers
{
    public class DataController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

  

        public DataController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Data
        public async Task<ActionResult> Index()
        {
            var user = await GetUserAsync();




           
            


            var patientsUserIsHelperFor = await _context.UserHelpers
                .Where(uh => uh.HelperId == user.Id)
                .Include(u => u.Patient)
                .Select(use => new UserViewModel()
                {
                    UserId = use.Patient.Id,
                    FirstName = use.Patient.FirstName,
                    LastName = use.Patient.LastName
                })
                .ToListAsync();

            if (user.IsPatient == true)
            {
                var currentUserModel = new UserViewModel()
                {
                    Id = 1,
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                patientsUserIsHelperFor.Insert(0,currentUserModel);
            }

            return View(patientsUserIsHelperFor);


        }

        // GET: Data/Details/5
        public async Task<ActionResult> Details(string userId)
        {
            var viewModel = new UserDataViewModel();
            var user = await GetUserAsync();

            var patient = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);

            var userData = await _context.UserData.Where(p => p.UserId == userId).Include(u=> u.User).ToListAsync();

            viewModel.User = user;
            viewModel.Patient = patient;
            viewModel.UsersData = userData;
            return View(viewModel);
        }

        


        // GET: Data/Create/userId

        public ActionResult Create(string userId)
        {

            return View(userId);
        }

        // POST: Data/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string userId, UserData data)
        {
            try
            {
                // TODO: Add insert logic here
                var user = await GetUserAsync();


                var newUserData = new UserData()
                {                   
                    Note = data.Note,
                    Reading = data.Reading,
                    DateTime = DateTime.Now

                };
                if(userId == null)
                {
                    newUserData.UserId = user.Id;
                }
                else
                {
                    newUserData.UserId = userId;
                }

                _context.UserData.Add(newUserData);
                await _context.SaveChangesAsync();
                if(userId != null)
                {
                return RedirectToAction("Details",new { userId = userId });

                }
                else
                {
                return RedirectToAction("Details", new { userId = user.Id });

                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Data/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = await _context.UserData.Include(u => u.User).FirstOrDefaultAsync(d => d.Id == id);
            return View(data);
        }

        // POST: Data/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserData data)
        {
            try
            {
                // TODO: Add update logic here

                _context.UserData.Update(data);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = data.UserId, userId = data.UserId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Data/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _context.UserData.Include(u=>u.User).FirstOrDefaultAsync(d => d.Id == id);
            return View(data);
        }

        // POST: Data/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, UserData data)
        {
            try
            {
                // TODO: Add delete logic here
                _context.UserData.Remove(data);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { userId = data.UserId });
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Chart(string patientId, string chartLength)
        {
            var user = await GetUserAsync();
            var viewModel = new UserDayChartViewModel()
            {
                ReadingValues = new List<AreaSeriesData>(),
                ReadingTimes = new List<string>()
            };
            var patient = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == patientId);
            var chartData = new List<UserData>();
            if (chartLength == "Month")
            {
                var thisMonth = DateTime.Now.Month;
             chartData = await _context.UserData.Where(ud => ud.UserId == patient.Id && ud.DateTime.Month == thisMonth).ToListAsync();
                viewModel.ChartTitle = "This Month's Chart";
            }else if(chartLength == "Year")
            {
                var thisYear = DateTime.Now.Year;
                chartData = await _context.UserData.Where(ud => ud.UserId == patient.Id && ud.DateTime.Year == thisYear).ToListAsync();
                viewModel.ChartTitle = "This Year's Chart";

            }else if (chartLength == "Day")
            {
                var today = DateTime.Now.Date;
                chartData = await _context.UserData.Where(ud => ud.UserId == patient.Id && ud.DateTime.Date == today).ToListAsync();
                viewModel.ChartTitle = "Today's Chart";
            }


            

            

            foreach (var item in chartData)
            {
                var areaSeriesData = new AreaSeriesData()
                {
                    Name = item.Note,
                    Y = item.Reading
                };

                viewModel.ReadingValues.Add(areaSeriesData);
                viewModel.ReadingTimes.Add(item.DateTime.ToString());

            }


            viewModel.User = user;
            viewModel.Patient = patient;


            return View(viewModel);

        }










        private async Task<ApplicationUser> GetUserAsync() => await _userManager.GetUserAsync(HttpContext.User);
    }
}