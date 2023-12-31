﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace NSE.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object? result = null)
        {
            if (IsValid()) return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
                AddProcessingErrors(error.ErrorMessage);

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                AddProcessingErrors(error.ErrorMessage);

            return CustomResponse();
        }

        protected bool IsValid()
        {
            return !Errors.Any();
        }

        protected void AddProcessingErrors(string error)
        {
            Errors.Add(error);
        }

        protected void ClearProcessingErrors()
        {
            Errors.Clear();
        }
    }
}
