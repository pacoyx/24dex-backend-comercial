public static class PickupGuideRevisionsEndpoints
{
    internal static PickupGuideDetailRevisionResponse ToDetailResponse(PickupGuideDetailRevision detailRevision) => new(
        detailRevision.Id, detailRevision.PickupGuideDetailId, detailRevision.PickupGuideDetail.ServiceItem.Name,
        detailRevision.OriginalQuantity, detailRevision.NewQuantity, detailRevision.Reason
    );

    internal static PickupGuideRevisionResponse ToResponse(PickupGuideRevision revision) => new(
        revision.Id, revision.PickupGuideId, revision.EmployeeId,
        $"{revision.Employee.FirstName} {revision.Employee.LastName}".Trim(),
        revision.Observation, revision.RevisionDate,
        revision.DetailRevisions.Select(ToDetailResponse).ToList()
    );
}
