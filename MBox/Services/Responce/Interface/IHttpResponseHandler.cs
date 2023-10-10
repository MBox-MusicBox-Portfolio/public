using MBox.Models.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Services.Responce.Interface;

public interface IHttpResponseHandler
{
    // Successful Responses
    ActionResult<ResponsePresenter> Succes();
    ActionResult<ResponsePresenter> Succes(object data);
    ActionResult<ResponsePresenter> Created(object data);

    // Client Errors
    ActionResult<ResponsePresenter> NotFound();
    ActionResult<ResponsePresenter> NotFoundWithMessage(string errorMessage, string key = "Server");
    ActionResult<ResponsePresenter> BadRequest();
    ActionResult<ResponsePresenter> BadRequestWithMessage(string errorMessage, string key = "Server");
    ActionResult<ResponsePresenter> Unauthorized();
    ActionResult<ResponsePresenter> Forbidden();
    ActionResult<ResponsePresenter> UnprocessableEntity();
    ActionResult<ResponsePresenter> NoContent();

    // Server Errors
    ActionResult<ResponsePresenter> InternalServerError(Exception ex);

    // Utility Methods
    void AddError(string errorCode, string errorMessage);
    bool IsError();
    void AddValue(object data);
    void AddValue(IEnumerable<object> collection);
    void AddError(object responseData);

    // Error Handling
    public ActionResult<ResponsePresenter> HandleError(Exception ex);
}