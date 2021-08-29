using static Bogus.DataSets.Name;

namespace Modern.NFT.Fake
{
    public class UserEntity
    {
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
    }
}
