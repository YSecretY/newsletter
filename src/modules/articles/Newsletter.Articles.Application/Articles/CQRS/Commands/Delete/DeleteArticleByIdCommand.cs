using FluentResults;
using MediatR;

namespace Newsletter.Articles.Application.Articles.CQRS.Commands.Delete;

public sealed record DeleteArticleByIdCommand(string ArticleId) : IRequest<Result>;