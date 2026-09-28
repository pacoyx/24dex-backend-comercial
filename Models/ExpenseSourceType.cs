using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExpenseSourceType
{
    Cash = 1,
    BankTransfer = 2,
    DebitCard = 3,
    CreditCard = 4,
    CashBox = 5,
    Other = 6
}
