using JewelShopService.BindingModels;
using JewelShopService.Interfaces;
using JewelShopService.ViewModels;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.ImplementationsBD
{
    public class ReportServiceBD : IReportService
    {
        private AbstractDataBaseContext context;

        public ReportServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public void SaveAdornmentPrice(ReportBindingModel model)
        {
            if (File.Exists(model.fileName))
            {
                File.Delete(model.fileName);
            }

            var winword = new Microsoft.Office.Interop.Word.Application();
            try
            {
                object missing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Document document =
                    winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;
                range.Text = "Прайс изделий";
                var font = range.Font;
                font.Size = 16;
                font.Name = "Times New Roman";
                font.Bold = 1;
                var paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;
                range.InsertParagraphAfter();

                var products = context.Adornments.ToList();
                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;
                var table = document.Tables.Add(rangeTable, products.Count, 2, ref missing, ref missing);

                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";

                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;

                for (int i = 0; i < products.Count; ++i)
                {
                    table.Cell(i + 1, 1).Range.Text = products[i].adornmentName;
                    table.Cell(i + 1, 2).Range.Text = products[i].price.ToString();
                }
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;

                range.Text = "Дата: " + DateTime.Now.ToLongDateString();

                font = range.Font;
                font.Size = 12;
                font.Name = "Times New Roman";

                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 10;

                range.InsertParagraphAfter();
                object fileFormat = WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(model.fileName, ref fileFormat, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing);
                document.Close(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                winword.Quit();
            }
        }

        public List<HangarsLoadViewModel> GetHangarsLoad()
        {
            return context.Hangars
                            .ToList()
                            .GroupJoin(
                                    context.HangarElements
                                                .Include(r => r.Element)
                                                .ToList(),
                                    stock => stock,
                                    stockComponent => stockComponent.Hangar,
                                    (stock, stockCompList) =>
            new HangarsLoadViewModel
            {
                hangarName = stock.hangarName,
                totalCount = stockCompList.Sum(r => r.count),
                 Elements = stockCompList.Select(r => new HangarsElementLoadViewModel { ElementName = r.Element.elementName, Count = r.count }).ToList()
            })
                            .ToList();
        }

        public void SaveHangarsLoad(ReportBindingModel model)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                if (File.Exists(model.fileName))
                {
                    excel.Workbooks.Open(model.fileName, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = 1;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs(model.fileName, XlFileFormat.xlExcel8, Type.Missing,
                        Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                Sheets excelsheets = excel.Workbooks[1].Worksheets;
                var excelworksheet = (Worksheet)excelsheets.get_Item(1);
                excelworksheet.Cells.Clear();
                excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                excelworksheet.PageSetup.CenterHorizontally = true;
                excelworksheet.PageSetup.CenterVertically = true;
                Microsoft.Office.Interop.Excel.Range excelcells = excelworksheet.get_Range("A1", "C1");

                excelcells.Merge(Type.Missing);
                excelcells.Font.Bold = true;
                excelcells.Value2 = "Загруженность складов";
                excelcells.RowHeight = 25;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 14;

                excelcells = excelworksheet.get_Range("A2", "C2");
                excelcells.Merge(Type.Missing);
                excelcells.Value2 = "на" + DateTime.Now.ToShortDateString();
                excelcells.RowHeight = 20;
                excelcells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                excelcells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                excelcells.Font.Name = "Times New Roman";
                excelcells.Font.Size = 12;

                var dict = GetHangarsLoad();
                if (dict != null)
                {
                    excelcells = excelworksheet.get_Range("C1", "C1");
                    foreach (var elem in dict)
                    {
                        excelcells = excelcells.get_Offset(2, -2);
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = elem.hangarName;
                        excelcells = excelcells.get_Offset(1, 1);
                        if (elem.Elements.Count() > 0)
                        {
                            var excelBorder =
                                excelworksheet.get_Range(excelcells,
                                            excelcells.get_Offset(elem.Elements.Count() - 1, 1));
                            excelBorder.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                            excelBorder.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
                            excelBorder.HorizontalAlignment = Constants.xlCenter;
                            excelBorder.VerticalAlignment = Constants.xlCenter;
                            excelBorder.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                                    Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                                    Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                    1);

                            foreach (var listElem in elem.Elements)
                            {
                                excelcells.Value2 = listElem.ElementName;
                                excelcells.ColumnWidth = 10;
                                excelcells.get_Offset(0, 1).Value2 = listElem.Count;
                                excelcells = excelcells.get_Offset(1, 0);
                            }
                        }
                        excelcells = excelcells.get_Offset(0, -1);
                        excelcells.Value2 = "Итого";
                        excelcells.Font.Bold = true;
                        excelcells = excelcells.get_Offset(0, 2);
                        excelcells.Value2 = elem.totalCount;
                        excelcells.Font.Bold = true;
                    }
                }
                excel.Workbooks[1].Save();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                excel.Quit();
            }
        }

        public List<BuyerOrdersModel> GetBuyerOrders(ReportBindingModel model)
        {
            return context.ProdOrders
                            .Include(rec => rec.Buyer)
                            .Include(rec => rec.Adornment)
                            .Where(rec => rec.DateCreate >= model.dateFrom && rec.DateCreate <= model.dateTo)
                            .Select(rec => new BuyerOrdersModel
                            {
                                buyerName = rec.Buyer.buyerFIO,
                                dateCustom = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                            SqlFunctions.DateName("yyyy", rec.DateCreate),
                                adornmentName = rec.Adornment.adornmentName,
                                count = rec.count,
                                sum = rec.sum,
                                status = rec.status.ToString()
                            })
                            .ToList();
        }

        public void SaveBuyerOrders(ReportBindingModel model)
        {
            if (!File.Exists("TIFCYR.TTF"))
            {
                File.WriteAllBytes("TIFCYR.TTF", Properties.Resources.TIFCYR);
            }
            FileStream fs = new FileStream(model.fileName, FileMode.OpenOrCreate, FileAccess.Write);
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIFCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            var phraseTitle = new Phrase("Заказы клиентов",
                new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            var phrasePeriod = new Phrase("c " + model.dateFrom.Value.ToShortDateString() +
                                    " по " + model.dateTo.Value.ToShortDateString(),
                                    new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);
            PdfPTable table = new PdfPTable(6)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[] { 160, 140, 160, 100, 100, 140 });
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("ФИО клиента", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата создания", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Изделие", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Сумма", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Статус", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            var list = GetBuyerOrders(model);
            var fontForCells = new iTextSharp.text.Font(baseFont, 10);
            for (int i = 0; i < list.Count; i++)
            {
                cell = new PdfPCell(new Phrase(list[i].buyerName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].dateCustom, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].adornmentName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].count.ToString(), fontForCells));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].sum.ToString(), fontForCells));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].status, fontForCells));
                table.AddCell(cell);
            }
            cell = new PdfPCell(new Phrase("Итого:", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 4,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(list.Sum(rec => rec.sum).ToString(), fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fontForCellBold))
            {
                Border = 0
            };
            table.AddCell(cell);
            doc.Add(table);

            doc.Close();
        }
    }
}
