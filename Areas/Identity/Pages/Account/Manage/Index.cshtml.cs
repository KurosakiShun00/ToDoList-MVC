// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList_MVC.User;

namespace ToDoList_MVC.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string? Username { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string? PhoneNumber { get; set; }
            
            [Display(Name = "Nome")]
            public string? Nome { get; set; }
            
            [Display(Name = "Cognome")]
            public string? Cognome { get; set; }
            
            [Display(Name = "NickName")]
            public string? NickName { get; set; }
            
            [Display(Name = "Sesso")]
            public int? Sesso { get; set; }
            
            [Display(Name = "DataDiNascita")]
            public DateTime? DataDiNascita { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nome = user.Nome,
                Cognome = user.Cognome,
                NickName = user.NickName,
                Sesso = user.Sesso,
                DataDiNascita = user.DataDiNascita
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            bool isModified = false;

            if (Input.Nome != user.Nome)
            {
                user.Nome = Input.Nome;
                isModified = true;
            }

            if (Input.Cognome != user.Cognome)
            {
                user.Cognome = Input.Cognome;
                isModified = true;
            }            
            if (Input.Sesso != user.Sesso)
            {
                user.Sesso = Input.Sesso;
                isModified = true;
            }            
            if (Input.DataDiNascita != user.DataDiNascita)
            {
                user.DataDiNascita = Input.DataDiNascita;
                isModified = true;
            }            
            if (Input.NickName != user.NickName)
            {
                user.NickName = Input.NickName;
                isModified = true;
            }
            
            if (isModified)
            {
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    StatusMessage = "Errore durante l'aggiornamento del profilo";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Profilo Aggiornato";
            return RedirectToPage();
        }
    }
}