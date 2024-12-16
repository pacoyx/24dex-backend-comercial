
public record ResponseSearchCustomerDtoPaginator(
  int TotalCount,
  List<ResponseSearchCustomerDtoDetailpaginator> Customers
  );


public record ResponseSearchCustomerDtoDetailpaginator(
int Id,
string FirstName,
string? LastName,
string? Address,
string? Phone,
string? Email,
string? DocPersonal,
string? Status
);

public record ResponseSearchCustomerDto(
  int id,
  string codigo,
  string nombres,
  string apellidos,
  string telefono
);
