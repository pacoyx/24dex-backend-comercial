public interface ICreateUnitMeasurementService
{
    Task<CreateUnitMeasurementResponseDto> CreateUnitMeasurementAsync(CreateUnitMeasurementRequestDto request);
}