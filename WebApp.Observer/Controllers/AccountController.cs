using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Observer.Models;
using WebApp.Observer.Observer;

namespace BaseProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserObserverSubject _userObserverSubject;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, UserObserverSubject userObserverSubject)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userObserverSubject = userObserverSubject;
        }

        public IActionResult Login()
        {
            return View();
        }
        //Login actiondaki parametreleri sınıf olarakda verebiliriz örneğin User classı oluşturup içerisinde email ve string bulundurabiliriz.
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hasUser = await _userManager.FindByEmailAsync(email);
            if (hasUser == null) return View();

            //Login olma işlemi
            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, password, true, false);

            if (!signInResult.Succeeded) return View();
            //Neden nameof kullanıldı? çünkü Index ismi değiştiğinde compile zamanında hata alabilmek için
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
        public async Task<IActionResult> Logout()
        {
            //Cookie'yi siler yani çıkış yapar.
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateViewModel userCreateViewModel)
        {
            var appUser = new AppUser() { UserName=userCreateViewModel.UserName,Email=userCreateViewModel.Email };

            //IdentityResult olarak kullanıcı girişi başarılı mı başarısız mı bilgisi döner
            var identityResult = await _userManager.CreateAsync(appUser, userCreateViewModel.Password);

            if (identityResult.Succeeded)
            {
                //Kullanıcının kayıt oldugunu bildiriyoruz.
                _userObserverSubject.NotifyObservers(appUser);

                ViewBag.message = "Üyelik işlemi başarıyla gerçekleştirildi";
            }
            else
            {
                ViewBag.message = identityResult.Errors.ToList().First().Description;
            }
            return View();
        }
    }
}
