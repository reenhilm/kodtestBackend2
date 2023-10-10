using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;

namespace backend.ResultFilter
{
    public class MyApiResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        private bool IsJsonRequest(string contentType)
        {
            return contentType.ToLower().Contains("application/json");
        }

        private bool IsJsonRequest(HttpRequest request)
        {
            return request.ContentType.ToLower().Contains("application/json");
        }

        private bool IsPOSTOnTransactionsRequest(HttpRequest request)
        {
            return request.Method.ToUpper() == "POST" && request.Path.Value.ToLower() == "/transactions";
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var count = context.ModelState.Count;
                var errors = new Dictionary<String, String[]>(count);
                foreach (var keyModelStatePair in context.ModelState)
                {
                    var key = keyModelStatePair.Key;
                    var modelErrors = keyModelStatePair.Value.Errors;
                    if (modelErrors is not null && modelErrors.Count > 0)
                    {
                        var errorMessages = modelErrors.Select(error => error.ErrorMessage).ToArray();
                        errors.Add(key, errorMessages);
                    }
                }

                if (!IsJsonRequest(context.HttpContext.Request) && IsPOSTOnTransactionsRequest(context.HttpContext.Request))
                {
                    var result = new ObjectResult(new { description = "Specified content type not allowed." });
                    result.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    context.Result = result;
                }
                else if (errors.ContainsKey("AccountId"))                    
                    context.Result = new BadRequestObjectResult(new { description = "account_id missing or has incorrect type." });
                else
                    context.Result = new BadRequestObjectResult(new { description = "Mandatory body parameters missing or have incorrect type." });
            }
        }
    }
}
