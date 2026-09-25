public interface IGetLoansService
{
    Task<IEnumerable<LoanListItemDto>> GetLoansAsync(long? employeeId, LoanType? loanType, bool? isClosed);
}
