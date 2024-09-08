using FluentResults;
using MediatR;

namespace Newsletter.Articles.Application.Articles.CQRS.Commands.Create;

public sealed record CreateArticleCommand(
    string Title,
    string Description,
    string Content,
    List<string>? Tags,
    string Slug
) : IRequest<Result>;