﻿using Application.Contracts.Presentation.CurrentUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Http;

namespace WebApi.Common.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    private ICurrentUserService _currentUser;

    private protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    private protected ICurrentUserService CurrentUser => _currentUser ??= HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();

    private protected ActionResult<JsonResponse<TData>> OkJsonResponse<TData>(TData data) => Ok(JsonResponse<TData>.Success(data));
}