using MediatR;
using FluentValidation;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


public class GetClothingWorkerById
{

    public class Command : IRequest<Result>
    {
        public string IdWorker { get; set; } = "";
        public int UserRef { get; set; }
    }

    // public record class Result(int Id, string Name, string LastName, string DocumentNumber);
    public record class Result(ApiResponse<ClothingWorker> response);

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ClothingWorker, Result>();
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.IdWorker).NotEmpty();
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
            var clothingWorker = await _db.ClothingWorkers
                .AsNoTracking()
                .Where(cw => (cw.Id == int.Parse(request.IdWorker) || cw.DocumentNumber == request.IdWorker) && cw.UserRef == request.UserRef)
                .FirstOrDefaultAsync(cancellationToken);

            if (clothingWorker == null)
            {
                var responseVal = new ApiResponse<ClothingWorker>()
                {
                    Data = null!,
                    Message = "Worker not found",
                    StatusCode = 200,
                    Success = false
                };
                return new Result(responseVal);
            }

            // return _mapper.Map<Result>(clothingWorker);
            var responseOk = new ApiResponse<ClothingWorker>()
            {
                Data = clothingWorker,
                Message = "Worker found",
                StatusCode = 200,
                Success = true
            };
            return new Result(responseOk);
        }
    }


}