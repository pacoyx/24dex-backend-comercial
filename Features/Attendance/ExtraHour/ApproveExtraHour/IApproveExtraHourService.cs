public interface IApproveExtraHourService
{
    Task<ExtraHourResponseDto?> ApproveExtraHourAsync(long id);
}
