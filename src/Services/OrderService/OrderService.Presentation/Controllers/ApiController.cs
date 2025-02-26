﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Shared;

namespace OrderService.Presentation.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly ISender _sender;

        protected ApiController(ISender sender) => _sender = sender;

        protected IActionResult HandleFailure(Result result) =>
            result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),
                IValidationResult validationResult =>
                    BadRequest(
                        CreateProblemDetails(
                            "Validation Error", StatusCodes.Status400BadRequest,
                            result.Error,
                            validationResult.Errors)),
                _ =>
                    BadRequest(
                        CreateProblemDetails(
                            "Bad Request",
                            StatusCodes.Status400BadRequest,
                            result.Error))
            };


        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            Error error,
            Error[]? errors = null) =>
            new ProblemDetails
            {
                Title = title,
                Status = status,
                Detail = error.Message,
                Extensions =
                {
                    ["errors"] = errors
                }
            };
    }
}
