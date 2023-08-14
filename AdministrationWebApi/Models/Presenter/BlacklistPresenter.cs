using AdministrationWebApi.Models.Db;
using System.ComponentModel.DataAnnotations;

namespace AdministrationWebApi.Models.Presenter
{
    public class BlacklistPresenter
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public UserPresenter User { get; set; }
        public UserPresenter Admin { get; set; }
        public string? Messqge { get; set; }
        public DateTime Created { get; set; }
        public BlacklistPresenter(BlacklistUser blacklist)
        {
            Id=blacklist.Id;
            User=new UserPresenter(blacklist.User);
            Admin = new UserPresenter(blacklist.Admin);
            Messqge = blacklist.Messqge;
            Created = blacklist.Created;
        }
    }
}
