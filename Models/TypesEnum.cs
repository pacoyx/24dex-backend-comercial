public enum TipoPago
{
    Efectivo,
    Tarjeta,
    Transferencia,
    Deposito,
    Qr,
    Otros
}
public enum TipoRecepcion
{
    Recepcion,
    Delivery
}

public enum PrintStatus
{
    Pending = 1,
    Processing = 2,
    Printed = 3,
    Error = 4,
    Cancelled = 5
}

public enum InvoiceStatus
{
    Draft = 1,
    Issued = 2,
    PartiallyPaid = 3,
    Paid = 4,
    Cancelled = 5
}

public enum CollectionSheetStatus
{
    Open = 1,
    Closed = 2,
    Cancelled = 3
}