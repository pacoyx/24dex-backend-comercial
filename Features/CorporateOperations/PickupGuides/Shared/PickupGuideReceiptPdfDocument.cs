using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class PickupGuideReceiptPdfDocument : IDocument
{
    private const string LogoPath = "wwwroot/images/logo.png";

    private readonly PickupGuide _guide;
    private readonly byte[]? _signatureImage;
    private readonly List<byte[]> _photoImages;
    private readonly DateTime _generatedAt;

    public PickupGuideReceiptPdfDocument(PickupGuide guide, byte[]? signatureImage, List<byte[]> photoImages)
    {
        _guide = guide;
        _signatureImage = signatureImage;
        _photoImages = photoImages;
        _generatedAt = DateTime.Now;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(30);
            page.DefaultTextStyle(x => x.FontSize(9));

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Row(row =>
            {
                if (File.Exists(LogoPath))
                {
                    row.ConstantItem(60).Height(60).Image(LogoPath).FitArea();
                }

                row.RelativeItem().Column(col =>
                {
                    col.Item().AlignCenter().Text("CONSTANCIA DE RECOJO").FontSize(16).Bold();
                    col.Item().AlignCenter().Text($"Guía: {_guide.GuideNumber}").FontSize(11).Bold();
                    col.Item().AlignCenter().Text($"Generado el: {_generatedAt:dd/MM/yyyy HH:mm}").FontSize(8);
                });
            });

            column.Item().PaddingTop(8).Row(row =>
            {
                row.RelativeItem().Text(text =>
                {
                    text.Span("Cliente: ").Bold();
                    text.Span(PickupGuidesEndpoints.GetCustomerDisplayName(_guide.Customer));
                });
                row.RelativeItem().Text(text =>
                {
                    text.Span("Fecha de recojo: ").Bold();
                    text.Span(_guide.PickupDate.ToString("dd/MM/yyyy"));
                });
            });

            column.Item().PaddingTop(3).Row(row =>
            {
                row.RelativeItem().Text(text =>
                {
                    text.Span("Empleado: ").Bold();
                    text.Span(_guide.Employee is null ? "-" : $"{_guide.Employee.FirstName} {_guide.Employee.LastName}".Trim());
                });
                row.RelativeItem().Text(text =>
                {
                    text.Span("Vehículo: ").Bold();
                    text.Span(_guide.Vehicle?.Plate ?? "-");
                });
            });

            column.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Darken1);
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().PaddingTop(10).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3f);
                    columns.RelativeColumn(1.2f);
                    columns.RelativeColumn(1.2f);
                    columns.RelativeColumn(1.2f);
                    columns.RelativeColumn(2f);
                });

                table.Header(header =>
                {
                    header.Cell().Element(HeaderCellStyle).Text("Servicio");
                    header.Cell().Element(HeaderCellStyle).AlignRight().Text("Cantidad");
                    header.Cell().Element(HeaderCellStyle).AlignRight().Text("P. Unit.");
                    header.Cell().Element(HeaderCellStyle).AlignRight().Text("Total");
                    header.Cell().Element(HeaderCellStyle).Text("Observación");

                    static IContainer HeaderCellStyle(IContainer c) =>
                        c.DefaultTextStyle(x => x.Bold().FontColor(Colors.White))
                         .Background(Colors.Blue.Darken2)
                         .Padding(4);
                });

                foreach (var detail in _guide.Details)
                {
                    var isPricedByWeight = detail.ServiceItem.IsPeso == "S";
                    var quantityText = isPricedByWeight
                        ? $"{detail.Weight:N2} kg"
                        : detail.Quantity.ToString("N2");

                    table.Cell().Element(CellStyle).Text(detail.ServiceItem.Name);
                    table.Cell().Element(CellStyle).AlignRight().Text(quantityText);
                    table.Cell().Element(CellStyle).AlignRight().Text(detail.UnitPrice.ToString("N2"));
                    table.Cell().Element(CellStyle).AlignRight().Text(detail.Total.ToString("N2"));
                    table.Cell().Element(CellStyle).Text(detail.Observation ?? "-");
                }

                static IContainer CellStyle(IContainer c) =>
                    c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(3).PaddingHorizontal(4);
            });

            column.Item().PaddingTop(8).AlignRight().Text(text =>
            {
                text.Span("Total: ").Bold();
                text.Span(_guide.TotalAmount.ToString("N2"));
            });

            if (!string.IsNullOrWhiteSpace(_guide.Observation))
            {
                column.Item().PaddingTop(10).Text(text =>
                {
                    text.Span("Observaciones: ").Bold();
                    text.Span(_guide.Observation);
                });
            }

            if (_photoImages.Count > 0)
            {
                column.Item().PaddingTop(15).Text("Evidencia fotográfica").Bold().FontSize(11);
                column.Item().PaddingTop(5).Row(row =>
                {
                    foreach (var photo in _photoImages)
                    {
                        row.RelativeItem().Padding(3).Height(140).Image(photo).FitArea();
                    }
                });
            }

            if (_signatureImage is not null)
            {
                column.Item().PaddingTop(15).Text("Firma de conformidad").Bold().FontSize(11);
                column.Item().PaddingTop(5).Height(100).Width(200).Image(_signatureImage).FitArea();
            }
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().LineHorizontal(1).LineColor(Colors.Grey.Darken1);
            column.Item().PaddingTop(5).AlignRight().Text(text =>
            {
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
            });
        });
    }
}
