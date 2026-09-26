public record MyProfileResponseDto(
    int Id,
    string Name,
    string UserName,
    string Email,
    string Phone,
    string Role,
    string Status,
    string PhotoUrl
);

public record UpdateMyProfileRequestDto(
    string Name,
    string Email,
    string Phone
);

public record UpdateMyPhotoRequestDto(
    string FileName,
    string BlobUrl
);

public record MyPhotoResponseDto(
    string PhotoUrl
);

public record CreateMyDocumentRequestDto(
    string DocumentName,
    string FileName,
    string BlobUrl,
    string? Notes
);

public record MyDocumentResponseDto(
    long Id,
    string DocumentName,
    string FileName,
    string BlobUrl,
    string? Notes,
    DateTime CreatedAt
);

public record ChangeMyPasswordRequestDto(
    string CurrentPassword,
    string NewPassword,
    string ConfirmNewPassword
);
