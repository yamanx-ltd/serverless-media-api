using Domain.Entities;

namespace Domain.Dto;

public class GalleryDto
{
    public string UserId { get; set; } = default!;
    public string ItemId { get; set; } = default!;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<GalleryImageDto> Images { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public class GalleryImageDto
    {
        public string Id { get; set; } = default!;
        public string Url { get; set; } = default!;
        public int Rank { get; set; }
    }
}

public static class GalleryDtoMapper
{
    public static GalleryDto ToDto(this GalleryEntity entity)
    {
        return new GalleryDto
        {
            Description = entity.Description,
            Images = entity.Images.Select(q => new GalleryDto.GalleryImageDto
            {
                Id = q.Id,
                Rank = q.Rank,
                Url = q.Url
            }).ToList(),
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
            ItemId = entity.ItemId,
            UpdatedAt = entity.UpdatedAt,
            UserId = entity.UserId
        };
    }
}