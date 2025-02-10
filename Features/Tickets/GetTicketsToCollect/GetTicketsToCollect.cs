using MediatR;
using FluentValidation;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
public class GetTicketsToCollect
{

    public class Command : IRequest<Result>
    {
        public int UserRef { get; set; }
    }

    public record class Result(ApiResponse<IEnumerable<GetTicketsToCollectResponseDto>> response);

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Ticket, Result>();
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
            var clothingWorker = await _db.Tickets.Include(cw => cw.ClothingWorker)
                .AsNoTracking()
                .Where(cw => cw.UserRef == request.UserRef && cw.Status == "ROPERIA POR LAVAR")
                .Select(cw => new GetTicketsToCollectResponseDto
                {
                    Ticket = cw.Id,
                    ClothingWorker = cw.ClothingWorker!.Name + " " + cw.ClothingWorker.LastName
                }).ToListAsync();

            if (clothingWorker == null)
            {
                var responseVal = new ApiResponse<IEnumerable<GetTicketsToCollectResponseDto>>()
                {
                    Data = null!,
                    Message = "Tickets not found",
                    StatusCode = 200,
                    Success = false
                };
                return new Result(responseVal);
            }

            // return _mapper.Map<Result>(clothingWorker);
            var responseOk = new ApiResponse<IEnumerable<GetTicketsToCollectResponseDto>>()
            {
                Data = clothingWorker,
                Message = "Tickets found",
                StatusCode = 200,
                Success = false
            };
            return new Result(responseOk);
        }
    }

}
