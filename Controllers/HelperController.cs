using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabeticAide.Data;
using DiabeticAide.Models;
using DiabeticAide.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DiabeticAide.Controllers
{
    public class HelperController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HelperController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Helper
        public async Task<ActionResult> Index()
        {
            var user = await GetUserAsync();
            var HelperViewModel = new AddHelperViewModel()
            {
                PotentialHelpers = new List<SelectListItem>()
            };
            HelperViewModel.CurrentHelpers = await _context.UserHelpers.Where(u => u.PatientId == user.Id)
                .Select(use => new UserViewModel()
                {
                    UserId = use.Helper.Id,
                    FirstName = use.Helper.FirstName,
                    LastName = use.Helper.LastName
                })
                .ToListAsync();
            var idList = new List<string>();
            foreach (var item in HelperViewModel.CurrentHelpers)
            {
                idList.Add(item.UserId);
            }

            var allUsersExceptCurrentUser = await _context.ApplicationUsers.Where(u => u.Id != user.Id)
                .Select(use => new SelectListItem()
                {
                    Text = use.FirstName + " " + use.LastName,
                    Value = use.Id.ToString()
                })
                .ToListAsync();
            
            foreach (var TheUser in allUsersExceptCurrentUser)
            {
                bool ishelper = false;
                foreach (var id in idList)
                {
                    if (id == TheUser.Value)
                    {
                        ishelper = true;
                    }
                    else
                    {
                        ishelper = false;
                    }
                }
                if (ishelper == false)
                {
                    HelperViewModel.PotentialHelpers.Add(TheUser);           
                }
            }
        
            return View(HelperViewModel);
        }

        // GET: Helper/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Helper/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Helper/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Helper/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Helper/Edit/5
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

        // GET: Helper/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Helper/Delete/5
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