using BaseProject.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverSubject
    {
        private readonly List<IUserObserver> _userObservers;

        public UserObserverSubject()
        {
            _userObservers = new List<IUserObserver>();
        }
        //Örneğin kayıt eklendikten sonra gerçekleştirilecek adımların (konsola yazdırma, indirim kuponu verme ve mail gönderme vb.) observerların eklendiği metot
        public void RegisterObserver(IUserObserver observer)
        {
            _userObservers.Add(observer);
        }
        //Örneğin kayıt eklendikten sonra gerçekleştirilecek adımların (konsola yazdırma, indirim kuponu verme ve mail gönderme vb.) observerların silindiği metot
        public void RemoveObserver(IUserObserver observer)
        {
            _userObservers.Remove(observer);
        }
        //Bilgilendirecek olan metot
        public void NotifyObservers(AppUser appUser)
        {
            _userObservers.ForEach(x =>
            {
                //Sırayla observerdaki örneğin  (konsola yazdırma, indirim kuponu verme ve mail gönderme vb.) metotları çalıştırmayı sağlayacaktır. Bir nevi bilgilendirecektir.
                x.UserCreated(appUser);
            });
        }

    }
}
