using System;
using ReactiveUI;
using Avalonia.Threading;
using Microsoft.EntityFrameworkCore;
namespace ArithmeticGunner.ViewModels
{

    public class UserViewModel : ViewModelBase
    {

        private readonly DbContextOptions<UserContext> _options;

        private IUserRepository UserRepository;

        public UserViewModel(DbContextOptions<UserContext> opt = null)
        {
            if (opt == null)
            {
            _options = new DbContextOptionsBuilder<UserContext>()
                    .UseSqlite("DataSource=UserStatictics.db").Options;
            }
            UserRepository = new UserContext(_options);
        }

        public string Login {get; set;} = "";

        public void Save(int level)
        {
            UserRepository.SaveResult(Login,level);
        }


        public void OpenStatistics()
        {

        }
    }
}