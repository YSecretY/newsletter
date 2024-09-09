using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newsletter.Articles.Domain.Articles;
using Newsletter.Articles.Domain.Articles.ValueObjects.ArticleId;
using Newsletter.Articles.Domain.Articles.ValueObjects.Contents;
using Newsletter.Articles.Domain.Articles.ValueObjects.Descriptions;
using Newsletter.Articles.Domain.Articles.ValueObjects.Slugs;
using Newsletter.Articles.Domain.Articles.ValueObjects.Tags;
using Newsletter.Articles.Domain.Articles.ValueObjects.Titles;

namespace Newsletter.Articles.Infrastructure.Articles;

internal sealed class ArticlesConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(article => article.Id);

        builder.Property(article => article.Id)
            .HasConversion(
                articleId => articleId.Value,
                dbId => ArticleId.From(dbId)
            );

        builder.Property(article => article.Title)
            .HasConversion(
                title => title.Value,
                dbTitle => Title.New(dbTitle).Value
            )
            .HasMaxLength(Title.MaxLength);

        builder.Property(article => article.Description)
            .HasConversion(
                description => description.Value,
                dbDescription => Description.New(dbDescription).Value
            )
            .HasMaxLength(Description.MaxLength);

        builder.Property(article => article.Content)
            .HasConversion(
                content => content.Value,
                dbContent => Content.New(dbContent).Value
            )
            .HasMaxLength(Content.MaxLength);

        builder.Property(article => article.Slug)
            .HasConversion(
                slug => slug.Value,
                dbSlug => Slug.New(dbSlug).Value
            ).HasMaxLength(Slug.MaxLength);

        builder.Property(article => article.Tags)
            .HasConversion(
                tags => tags.Select(t => t.Value).ToArray(),
                dbTags => dbTags.Select(t => Tag.New(t).Value).ToList()
            )
            .HasColumnType($"character varying({Tag.MaxLength})[]")
            .Metadata
            .SetValueComparer(new ValueComparer<List<Tag>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));
        ;

        builder.Property(article => article.ViewsCount);

        builder.Property(article => article.CreatedAt);
    }
}