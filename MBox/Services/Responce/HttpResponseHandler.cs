using MBox.Models.Exceptions;
using MBox.Models.Presenters;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MBox.Services.Responce;

public class HttpResponseHandler : IHttpResponseHandler
{
    private readonly ResponsePresenter _response;

    public HttpResponseHandler()
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
        _response.Errors.Add(responseData);
    }

    public void AddValue(object data)
    {
        _response.Value.Add(data);
    }

    public void AddValue(IEnumerable<object> collection)
    {
        _response.Value.AddRange(collection);
    }

    public ActionResult<ResponsePresenter> BadRequest()
    {
        _response.Status = 400;
        _response.Success = false;
        return new BadRequestObjectResult(_response);
    }

    public ActionResult<ResponsePresenter> BadRequestWithMessage(string errorMessage, string key = "Server")
    {
        _response.Status = 400;
        _response.Success = false;
        AddError(errorMessage, key);
        return new BadRequestObjectResult(_response);
    }

    public ActionResult<ResponsePresenter> Forbidden()
    {
        return new ForbidResult();
    }

    public ActionResult<ResponsePresenter> HandleError(Exception ex)
    {
        if (ex is NotFoundException notFoundEx)
        {
            AddError(notFoundEx.Response);
            return NotFound();
        }
        else if (ex is BadRequestException badRequestEx)
        {
            AddError(badRequestEx.Response);
            return BadRequest();
        }
        else
        {
            return InternalServerError(ex);
        }
    }

    public ActionResult<ResponsePresenter> InternalServerError(Exception ex)
    {
        _response.Status = 500;
        _response.Success = false;
        AddError($"An error occurred: ${ex.Message}", "Serve");
        return new ObjectResult(_response)
        {
            StatusCode = 500,
        };
    }

    public bool IsError()
    {
        return _response.Errors.Count > 0 ? true : false;
    }

    public ActionResult<ResponsePresenter> NoContent()
    {
        return new NoContentResult();
    }

    public ActionResult<ResponsePresenter> NotFound()
    {
        _response.Status = 404;
        _response.Success = false;
        return new NotFoundObjectResult(_response);
    }

    public ActionResult<ResponsePresenter> NotFoundWithMessage(string errorMessage, string key = "Server")
    {
        _response.Status = 404;
        _response.Success = false;
        AddError(errorMessage, key);
        return new NotFoundObjectResult(_response);
    }

    public ActionResult<ResponsePresenter> Succes()
    {
        _response.Status = 200;
        _response.Success = true;
        return new OkObjectResult(_response);
    }

    public ActionResult<ResponsePresenter> Unauthorized()
    {
        _response.Status = 401;
        _response.Success = false;
        return new UnauthorizedObjectResult(_response);
    }

    public ActionResult<ResponsePresenter> UnprocessableEntity()
    {
        _response.Status = 422;
        _response.Success = false;
        return new UnprocessableEntityObjectResult(_response);
    }

    public ActionResult<ResponsePresenter> Succes(object data)
    {
        _response.Status = 200;
        _response.Success = true;
        AddValue(data);
        return new OkObjectResult(_response);
    }

    public ActionResult<ResponsePresenter> Created(object data)
    {
        _response.Status = 201;
        _response.Success = true;
        AddValue(data);
        return new ObjectResult(_response)
        {
            StatusCode = 201,
        };
    }
}