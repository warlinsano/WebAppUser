using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using WebAppUser.Data;

namespace WebAppUser.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IUserHelper _userHelper;
        //private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
             ApplicationDbContext context,
             UserManager<User> userManager,
             SignInManager<User> signInManager,
             RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _SignInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var aspNetUsers = await _userHelper.GetUsersByIdAsync(id);
            //var aspNetUsers = await _userManager.FindByIdAsync(id);
            var User = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // public IActionResult Create(IFormCollection collection)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User User)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    _context.Add(User);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(User);
                }

            }
            return View(User);
        }


        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            // Create Admin Role
            string roleName = "Facturador";
            var si = await _roleManager.RoleExistsAsync(roleName);
            IdentityResult roleResult;

            // Check to see if Role Exists, if not create it
            if (!si)
            {
                roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            //var user =await _userManager.FindByNameAsync("warlinsano@yopmail.com");
            //var rol2 = await  _userManager.AddToRoleAsync(user, "Developer");
            //var rol1 = await _userManager.AddToRoleAsync(user, "Admin");

            if (id == null)
            {
                return NotFound();
            };

            var userSearched= await _context.Users.FindAsync(id);
            if (userSearched == null)
   {
                return NotFound();
            }

            var MisRoles = (
                 from r in _context.Roles
                 join ru in _context.UserRoles on r.Id equals ru.RoleId
                 where ru.UserId == userSearched.Id
                 select r.Name
                ).ToArray();

            //obtengo todos los roles de la tabla  roles
            var AllRole = _context.Roles.ToArray();

            string[,] todosRoles;
            todosRoles = new string[AllRole.Count(), 2];
            for (int i = 0; i < AllRole.Count(); i++)
            {
                if (MisRoles.Contains(AllRole[i].Name.ToString()))
                {
                    todosRoles[i, 0] = AllRole[i].Name;
                    todosRoles[i, 1] = "true";
                }
                else
                {
                    todosRoles[i, 0] = AllRole[i].Name;
                    todosRoles[i, 1] = "false";
                }
            }
            //todosRoles;
            User usuario = new User
            {
                Id = userSearched.Id,
                UserName = userSearched.UserName,
                NormalizedUserName = userSearched.NormalizedUserName,
                Email = userSearched.Email,
                todosRoles = todosRoles
            };

            //var mir = _context.AspNetRoles(x=>x.)
            //return View(aspNetUsers);
            //ViewBag.Estado = new SelectList(lst, "Value", "Text", usuario.Estado);
            LlenarDdlEstado(usuario.Estado);
            return View(usuario);
        }
        
        //llena el DDL de Estado  en Editar
  
        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
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


        private void LlenarDdlEstado(bool Estado)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Habilitado", Value = "true" });
            lst.Add(new SelectListItem() { Text = "Desabilitado", Value = "false" });
            ViewBag.Estado = new SelectList(lst, "Value", "Text", Estado);
        }
    }
}