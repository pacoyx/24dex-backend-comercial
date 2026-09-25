public interface IGetAreasService
{
    Task<IEnumerable<GetAreasResponseDto>> GetAreasAsync(bool? onlyActive);
}
