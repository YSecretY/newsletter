using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Articles.Application.Articles.Commands.Create;
using Newsletter.Shared.Presentation.Extensions;

namespace Newsletter.Articles.Api.Articles.Controllers;

[Route("api/v1/articles")]
public sealed class ArticlesController(ISender mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateArticleCommand command, CancellationToken cancellationToken) =>
        (await mediator.Send(command, cancellationToken)).ToActionResult();
}