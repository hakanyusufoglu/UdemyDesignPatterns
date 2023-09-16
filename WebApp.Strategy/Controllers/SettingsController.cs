using BaseProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Settings settings = new();
            //Cookieden okunmaktadır.
            if (User.Claims.Where(x => x.Type == Settings.claimDatabaseType).FirstOrDefault() != null)
            {
                //Buradan 1 gelirse SQL Server, 2 gelirse mongoDb olacaktır.
                settings.DatabaseType = (DatabaseTypeEnum)int.Parse(User.Claims.First(x => x.Type == Settings.claimDatabaseType).Value);
            }
            else
            {

                settings.DatabaseType = settings.GetDefaultDatabaseType;
            }
            return View(settings);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var newClaim = new Claim(Settings.claimDatabaseType, databaseType.ToString());

            //dbdeki claim
            var claims = await _userManager.GetClaimsAsync(user);

            var hasDatabaseTypeClaim = claims.FirstOrDefault(x=>x.Type==Settings.claimDatabaseType);

            if(hasDatabaseTypeClaim!=null)
            {
                await _userManager.ReplaceClaimAsync(user,hasDatabaseTypeClaim,newClaim);
            }
            else
            {
                await _userManager.AddClaimAsync(user,newClaim);
            }
            //cookienin yenilenmesi için kullanıcıya hissettirmeden logout login yaptırıyorum

            await _signInManager.SignOutAsync();

            //Cookieye token kaydetmiş olabilir beni hatırla kısmını tutabiliriz tüm bu bilgiler autenticateasync içerisinden gelir
            var autenticateResult = await HttpContext.AuthenticateAsync();

            await _signInManager.SignInAsync(user, autenticateResult.Properties);

            return RedirectToAction(nameof(Index));
        }
    }
}
