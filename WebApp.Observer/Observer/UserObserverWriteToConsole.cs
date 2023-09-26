using BaseProject.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverWriteToConsole : IUserObserver
    {
        //DI Container'dan istediğimiz servisi alacağız çünkü ben program.cs'de IUserObserver gördüğünde şu sınıfı türet demiyeceğim
        
        //Artık istediğim sınıfı kullanabilirim.
        private readonly IServiceProvider _serviceProvider;

        public UserObserverWriteToConsole(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverWriteToConsole>>();
            logger.LogInformation($"user created : Id={appUser.Id}");
        }
    }
}
