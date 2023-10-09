using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

namespace backend.ResultFilter
{
    public class MyApiResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

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
                if(errors.ContainsKey("AccountId"))
                    context.Result = new BadRequestObjectResult(new { description = "account_id missing or has incorrect type." });
                else
                    context.Result = new BadRequestObjectResult(new { description = "Mandatory body parameters missing or have incorrect type." });
            }
        }
    }
}
