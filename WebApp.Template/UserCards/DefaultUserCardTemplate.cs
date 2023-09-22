namespace WebApp.Template.UserCards
{
    public class DefaultUserCardTemplate : UserCardTemplate
    {
        protected override string SetFooter()
        {
            return string.Empty;
        }

        protected override string SetPicture()
        {
            //Dinamik olarak kayıt olmayan kullanıcı ikonunu gösteren kısımdır.
            return $"<img class='card-img-top' src='/userpictures/defaultuserpicture.png'";
        }
    }
}
