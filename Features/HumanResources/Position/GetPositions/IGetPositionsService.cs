public interface IGetPositionsService
{
    Task<IEnumerable<GetPositionsResponseDto>> GetPositionsAsync(bool? onlyActive);
}
