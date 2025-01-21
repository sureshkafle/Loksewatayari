using System.Net;

namespace WebApi.EndPoints.Responses;

public class  ApiResultModel<T>
{
    public T? Response {get;set;}
    public bool Success {get;set;}  = false;
    public List<string> Errors {get;set;}=new();
    public HttpStatusCode HttpStatusCode {get;set;} = HttpStatusCode.BadRequest;

     internal void SuccessResult(T value)
     {
         HttpStatusCode = HttpStatusCode.OK;    
         Success = true;
         Response = value;

     }
}