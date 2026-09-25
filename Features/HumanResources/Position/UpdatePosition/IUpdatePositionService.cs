public interface IUpdatePositionService
{
    Task<UpdatePositionResponseDto?> UpdatePositionAsync(int id, UpdatePositionRequestDto request);
}
