public interface ICreatePositionService
{
    Task<CreatePositionResponseDto> CreatePositionAsync(CreatePositionRequestDto request);
}
