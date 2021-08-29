using Bogus;
using System.Collections.Generic;
using System.Linq;
using static Bogus.DataSets.Name;

namespace Modern.NFT.Fake
{
    public class FakeUserGenerator
    {
        /// <summary>
        /// Generate Random Users
        /// </summary>
        /// <param name="max">Max</param>
        /// <param name="gender">Gender</param>
        /// <returns>Collection of Users</returns>
        public static List<UserEntity> GenerateRandomUsers(int max,
            Gender gender)
        {
            List<UserEntity> users = new List<UserEntity>();

            do
            {
                var tempUser = Generate(gender);
                var userFound = (from user in users
                                 where user.FullName == tempUser.FullName
                                 select user).FirstOrDefault();
                if (userFound == null)
                    users.Add(tempUser);
            }
            while (users.Count < max);

            return users;
        }

        public static UserEntity Generate(Gender gender)
        {
            var testUsers = new Faker<UserEntity>()
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(gender))
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
                .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName);

            var user = testUsers.Generate();
            return user;
        }
    }
}
