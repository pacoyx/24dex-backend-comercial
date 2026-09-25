public interface ICreateAreaService
{
    Task<CreateAreaResponseDto> CreateAreaAsync(CreateAreaRequestDto request);
}
