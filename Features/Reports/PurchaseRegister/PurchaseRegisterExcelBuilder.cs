using ClosedXML.Excel;

public static class PurchaseRegisterExcelBuilder
{
    private static readonly string[] Headers =
    {
        "Fecha", "Proveedor", "RUC", "Documento", "SubTotal", "IGV", "Total",
        "Pagado", "Saldo", "Moneda", "Tipo Cambio", "Estado"
    };

    public static byte[] Build(List<PurchaseRegisterRowDto> rows, PurchaseRegisterTotalsDto totals)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("RegistroCompras");

        WriteHeader(sheet);
        var lastDataRow = WriteRows(sheet, rows);
        WriteTotals(sheet, totals, lastDataRow);

        sheet.SheetView.FreezeRows(1);
        sheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    private static void WriteHeader(IXLWorksheet sheet)
    {
        for (var i = 0; i < Headers.Length; i++)
        {
            var cell = sheet.Cell(1, i + 1);
            cell.Value = Headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#BDD7EE");
        }
    }

    private static int WriteRows(IXLWorksheet sheet, List<PurchaseRegisterRowDto> rows)
    {
        var currentRow = 2;

        foreach (var row in rows)
        {
            sheet.Cell(currentRow, 1).Value = row.IssueDate;
            sheet.Cell(currentRow, 1).Style.DateFormat.Format = "dd/MM/yyyy";

            sheet.Cell(currentRow, 2).Value = row.SupplierName;
            sheet.Cell(currentRow, 3).Value = row.SupplierRuc;
            sheet.Cell(currentRow, 4).Value = row.SerieNumero;

            sheet.Cell(currentRow, 5).Value = row.SubTotal;
            sheet.Cell(currentRow, 5).Style.NumberFormat.Format = "#,##0.00";

            sheet.Cell(currentRow, 6).Value = row.Igv;
            sheet.Cell(currentRow, 6).Style.NumberFormat.Format = "#,##0.00";

            sheet.Cell(currentRow, 7).Value = row.Total;
            sheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0.00";

            sheet.Cell(currentRow, 8).Value = row.AmountPaid;
            sheet.Cell(currentRow, 8).Style.NumberFormat.Format = "#,##0.00";

            sheet.Cell(currentRow, 9).Value = row.Balance;
            sheet.Cell(currentRow, 9).Style.NumberFormat.Format = "#,##0.00";

            sheet.Cell(currentRow, 10).Value = row.CurrencyId;

            sheet.Cell(currentRow, 11).Value = row.ExchangeRate;
            sheet.Cell(currentRow, 11).Style.NumberFormat.Format = "#,##0.0000";

            sheet.Cell(currentRow, 12).Value = row.Status;

            currentRow++;
        }

        return currentRow - 1;
    }

    private static void WriteTotals(IXLWorksheet sheet, PurchaseRegisterTotalsDto totals, int lastDataRow)
    {
        var row = lastDataRow + 2;

        WriteTotalLine(sheet, row++, "Cantidad de documentos:", totals.CantidadDocumentos, "#,##0");
        WriteTotalLine(sheet, row++, "Total comprado:", totals.TotalCompras, "#,##0.00");
        WriteTotalLine(sheet, row++, "Total pagado:", totals.TotalPagado, "#,##0.00");
        WriteTotalLine(sheet, row, "Saldo pendiente:", totals.TotalPendiente, "#,##0.00");
    }

    private static void WriteTotalLine(IXLWorksheet sheet, int row, string label, decimal value, string numberFormat)
    {
        var labelCell = sheet.Cell(row, 2);
        labelCell.Value = label;
        labelCell.Style.Font.Bold = true;

        var valueCell = sheet.Cell(row, 3);
        valueCell.Value = value;
        valueCell.Style.Font.Bold = true;
        valueCell.Style.NumberFormat.Format = numberFormat;
    }
}
