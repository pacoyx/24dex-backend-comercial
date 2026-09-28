using ClosedXML.Excel;

public static class ExpenseDetailExcelBuilder
{
    private static readonly string[] Headers =
    {
        "Fecha", "Sucursal", "Categoría", "Origen", "Monto", "Descripción", "Documento", "Proveedor"
    };

    public static byte[] Build(List<ExpenseDetailRowDto> rows, ExpenseReportTotalsDto totals)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Gastos");

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

    private static int WriteRows(IXLWorksheet sheet, List<ExpenseDetailRowDto> rows)
    {
        var currentRow = 2;

        foreach (var row in rows)
        {
            sheet.Cell(currentRow, 1).Value = row.ExpenseDate;
            sheet.Cell(currentRow, 1).Style.DateFormat.Format = "dd/MM/yyyy";

            sheet.Cell(currentRow, 2).Value = row.BranchName;
            sheet.Cell(currentRow, 3).Value = row.CategoryName;
            sheet.Cell(currentRow, 4).Value = row.SourceType;

            sheet.Cell(currentRow, 5).Value = row.Amount;
            sheet.Cell(currentRow, 5).Style.NumberFormat.Format = "#,##0.00";

            sheet.Cell(currentRow, 6).Value = row.Description;
            sheet.Cell(currentRow, 7).Value = row.DocumentNumber ?? "-";
            sheet.Cell(currentRow, 8).Value = row.SupplierName ?? "-";

            currentRow++;
        }

        return currentRow - 1;
    }

    private static void WriteTotals(IXLWorksheet sheet, ExpenseReportTotalsDto totals, int lastDataRow)
    {
        var row = lastDataRow + 2;

        WriteTotalLine(sheet, row++, "Cantidad de gastos:", totals.Count, "#,##0");
        WriteTotalLine(sheet, row, "Total:", totals.TotalAmount, "#,##0.00");
    }

    private static void WriteTotalLine(IXLWorksheet sheet, int row, string label, decimal value, string numberFormat)
    {
        var labelCell = sheet.Cell(row, 4);
        labelCell.Value = label;
        labelCell.Style.Font.Bold = true;

        var valueCell = sheet.Cell(row, 5);
        valueCell.Value = value;
        valueCell.Style.Font.Bold = true;
        valueCell.Style.NumberFormat.Format = numberFormat;
    }
}
