using MediatR;
using FluentValidation;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class GetClothingItems
{

    public class Command : IRequest<Result>
    {
        public int UserRef { get; set; }
    }

    public record class Result(ApiResponse<IEnumerable<GetClothingItemResponseDto>> response);

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ClothingItem, Result>();
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserRef).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly RecepcionDbContext _db;
        private readonly IMapper _mapper;

        public Handler(RecepcionDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var clothingItems = await _db.ClothingItems
                .AsNoTracking()
                .Where(ci => ci.UserRef == request.UserRef)
                .ToListAsync(cancellationToken);

            if (clothingItems == null)
            {
                var responseErr = new ApiResponse<IEnumerable<GetClothingItemResponseDto>>()
                {
                    Data = null!,
                    Message = "clothingItems not found",
                    StatusCode = 200,
                    Success = false
                };
                return new Result(responseErr);
            }

            var responseDto = clothingItems.Select(ci => new GetClothingItemResponseDto
            {
                Id = ci.Id,
                Description = ci.Description
            }).ToList();

            // return _mapper.Map<Result>(clothingWorker);
            var responseOk = new ApiResponse<IEnumerable<GetClothingItemResponseDto>>()
            {
                Data = responseDto,
                Message = "ClothingItems found",
                StatusCode = 200,
                Success = true
            };
            return new Result(responseOk);
        }
    }


}