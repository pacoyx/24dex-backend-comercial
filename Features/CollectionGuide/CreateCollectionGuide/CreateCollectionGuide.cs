using MediatR;
using FluentValidation;
using AutoMapper;

public class CreateCollectionGuide
{
    public class Command : IRequest<Result>
    {
        public string Carrier { get; set; } = "";
        public string Observations { get; set; } = "";
        public int UserRef { get; set; }
    }

    public record class Result(int Id);

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CollectionGuide, Result>();
        }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Carrier).NotEmpty();
            RuleFor(x => x.UserRef).NotEmpty();
            RuleFor(x => x.UserRef).GreaterThan(0);
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
            var collectionGuide = new CollectionGuide
            {
                FechaEmision = DateTime.Now,
                Carrier = request.Carrier,
                Observations = request.Observations,
                LaundryObservations = "",
                Status = "A",
                UserRef = request.UserRef,
            };

            var ticketsPendientes = _db.Tickets.Where(x => x.Status == "ROPERIA POR LAVAR").ToList();

            foreach (var ticket in ticketsPendientes)
            {
                collectionGuide.CollectionGuideTickets.Add(new CollectionGuideTicket
                {
                    TicketId = ticket.Id,
                    Status = "A",
                });
            }

            //los tickets pendientes cambiar su estado a ''ENVIADO A LAVANDERIA''
            foreach (var ticket in ticketsPendientes)
            {
                ticket.Status = "ENVIADO A LAVANDERIA";
            }

            await _db.CollectionGuides.AddAsync(collectionGuide);
            await _db.SaveChangesAsync(cancellationToken);

            return new Result(collectionGuide.Id);
        }
    }
}