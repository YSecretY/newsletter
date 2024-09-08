using FluentResults;
using MediatR;
using Newsletter.Articles.Application.Articles.CQRS.Commands.Create;

namespace Newsletter.Articles.Application.Articles.CQRS.Commands.Update;

public sealed record UpdateArticleCommand(
    string Id,
    CreateArticleCommand CreateArticleCommand,
    DateTime CreatedAt
) : IRequest<Result>;