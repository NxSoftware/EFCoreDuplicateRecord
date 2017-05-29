using API.Persistence;

namespace API.Users
{
    public interface IUserRepository
    {
        User CreateOrGetUser(string name);
    }

    public class UserRepository : IUserRepository
    {
        readonly Database _db;

        public UserRepository(Database db)
        {
            _db = db;
        }

        public User CreateOrGetUser(string name)
        {
            var user = _db.Users.Find(name);
			if (user == null)
			{
                user = new User
				{
					Name = name
				};
				_db.Users.Add(user);
			}
			return user;
        }
    }
}
