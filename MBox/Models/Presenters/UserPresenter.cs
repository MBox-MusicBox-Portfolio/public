using MBox.Models.Db;

namespace MBox.Models.Presenters
{
    public class UserPresenter
    {
        public UserPresenter(User user)
        {
            Id = user.Id;
            Avatar = user.Avatar;
            Name = user.Name;
            Email = user.Email;
            Birthday = user.Birthday;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Avatar { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
