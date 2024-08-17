using PredmetProjekat.Common.Dtos.ProductDtos;
using PredmetProjekat.Common.Interfaces.IService;
using PredmetProjekat.Models.Models;
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
        public void CreatePDF(IEnumerable<Receipt> sales, FilterParams filterParams, string username)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .AlignCenter()
                        .Text(GenerateHeaderText(username))
                        .SemiBold().FontSize(24).FontColor(Colors.Grey.Darken4);

                    page.Content()
                        .Column(column =>
                        {
                            column.Item().Text("Filter parameters").FontSize(16).Bold().AlignRight();
                            column.Item().Text("Start date: " + $"{filterParams.StartDate?.ToString() ?? "none"}").FontSize(14).AlignRight();
                            column.Item().Text("End date: " + $"{filterParams.EndDate?.ToString() ?? "none"}").FontSize(14).AlignRight();
                            //column.Item().Text("" + $"{filterParams.EmployeeUsernames}");

                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Sold by");
                                    header.Cell().Text("Register");
                                    header.Cell().Text("Location");
                                    header.Cell().Text("Date");
                                    header.Cell().Text("Total Price").FontColor(Colors.Red.Darken4).AlignRight();

                                    header.Cell().ColumnSpan(5)
                                            .PaddingVertical(5)
                                            .BorderBottom(3)
                                            .BorderColor(Colors.Black);
                                });

                                foreach (var sale in sales)
                                {
                                    table.Cell().Text(sale.SoldBy.UserName).FontColor(Colors.Red.Darken4);
                                    table.Cell().Text(sale.Register.RegisterCode).FontColor(Colors.Red.Darken4);
                                    table.Cell().Text(sale.Register.Location).FontColor(Colors.Red.Darken4);
                                    table.Cell().Text(sale.Date).FontColor(Colors.Red.Darken4);
                                    table.Cell().Text(sale.TotalPrice.ToString("C")).FontColor(Colors.Red.Darken4).AlignRight();

                                    table.Cell()
                                        .ColumnSpan(4)
                                        .PaddingLeft(40)
                                        .PaddingVertical(5)
                                        .Table(productTable =>
                                        {
                                            productTable.ColumnsDefinition(columns =>
                                            {
                                                columns.RelativeColumn();
                                                columns.RelativeColumn();
                                                columns.RelativeColumn();
                                                columns.RelativeColumn();
                                            });

                                            productTable.Header(productHeader =>
                                            {
                                                productHeader.Cell().Text("Product name");
                                                productHeader.Cell().Text("Product type");
                                                productHeader.Cell().Text("Quantity").AlignRight();
                                                productHeader.Cell().Text("Price").AlignRight();


                                                productHeader.Cell().ColumnSpan(4)
                                                        .PaddingVertical(5)
                                                        .BorderBottom(1)
                                                        .AlignRight()
                                                        .BorderColor(Colors.Black);

                                            });

                                            foreach (var product in sale.SoldProducts)
                                            {
                                                productTable.Cell().Text($"# {product.Product.Name}");
                                                productTable.Cell().Text(product.Product.ProductType.Name);
                                                productTable.Cell().Text(product.Quantity.ToString()).AlignRight();
                                                productTable.Cell().Text(product.Product.Price.ToString("C")).AlignRight();
                                            }
                                        });


                                    table.Cell().ColumnSpan(5)
                                            .PaddingVertical(5)
                                            .BorderBottom(2)
                                            .BorderColor(Colors.Black);
                                }
                            });
                        });
                    

                    page.Footer()
                        .AlignBottom()
                        .Text($"Created on: {DateTime.Now}")
                        .SemiBold().FontSize(12).FontColor(Colors.Grey.Darken4);
                });
            }).GeneratePdf(GenerateFileName());
        }

        private string GenerateHeaderText(string username)
        {
            return "Sales list for " + username;
        }

        private string GenerateFileName()
        {
            return "Invoice-" + DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd") + "-" + TimeOnly.FromDateTime(DateTime.Now).ToString("HH'h'mm") + ".pdf";
        }
    }
}
