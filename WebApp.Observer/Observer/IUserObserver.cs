using BaseProject.Models;

namespace WebApp.Observer.Observer
{
    public interface IUserObserver
    {
        void CreateUser(AppUser appUser);
    }
}
