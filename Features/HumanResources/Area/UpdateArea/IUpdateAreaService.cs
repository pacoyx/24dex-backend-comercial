public interface IUpdateAreaService
{
    Task<UpdateAreaResponseDto?> UpdateAreaAsync(int id, UpdateAreaRequestDto request);
}
