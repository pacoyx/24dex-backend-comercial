public record LoginUserDto(
    string Username,
    string Password);

public record LoginUserResponseDto(
    string Token,
    string Name,
    string UserName,
    string Role,
    string Status,
    int userId,
    List<BranchSalesResponseDto> BranchSales
);

public record BranchSalesResponseDto(
     int Id,
     int BranchSalesId,
     string BranchSalesName,
     string Status
     );