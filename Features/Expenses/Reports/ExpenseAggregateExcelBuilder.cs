using ClosedXML.Excel;

public static class ExpenseAggregateExcelBuilder
{
    public static byte[] Build(string labelHeader, List<ExpenseAggregateRowDto> rows, ExpenseReportTotalsDto totals)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Gastos");

        WriteHeader(sheet, labelHeader);
        var lastDataRow = WriteRows(sheet, rows);
        WriteTotals(sheet, totals, lastDataRow);

        sheet.SheetView.FreezeRows(1);
        sheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    private static void WriteHeader(IXLWorksheet sheet, string labelHeader)
    {
        string[] headers = { labelHeader, "Cantidad", "Monto" };

        for (var i = 0; i < headers.Length; i++)
        {
            var cell = sheet.Cell(1, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#BDD7EE");
        }
    }

    private static int WriteRows(IXLWorksheet sheet, List<ExpenseAggregateRowDto> rows)
    {
        var currentRow = 2;

        foreach (var row in rows)
        {
            sheet.Cell(currentRow, 1).Value = row.Label;

            sheet.Cell(currentRow, 2).Value = row.Count;
            sheet.Cell(currentRow, 2).Style.NumberFormat.Format = "#,##0";

            sheet.Cell(currentRow, 3).Value = row.Amount;
            sheet.Cell(currentRow, 3).Style.NumberFormat.Format = "#,##0.00";

            currentRow++;
        }

        return currentRow - 1;
    }

    private static void WriteTotals(IXLWorksheet sheet, ExpenseReportTotalsDto totals, int lastDataRow)
    {
        var row = lastDataRow + 2;

        var labelCell = sheet.Cell(row, 1);
        labelCell.Value = "Total:";
        labelCell.Style.Font.Bold = true;

        var countCell = sheet.Cell(row, 2);
        countCell.Value = totals.Count;
        countCell.Style.Font.Bold = true;
        countCell.Style.NumberFormat.Format = "#,##0";

        var amountCell = sheet.Cell(row, 3);
        amountCell.Value = totals.TotalAmount;
        amountCell.Style.Font.Bold = true;
        amountCell.Style.NumberFormat.Format = "#,##0.00";
    }
}
