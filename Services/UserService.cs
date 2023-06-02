using System;
using dotnet_mvc.Data;

namespace dotnet_mvc.Services
{
	public class UserService
	{
        private DatabaseContext _db;
        public UserService(DatabaseContext db)
		{
            this._db = db;
        }

        public UserModel findByEmail(string email)
        {
            var users = this._db.Users.Where(x => x.Email == email).ToList();
            if(users.Count == 0)
            {
                throw new SystemException("User not found");
            }
            return users.First();
            
        }

        public void create(UserModel userModel)
        {
           this._db.Users.Add(userModel);
        }
	}
}

