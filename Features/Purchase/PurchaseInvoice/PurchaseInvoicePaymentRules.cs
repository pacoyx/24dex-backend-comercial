public static class PurchaseInvoicePaymentRules
{
    public static string? Validate(PurchaseInvoice invoice, decimal amount)
    {
        if (invoice.PaymentStatus == PurchaseInvoiceStatus.Cancelled)
        {
            return "No se pueden registrar pagos sobre una compra anulada.";
        }

        if (amount > invoice.Balance)
        {
            return "El monto del pago excede el saldo pendiente de la compra.";
        }

        return null;
    }

    public static void Apply(PurchaseInvoice invoice, decimal amount)
    {
        invoice.AmountPaid += amount;
        invoice.Balance = invoice.Total - invoice.AmountPaid;
        invoice.PaymentStatus = invoice.Balance <= 0
            ? PurchaseInvoiceStatus.Paid
            : PurchaseInvoiceStatus.PartiallyPaid;
    }
}
