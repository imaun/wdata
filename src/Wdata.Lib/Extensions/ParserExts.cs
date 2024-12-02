using Wdata.Models;

namespace Wdata.Extensions;

internal static class ParserExts
{

    public static WebsitePost ToWebsitePost(this (string body, Dictionary<string, object> metadata) input)
    {
        var post = new WebsitePost
        {
            Body = input.body,
            Title = input.metadata.GetMetadataValue("title", out var title) ? title.ToString() : null,
            Category = input.metadata.GetMetadataValue("category", out var category) ? category.ToString() : null,
            Description = input.metadata.GetMetadataValue("description", out var desc) ? desc.ToString() : null,
            Slug = input.metadata.GetMetadataValue("slug", out var slug) ? slug.ToString() : "",
            Summary = input.metadata.GetMetadataValue("summary", out var summary) ? summary.ToString() : null,
            Author = input.metadata.GetMetadataValue("author", out var author) ? author.ToString() : null,
            PostType = input.metadata.GetMetadataValue("post_type", out var postType) ? postType.ToString() : null,
            Tags = input.metadata.GetMetadataValue("tags", out var tags)
                ? ((IEnumerable<object>)tags).Select(tag => tag.ToString()).ToArray()
                : [],
            CoverImage = input.metadata.GetMetadataValue("cover_image", out var coverImage) 
                ? coverImage.ToString() 
                : null,
            CreatedAt = input.metadata.TryGetValue("created_at", out var createdAt)
                ? DateTime.Parse(createdAt.ToString() ?? string.Empty)
                : DateTime.MinValue,
            UpdatedAt = input.metadata.TryGetValue("updated_at", out var updatedAt)
                ? DateTime.Parse(updatedAt.ToString() ?? string.Empty)
                : DateTime.MinValue,
            Ref = input.metadata.TryGetValue("ref", out var @ref) ? @ref.ToString() : null,
        };

        return post;
    }


    private static bool GetMetadataValue(this Dictionary<string, object> metadata, string key, out object value)
    {
        value = metadata.FirstOrDefault(v=> v.Key.Equals(key, StringComparison.OrdinalIgnoreCase)).Value;
        return value != default;
    }
}