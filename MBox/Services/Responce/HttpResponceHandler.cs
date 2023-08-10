using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MBox.Services.Responce;

public class HttpResponceHandler : IHttpResponseHandler
{
    private readonly ResponsePresenter _response;

    public HttpResponceHandler()
    {
        _response = new ResponsePresenter();
    }

    private object CreateObject(string errorCode, string errorMessage)
    {
        return new
        {
            ErrorCode = errorCode,
            ErrorMessage = errorMessage
        };
    }

    public void AddError(string errorCode, string errorMessage)
    {
        _response.Errors.Add(CreateObject(errorCode, errorMessage));
    }

    public void AddError(object responseData)
    {
        throw new NotImplementedException();
    }

    public void AddValue(object data)
    {
        throw new NotImplementedException();
    }

    public void AddValue(IEnumerable<object> collection)
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> BadRequest()
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> BadRequestWithMessage(string errorMessage, string key = "Server")
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> Created()
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> Forbidden()
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> HandleError(Exception ex)
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> InternalServerError()
    {
        throw new NotImplementedException();
    }

    public bool IsError()
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> NoContent()
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> NotFound()
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> NotFoundWithMessage(string errorMessage, string key = "Server")
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> Succes()
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> Unauthorized()
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> UnprocessableEntity()
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> Succes(object data)
    {
        throw new NotImplementedException();
    }

    public ActionResult<ResponsePresenter> Created(object data)
    {
        throw new NotImplementedException();
    }
}