using Wdata.Models;

namespace Wdata.Extensions;

internal static class ParserExts
{

    public static WebsitePost ToWebsitePost(this (string body, Dictionary<string, object> metadata) input)
    {
        return new WebsitePost
        {
            Body = input.body,

            Title = input.metadata.TryGetValue("Title", out var title) ? title.ToString() : null,

            Category = input.metadata.TryGetValue("Category", out var category) ? category.ToString() : null,

            Description = input.metadata.TryGetValue("Description", out var desc) ? desc.ToString() : null,

            Slug = input.metadata.TryGetValue("Slug", out var slug) ? slug.ToString() : "",

            Summary = input.metadata.TryGetValue("Summary", out var summary) ? summary.ToString() : null,

            Tags = input.metadata.TryGetValue("Tags", out var tags)
                ? ((IEnumerable<object>)tags).Select(tag => tag.ToString()).ToArray()
                : new string[] { },

            CoverImage = input.metadata.TryGetValue("CoverImage", out var coverImage) ? coverImage.ToString() : null,

            CreatedAt = input.metadata.TryGetValue("CreatedAt", out var createdAt)
                ? DateTime.Parse(createdAt.ToString() ?? string.Empty)
                : DateTime.MinValue,

            UpdatedAt = input.metadata.TryGetValue("UpdatedAt", out var updatedAt)
                ? DateTime.Parse(updatedAt.ToString() ?? string.Empty)
                : DateTime.MinValue
        };
    }
}