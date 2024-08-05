﻿using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces.IService;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PredmetProjekat.Services.Services
{
    public class DocumentService : IDocumentService
    {
        public DocumentService(LicenseType licenseType)
        {
            QuestPDF.Settings.License = licenseType;
        }
        public void CreatePDF(List<LineItem> lineItems)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(16));

                    page.Header()
                        .AlignCenter()
                        .Text("Sales for date #: 2023-77")
                        .SemiBold().FontSize(24).FontColor(Colors.Grey.Darken4);

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(20);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("#");
                                header.Cell().Text("Product");
                                header.Cell().AlignRight().Text("Price");
                            });

                            foreach (var lineItem in lineItems)
                            {
                                table.Cell().Text(lineItem.Index.ToString());
                                table.Cell().Text(lineItem.Name);
                                table.Cell().Text($"${lineItem.Price}");
                            }
                        });
                });
            })
                .GeneratePdf("invoice.pdf");
        }
    }
}
