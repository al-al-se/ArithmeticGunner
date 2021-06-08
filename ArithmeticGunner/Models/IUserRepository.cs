using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository
{
    void SaveResult(string login, int level);

    IEnumerable<User> GetStatictics();
}