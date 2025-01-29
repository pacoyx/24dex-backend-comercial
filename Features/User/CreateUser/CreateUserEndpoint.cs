public static class CreateUserEndpoint
{
    public static void MapCreateUser(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateUserDto userDto, ICreateUserService userService) =>
        {
            if (userDto == null)
            {
                return Results.BadRequest();
            }

            var existUser = await userService.ExistUser(userDto.UserName);
            if (existUser)
            {
                var api = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "User already exists",
                    StatusCode = 400,
                    Success = false
                };

                return Results.Ok(api);
            }
 
            var user = await userService.Create(userDto);
            var responseUser = new CreateUserResponseDto(user.Name, user.UserName, user.Role);
            var response = new ApiResponse<CreateUserResponseDto>()
            {
                Data = responseUser,
                Message = "User created successfully",
                StatusCode = 201,
                Success = true
            };
            return Results.Ok(response);
        }).RequireAuthorization();
    }
}