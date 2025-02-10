
public record CreateTicketClothesDto(
     int Item,
     int ClothingItemId,
     string CustomObservations,
     IEnumerable<CreateTicketObservationsDto> Observations
);
public record CreateTicketObservationsDto(
    int TypeObservationId,
    int ObservationSectionId,
    string Observations    
);