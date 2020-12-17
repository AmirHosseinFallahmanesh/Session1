﻿using demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace demo.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }



        public IActionResult Index()
        {
            roleManager.Roles.ToList();
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            AppUser app = new AppUser();

           var result= roleManager.CreateAsync(new IdentityRole(roleViewModel.Name)).Result;
            return View();
        }
    }
}
