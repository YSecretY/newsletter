using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Articles.Application.Articles;
using Newsletter.Articles.Application.Articles.CQRS.Commands.Create;
using Newsletter.Articles.Application.Articles.CQRS.Commands.Delete;
using Newsletter.Articles.Application.Articles.CQRS.Commands.Update;
using Newsletter.Articles.Application.Articles.CQRS.Queries.GetById;
using Newsletter.Shared.Presentation.Extensions;

namespace Newsletter.Articles.Api.Articles.Controllers;

[Route("api/v1/articles")]
public sealed class ArticlesController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleCommand command, CancellationToken cancellationToken) =>
        (await mediator.Send(command, cancellationToken)).ToActionResult();


    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleDto>> GetById([FromRoute] string id, CancellationToken cancellationToken)
    {
        GetArticleByIdQuery query = new(ArticleId: id);

        return (await mediator.Send(query, cancellationToken)).ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateArticleCommand command, CancellationToken cancellationToken) =>
        (await mediator.Send(command, cancellationToken)).ToActionResult();

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
    {
        DeleteArticleByIdCommand command = new(ArticleId: id);

        return (await mediator.Send(command, cancellationToken)).ToActionResult();
    }
}