// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Finances.Models;
using Finances.Services.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static QRCoder.PayloadGenerator;

namespace Finances.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
      
public async Task<IActionResult> SendConfirmEmail()
{
    // Get the currently logged-in user
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return NotFound("Unable to load user.");
    }

    // Get the user's email
    var receiver = user.Email;
    var subject = "Test";
    var htmlMessage = "hello";

    // Send a test email
    _emailService.SendEmail(receiver, subject, htmlMessage);

    // Create an EmailDto for the confirmation email
    var userId = await _userManager.GetUserIdAsync(user);
    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    var callbackUrl = Url.Page(
        "/Account/ConfirmEmail",
        pageHandler: null,
        values: new { area = "Identity", userId = userId, code = code },
        protocol: Request.Scheme);

    var emailDto = new EmailDto
    {
        To = receiver,
        Subject = "Confirm your email",
        Body = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
    };

    // Send the confirmation email
    SendEmail(emailDto);

    // Redirect to a confirmation page or other appropriate page
    return RedirectToPage("/Email");
}

private void SendEmail(EmailDto emailDto)
{
    try
    {
        _emailService.SendEmail(emailDto.To, emailDto.Subject, emailDto.Body);
    }
    catch (Exception ex)
    {
        // Log the exception or handle it accordingly
        // Example: _logger.LogError($"Error sending email: {ex.Message}");
    }
}


        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return RedirectToPage("/Email");
        }
    }
}
