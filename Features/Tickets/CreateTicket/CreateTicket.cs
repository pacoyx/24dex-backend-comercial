using MediatR;
using FluentValidation;
using AutoMapper;

public class CreateTicket
{
    public class Command : IRequest<Result>
    {
        public int ClothingWorkerId { get; set; }
         
        public IEnumerable<CreateTicketClothesDto> TicketClothes { get; set; } = new List<CreateTicketClothesDto>();
    }
    public record class Result(int Id);
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
            RuleFor(x => x.ClothingWorkerId).NotEmpty();
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
            var ticket = new Ticket
            {
                FechaEmision = DateTime.Now,
                ClothingWorkerId = request.ClothingWorkerId,
                Status = "ROPERIA POR LAVAR",
                UserRef = 1,
            };

            foreach (var ticketClothe in request.TicketClothes)
            {
                ticket.TicketClothes.Add(new TicketClothe
                {
                    Item = ticketClothe.Item,
                    ClothingItemId = ticketClothe.ClothingItemId,
                    CustomObservations = ticketClothe.CustomObservations,
                    Status = "ROPERIA POR LAVAR",
                    clothingObservations = [.. ticketClothe.Observations.Select(x => new ClothingObservations
                    {
                        TypeObservationId = x.TypeObservationId,
                        ObservationSectionId = x.ObservationSectionId,
                        Observations = x.Observations,
                        Status = "REVISION"
                    })]
                });
            }            

            await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();
            return await Task.FromResult(_mapper.Map<Result>(ticket));
        }
    }
}