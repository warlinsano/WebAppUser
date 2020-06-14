using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppUser.Models;
using WebAppUser.ViewModels;

namespace WebAppUser.Helpers
{
    public interface ILogToDB
    {
           // GET: ActivityLogs
          Task<IActionResult> Index();

          // GET: ActivityLogs/Details/5
          Task<IActionResult> Details(string id);

          // GET: ActivityLogs/Create
          public IActionResult Create();

           // POST: ActivityLogs/Create
          Task<bool> Create(ActivityLogViewModel model);

           // GET: ActivityLogs/Edit/5
          Task<IActionResult> Edit(string id);

          // POST: ActivityLogs/Edit/5
          Task<IActionResult> Edit(string id, ActivityLog activityLog);

          // GET: ActivityLogs/Delete/5
          Task<IActionResult> Delete(string id);
    
           // POST: ActivityLogs/Delete/5
          Task<IActionResult> DeleteConfirmed(string id);

          //bool ActivityLogExists(string id);
    }
}
