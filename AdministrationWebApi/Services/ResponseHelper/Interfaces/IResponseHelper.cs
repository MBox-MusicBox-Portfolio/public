using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Services.ResponseHelper.Interfaces
{
    public interface IResponseHelper
    {
        ActionResult<ResponsePresenter> Ok();
        ActionResult<ResponsePresenter> Ok(object obj);
        ActionResult<ResponsePresenter> Ok(IEnumerable<object> objects);
        ActionResult<ResponsePresenter> Created(object obj);
        ActionResult<ResponsePresenter> NotFound();
        ActionResult<ResponsePresenter> NotFound(string error, string key = "Server");
        ActionResult<ResponsePresenter> BadRequest();
        ActionResult<ResponsePresenter> BadRequest(string error, string key = "Server");
        ActionResult<ResponsePresenter> Unauthorized();
        ActionResult<ResponsePresenter> Forbidden();
        ActionResult<ResponsePresenter> UnprocessableEntity();
        ActionResult<ResponsePresenter> InternalServerError(Exception ex);
        ActionResult<ResponsePresenter> NoContent();
        void AddError(string v1, string v2);
        bool IsError();
        void AddValue(object obj);
        void AddValue(IEnumerable<object> objects);
        void AddError(object response);
        public ActionResult<ResponsePresenter> HandleError(Exception ex);
    }
}
