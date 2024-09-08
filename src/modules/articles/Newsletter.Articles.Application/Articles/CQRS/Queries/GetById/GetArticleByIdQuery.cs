using FluentResults;
using MediatR;

namespace Newsletter.Articles.Application.Articles.CQRS.Queries.GetById;

public sealed record GetArticleByIdQuery(string ArticleId) : IRequest<Result<ArticleDto>>;