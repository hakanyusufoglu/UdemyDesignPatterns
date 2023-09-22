using WebApp.Template.Models;
using System.Text;

namespace WebApp.Template.UserCards
{
    //Nasıl Data geleceğini bilmiyor, soyutluyoruz. Algoritmanın kemiği, yapısını burası belirliyor. Resim alanı yuvarlak mı olacak vs hepsini burası belirliyor.
    public abstract class UserCardTemplate
    {
        protected AppUser AppUser { get; set; }
        public void SetUser(AppUser appUser)
        {
            AppUser = appUser;
        }

        //Algoritmanın tüm sırası burada belirlenir.
        public string Build()
        {
            if (AppUser == null) throw new ArgumentNullException(nameof(AppUser));

            var sb = new StringBuilder();
            sb.Append("<div class='card'>"); //birinci kart.
            //Algoritmanın 1. sırası
            sb.Append(SetPicture());
            sb.Append($@"<div class='card-body'><h5>{AppUser.UserName}</h5><p>{AppUser.Description}</p>");
            sb.Append(SetFooter());
            sb.Append("</div>");

            sb.Append("</div>");
            return sb.ToString(); //template hazırz
        }

        //Değişiklik gösteren kısımlar, alt sınıflarda kullanılabilsin diye protected tanımladık
        protected abstract string SetFooter();

        protected abstract string SetPicture();
    }
}
