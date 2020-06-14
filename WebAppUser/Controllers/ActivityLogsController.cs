using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using WebAppUser.Data;
using WebAppUser.Helpers;
using WebAppUser.Models;
using WebAppUser.ViewModels;

namespace WebAppUser.Controllers
{
    public class ActivityLogsController : Controller, ILogToDB
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ActivityLogsController(ApplicationDbContext context,
         UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ActivityLogs
        public async Task<IActionResult> Index()
        {
            var activityLog = await _context.ActivityLog.Select(a => new ActivityLog
            {
                IdActivityLog = a.IdActivityLog,
                Date = a.Date,
                Ip = a.Ip,
                MacAddress = a.MacAddress,
                NameDevice = a.NameDevice,
                UserId = a.UserId,
                UserEMail = a.UserEMail,
                UserFullName = a.UserFullName,
                Action = a.Action,
                Controller = a.Controller,
                Description = a.Description,
                Status = a.Status,
                Type = a.Type,
                Photo =  _context.Users.Where(x=>x.Id == a.UserId).Select(u=>u.PhotoPath).FirstOrDefault() == null ?
                        _context.Users.Where(x => x.Id == a.UserId).Select(u => u.PhotoBase64).FirstOrDefault() :
                        _context.Users.Where(x => x.Id == a.UserId).Select(u => u.PhotoPath).FirstOrDefault()
            }).ToListAsync();
           
            return View(activityLog);
            //return View(await _context.ActivityLog.ToListAsync());
        }

        // GET: ActivityLogs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityLog = await _context.ActivityLog
                .FirstOrDefaultAsync(m => m.IdActivityLog == id);
            if (activityLog == null)
            {
                return NotFound();
            }

            return View(activityLog);
        }

        // GET: ActivityLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActivityLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<bool> Create(ActivityLogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserEMail);

                if (user != null)
                {
                    var Registro = new ActivityLog();
                    Registro.Date = DateTime.Now;
                    Registro.Ip = ActivityLogHelper.GetIPClient();
                    Registro.MacAddress = ActivityLogHelper.GetMacAdreessDeviceClient();
                    Registro.NameDevice = ActivityLogHelper.GetNameDeviceClient();

                    Registro.UserId = user.Id;
                    Registro.UserEMail = user.Email;
                    Registro.UserFullName = user.FullName;                   
                    Registro.Controller = model.Controller;
                    Registro.Action = model.Action;
                    Registro.Description = model.Description;
                    Registro.Status = model.Status;
                    Registro.Type = model.Type;

                    _context.Add(Registro);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }


        // GET: ActivityLogs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityLog = await _context.ActivityLog.FindAsync(id);
            if (activityLog == null)
            {
                return NotFound();
            }
            return View(activityLog);
        }

        // POST: ActivityLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ActivityLog activityLog)
        {
            if (id != activityLog.IdActivityLog)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityLogExists(activityLog.IdActivityLog))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(activityLog);
        }

        // GET: ActivityLogs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityLog = await _context.ActivityLog
                .FirstOrDefaultAsync(m => m.IdActivityLog == id);
            if (activityLog == null)
            {
                return NotFound();
            }

            return View(activityLog);
        }

        // POST: ActivityLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var activityLog = await _context.ActivityLog.FindAsync(id);
            _context.ActivityLog.Remove(activityLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityLogExists(string id)
        {
            return _context.ActivityLog.Any(e => e.IdActivityLog == id);
        }
    }
}
