﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticAide.Data;
using DiabeticAide.Models;
using DiabeticAide.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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




            if (user.IsPatient == true)
            {
                var currentUserModel = new UserViewModel()
                {
                    Id = 1,
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }



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
                patientsUserIsHelperFor.Add(currentUserModel);
            }

            return View(patientsUserIsHelperFor);


        }

        // GET: Data/Details/5
        public async Task<ActionResult> Details(string userId)
        {
            var user = await GetUserAsync();
            var userData = await _context.UserData.Where(p => p.UserId == userId).ToListAsync();

            return View(userData);
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
                    UserId = user.Id,
                    Note = data.Note,
                    Reading = data.Reading,
                    DateTime = DateTime.Now

                };

                _context.UserData.Add(newUserData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Data/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Data/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Data/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Data/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private async Task<ApplicationUser> GetUserAsync() => await _userManager.GetUserAsync(HttpContext.User);
    }
}