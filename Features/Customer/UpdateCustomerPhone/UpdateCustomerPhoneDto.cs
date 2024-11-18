public record UpdateCustomerPhoneRequestDto
{
    public int Id { get; init; }

    required public string Phone { get; init; }
}