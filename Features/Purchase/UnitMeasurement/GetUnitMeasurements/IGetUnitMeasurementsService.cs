public interface IGetUnitMeasurementsService
{
    Task<IEnumerable<GetUnitMeasurementsResponseDto>> GetUnitMeasurementsAsync();
    Task<IEnumerable<GetUnitMeasurementsComboResponseDto>> GetUnitMeasurementsComboAsync();
}

