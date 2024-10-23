
using Microsoft.EntityFrameworkCore;

public static class LoginUserEndpoint
{
    public static void MapLoginUser(this IEndpointRouteBuilder app)
    {

        app.MapPost("/", async (LoginUserDto loginDto, RecepcionDbContext context, JwtService jwtService) =>
        {
            var user = await context.Users
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.UserName == loginDto.Username && u.Password == loginDto.Password);

                Console.WriteLine("User: " + user);
            if (user == null)
            {                
                return Results.Unauthorized();
            }

            var branchSalesUser = await context.BrachSalesUsers
                                            .Include(bsu => bsu.BranchSales)
                                            .AsNoTracking()
                                            .Where(bsu => bsu.UserId == user.Id && bsu.Status == "A")
                                            .ToListAsync();

            if (branchSalesUser == null)
            {
                
                return Results.Problem("User does not have any branch sales assigned" , statusCode: 401);
            }


            var token = jwtService.GenerateToken(loginDto.Username, loginDto.Password);
            var loginUserResponseDto = new LoginUserResponseDto(token, user.Name, user.UserName, user.Role, user.Status, user.Id,

            branchSalesUser.Select(bsu => new BranchSalesResponseDto(bsu.Id, bsu.BranchSalesId, bsu.BranchSales!.Description, bsu.Status)).ToList()

            );

            var response = new ApiResponse<LoginUserResponseDto>()
            {
                Success = true,
                Message = "Request was successful",
                Data = loginUserResponseDto,
                StatusCode = 200
            };

            return Results.Ok(response);
        }).AllowAnonymous();
    }
}