using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MVCDHProject.Models;
using System.Text;
using MimeKit;
using MailKit.Net.Smtp;


namespace MVCDHProject.Controllers
{

    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #region Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = new IdentityUser { UserName = userModel.Name, Email = userModel.Email, PhoneNumber = userModel.Mobile };
                var result = await userManager.CreateAsync(identityUser, userModel.Password);
                if (result.Succeeded)
                {
                    //Performing a Sign-In into the applic
                    //await signInManager.SignInAsync(IdentityUser, false);
                    //return RedirectToAction("Index", "Home");
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                    var confirmationUrlLink = Url.Action("ConfirmEmail", "Account", new { Userid = identityUser.Id, Token = token }, Request.Scheme);
                    SendMail(identityUser, confirmationUrlLink, "Email Confirmation link");
                    TempData["Title"] = "Email Confirmation Link";
                    TempData["Message"] = "A confirm email link has been sent to your registered mail, click on it to confirm.";
                    return View("DisplayMessages");


                }
                else
                {
                    foreach (var Error in result.Errors)
                    {
                        //Displaying error details to the user
                        ModelState.AddModelError("", Error.Description);
                    }
                }
            }
            return View(userModel);
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginModel.Name, loginModel.Password, loginModel.RememberMe, false);
                if (result.Succeeded)

                {
                    if (string.IsNullOrEmpty(loginModel.ReturnUrl))
                        return RedirectToAction("Index", "Home");


                    else
                        return LocalRedirect(loginModel.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials");
                }
            }
            return View(loginModel);
        }
        #endregion
        #region Logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion

        #region SendMail


        public void SendMail(IdentityUser identityUser, string requestLink, string subject)
        {
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("Hello " + identityUser.UserName + "<br /><br />");
            if (subject == "Email Confirmation Link")
            {
                mailBody.Append("Click on the link below to confirm your email:");

            }
            else if (subject == "Reset Password Link")
            {
                mailBody.Append("Click on the link below to reset your password:");

            }
            mailBody.Append("<br />");
            mailBody.Append(requestLink);
            mailBody.Append("<br /><br />Regards<br /><br />");
            mailBody.Append("Regards");
            mailBody.Append("<br /><br />");
            mailBody.Append("Customer Support.");
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = mailBody.ToString();
            MailboxAddress fromAddress = new MailboxAddress("Customer Support", "<Use your Email Id here>");
            MailboxAddress toAddress = new MailboxAddress(identityUser.UserName, identityUser.Email);
            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(fromAddress);
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = bodyBuilder.ToMessageBody();
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 465, true);
            smtpClient.Authenticate("shuklavarunakar@gmail.com ","shukla@95");
            smtpClient.Send(mailMessage);
            #endregion


        }
    }

}