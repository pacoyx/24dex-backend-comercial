public static class InvoicePaymentRules
{
    public static string? Validate(Invoice invoice, decimal amount)
    {
        if (invoice.Status == InvoiceStatus.Cancelled)
        {
            return "No se pueden registrar pagos sobre una factura anulada.";
        }

        if (amount > invoice.Balance)
        {
            return "El monto del pago excede el saldo pendiente de la factura.";
        }

        return null;
    }

    public static void Apply(Invoice invoice, decimal amount)
    {
        invoice.AmountPaid += amount;
        invoice.Balance = invoice.Total - invoice.AmountPaid;
        invoice.Status = invoice.Balance <= 0
            ? InvoiceStatus.Paid
            : InvoiceStatus.PartiallyPaid;
    }
}
