public record LoginCustomerRequest(string UserName, string Password);

public record LoginCustomerResponse(string Token, string RefreshToken, CustomerMeResponse User);
