public interface IGetExtraHoursService
{
    Task<IEnumerable<ExtraHourListItemDto>> GetExtraHoursAsync(long? employeeId, bool? approved, DateOnly? startDate, DateOnly? endDate);
}
