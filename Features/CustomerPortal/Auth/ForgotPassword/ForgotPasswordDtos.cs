public record ForgotPasswordRequest(string UserName);

public record ForgotPasswordResponse(string ResetUrl);
