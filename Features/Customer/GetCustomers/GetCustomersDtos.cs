public record GetCustomersResponseDto(
    int TotalCount,
    List<GetCustomersResponseDatosDto> Customers
    );

public record GetCustomersResponseDatosDto(
int Id,
string FirstName,
string? LastName,
string? Address,
string? Phone,
string? Email,
string? DocPersonal,
string? Status
);