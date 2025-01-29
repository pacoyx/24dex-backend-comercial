public static class DeleteUserEndpoint
{
    public static void MapDeleteUser(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (int id, IDeleteUserService userService) =>
        {
            var user = await userService.GetUser(id);
            if (user == null)
            {
                var api = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "User not found",
                    StatusCode = 404,
                    Success = false
                };

                return Results.Ok(api);
            }

            await userService.Delete(user);
            var response = new ApiResponse<string>()
            {
                Data = "",
                Message = "User deleted successfully",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }
}