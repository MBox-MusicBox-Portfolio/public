using AdministrationWebApi.Models.Db;
using System.ComponentModel.DataAnnotations;

namespace AdministrationWebApi.Models.Presenter
{
    public class UserPresenter
    {
        public UserPresenter(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Birthday = user.Birthday;
            Role = user.Role != null ? new CommonPresenter(user.Role) : null;
            IsBlocked = user.IsBlocked;
            IsEmailVerify = user.IsEmailVerify;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public CommonPresenter? Role { get; set; }
        public bool IsEmailVerify { get; set; }
        public bool IsBlocked { get; set; }
    }
}
