using Domain.Dto;
using Domain.Entities;

namespace Domain.Repositories;

public interface IGalleryRepository
{
    Task<GalleryEntity?> GetGalleryAsync(string userId, string itemId, CancellationToken cancellationToken = default);
    Task<bool> SaveGalleryAsync(GalleryEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteGalleryAsync(string userId, string itemId, CancellationToken cancellationToken = default);
    Task<(List<GalleryEntity>, string)> GetGalleryPagedAsync(string userId, int? limit, string? nextToken, CancellationToken cancellationToken);
    Task<List<GalleryEntity>> GetBatchGalleryAsync(Dictionary<string,string> ids, CancellationToken cancellationToken);
}