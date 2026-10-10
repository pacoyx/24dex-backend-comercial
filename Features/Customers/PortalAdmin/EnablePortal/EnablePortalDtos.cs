public record EnablePortalRequest(string FullName, string Email);

public record EnablePortalResponse(int CustomerId, string CustomerName, bool PortalEnabled, string ActivationUrl);
