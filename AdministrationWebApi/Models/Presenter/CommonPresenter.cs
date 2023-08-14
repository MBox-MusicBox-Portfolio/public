using AdministrationWebApi.Models.Db;
using System.ComponentModel.DataAnnotations;

namespace AdministrationWebApi.Models.Presenter
{
    public class CommonPresenter
    {
        public Guid Id { get; set; } = Guid.NewGuid();     
        public string? Name { get; set; }
        public CommonPresenter(StatusApplications app)
        {
            Id=app.Id;
            Name=app.Name;
        }
        public CommonPresenter(Role role)
        {
            Id = role.Id;
            Name = role.Name;
        }
    }
}
