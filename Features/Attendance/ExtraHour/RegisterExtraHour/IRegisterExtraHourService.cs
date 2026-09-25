public interface IRegisterExtraHourService
{
    Task<ExtraHourResponseDto?> RegisterExtraHourAsync(long employeeId, RegisterExtraHourRequestDto request);
}
