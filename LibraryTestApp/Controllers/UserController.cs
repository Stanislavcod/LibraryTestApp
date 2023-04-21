﻿using Library.BusinessLogic.Services.Contracts;
using Library.Common.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTestApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _userService.Get();
            return View(users);
        }
        [HttpPost("EditUser")]
        public IActionResult EditUser([FromForm] UserDto request, [FromForm] string password)
        {
            _userService.Edit(request, password);
            return RedirectToAction("GetUsers", "Users");
        }
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser([FromForm] int id)
        {
            _userService.Delete(id);
            return RedirectToAction("GetUsers", "Users");
        }
        [HttpPost("EditPassword")]
        public IActionResult EditPassword(EditPasswordDto request)
        {
            if (request.NewPassword != request.ConfirmPassword)
                throw new Exception("Пароли не совпадают!");
            var user = _userService.Get(User.Identity.Name);
            _userService.EditPassword(user, request.OldPassword, request.NewPassword);
            return RedirectToAction("Index", "Book");
        }
    }
}