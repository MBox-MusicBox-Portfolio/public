using AdministrationWebApi.Models.Exceptions;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Services.ResponseHelper
{
    public class ResponseHelper : IResponseHelper
    {
        public ResponseHelper()
        {
            _response = new ResponsePresenter();
        }
        private readonly ResponsePresenter _response;
        public ActionResult<ResponsePresenter> Ok()
        {
            _response.Status = 200;
            _response.Success = true;
            return new OkObjectResult(_response);
        }
        public ActionResult<ResponsePresenter> Ok(object obj)
        {
            _response.Status = 200;
            _response.Success = true;
            AddValue(obj);
            return new OkObjectResult(_response);
        }
        public ActionResult<ResponsePresenter> Ok(IEnumerable<object> objects)
        {
            _response.Status = 200;
            _response.Success = true;
            AddValue(objects);
            return new OkObjectResult(_response);
        }
        public ActionResult<ResponsePresenter> Created(object obj)
        {
            _response.Status = 201;
            _response.Success = true;
            AddValue(obj);
            return new ObjectResult(_response)
            {
                StatusCode = 201,
            };
        }

        public ActionResult<ResponsePresenter> NotFound()
        {
            _response.Status = 404;
            _response.Success = false;
            return new NotFoundObjectResult(_response);
        }
        public ActionResult<ResponsePresenter> NotFound(string error, string key = "Server")
        {
            _response.Status = 404;
            _response.Success = false;
            AddError(error, key);
            return new NotFoundObjectResult(_response);
        }

        public ActionResult<ResponsePresenter> BadRequest()
        {
            _response.Status = 400;
            _response.Success = false;
            return new BadRequestObjectResult(_response);
        }
        public ActionResult<ResponsePresenter> BadRequest(string error, string key = "Server")
        {
            _response.Status = 400;
            _response.Success = false;
            AddError(error, key);
            return new BadRequestObjectResult(_response);
        }

        public ActionResult<ResponsePresenter> Unauthorized()
        {
            _response.Status = 401;
            _response.Success = false;
            return new UnauthorizedObjectResult(_response);
        }

        public ActionResult<ResponsePresenter> Forbidden()
        {
            return new ForbidResult();
        }

        public ActionResult<ResponsePresenter> UnprocessableEntity()
        {
            _response.Status = 422;
            _response.Success = false;
            return new UnprocessableEntityObjectResult(_response);
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
        public ActionResult<ResponsePresenter> NoContent()
        {
            return new NoContentResult();
        }
        public static object CreateObjectError(string key, string value)
        {
            return new
            {
                Key = key,
                Value = value
            };
        }
        public void AddError(string error, string key)
        {
            _response.Errors.Add(ResponseHelper.CreateObjectError(key, error));
        }
        public void AddError(object obj)
        {
            _response.Errors.Add(obj);
        }
        public void AddValue(IEnumerable<object> objects)
        {
            _response.Value.AddRange(objects);
        }
        public void AddValue(object obj)
        {
            _response.Value.Add(obj);
        }
        public bool IsError()
        {
            return _response.Errors.Count > 0 ? true : false;
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
    }
}
