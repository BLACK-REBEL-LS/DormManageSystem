using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//
using MSword = Microsoft.Office.Interop.Word;
using System.IO;

using System.Threading;
using Microsoft.Office.Interop.Word;
using System.Data;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace Common
{
    public class ToWord
    {
        MSword.Application WordApp;//Application:代表Microsoft Word 应用呈现本身
        MSword.Document WordDoc;//Document代表一个我word文档，拥有焦点的document称为Active Document
        private object filename = "";
        System.Data.DataTable dt = new System.Data.DataTable();

        public bool CourseTable(System.Data.DataTable dt, string path, string title)
        {
            int r = dt.Rows.Count;
            int c = dt.Columns.Count;
            try
            {
                string strTitle = title;//表头
                object oEndOfDoc = "\\endofdoc";
                Object Nothing = System.Reflection.Missing.Value;
                filename = path;//路径

                WordApp = new MSword.ApplicationClass();//创建word应用程序
                WordDoc = WordApp.Documents.Add(ref Nothing,ref Nothing,ref Nothing,ref Nothing);//根据这个模板来创建新的文档 文档对象

                WordApp.Visible = false;//设置动态建立的word文档可见
                WordDoc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;//A4
                WordDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;//排列方式为横向方向
                WordApp.Selection.PageSetup.LeftMargin = WordApp.CentimetersToPoints(float.Parse("1"));//设置word文档的左边距
                WordApp.Selection.PageSetup.RightMargin = WordApp.CentimetersToPoints(float.Parse("1"));
                WordApp.Selection.PageSetup.TopMargin = WordApp.CentimetersToPoints(float.Parse("1"));
                WordApp.Selection.PageSetup.BottomMargin=WordApp.CentimetersToPoints(float.Parse("1"));
                WordApp.ActiveWindow.HorizontalPercentScrolled = 11;//设置文档的水平滑动距离
                WordApp.ActiveWindow.ActivePane.View.Zoom.Percentage = 100;//设置文档的百分比例

                WordDoc.PageSetup.HeaderDistance = 30.0f;//页眉位置

                WordApp.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdOutlineView;//视图样式
                WordApp.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekPrimaryHeader;//进入页眉设置，
                WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                WordApp.ActiveWindow.ActivePane.Selection.ParagraphFormat.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                WordApp.ActiveWindow.ActivePane.Selection.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].Visible = false;
                WordApp.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument;

                //设置页码
                Microsoft.Office.Interop.Word.PageNumbers pns = WordApp.Selection.Sections[1].Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers;//获取当前页的号码
                pns.NumberStyle = Microsoft.Office.Interop.Word.WdPageNumberStyle.wdPageNumberStyleNumberInDash;
                pns.HeadingLevelForChapter = 0;
                pns.IncludeChapterNumber = false;
                pns.RestartNumberingAtSection = false;
                pns.StartingNumber = 0;
                object pagenmbetal = Microsoft.Office.Interop.Word.WdPageNumberAlignment.wdAlignPageNumberCenter;//将号码设置在中间.
                object first = true;
                WordApp.Selection.Sections[1].Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers.Add(ref pagenmbetal, ref first);


                //行距
                //WordApp.Selection.ParagraphFormat.LineSpacingRule = MSword.WdLineSpacing.wdLineSpaceExactly;
                //WordApp.Selection.ParagraphFormat.LineSpacing = 24f;//设置文档的行间距

                MSword.Paragraph d;
                //在document的开始部分添加一个paragraph
                d = WordDoc.Content.Paragraphs.Add(ref Nothing);
                d.Range.ParagraphFormat.LineSpacingRule = MSword.WdLineSpacing.wdLineSpaceExactly;
                d.Range.ParagraphFormat.LineSpacing = 22f;//Format样式，版式
                d.Range.Text = strTitle;//表头
                d.Range.Font.Bold = 0;
                d.Range.Font.Name = "华文中宋";
                d.Range.Font.Size = 22;
                d.Range.ParagraphFormat.Alignment = MSword.WdParagraphAlignment.wdAlignParagraphCenter;//对齐方式（居中）
                d.Format.SpaceAfter = 5;
                d.Range.InsertParagraphAfter();

                //文档中创建表格
                MSword.Table newTable2;
                MSword.Range wrdRngdata = WordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                //row  conlumn
                newTable2 = WordDoc.Tables.Add(wrdRngdata,r+1,c,ref Nothing,ref Nothing);//设置表格为几行几列
               
                newTable2.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;//居中
                newTable2.Range.Font.Name = "微软雅黑";
                newTable2.Range.ParagraphFormat.LineSpacingRule = MSword.WdLineSpacing.wdLineSpaceExactly;
                newTable2.Range.ParagraphFormat.LineSpacing = 10f;

                //设置单元格样式居中
                for (int e = 1; e <= c; e++)
                {
                    newTable2.Columns[e].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                }

                //设置第一行 高
                newTable2.Borders.OutsideLineStyle = MSword.WdLineStyle.wdLineStyleSingle;
                newTable2.Borders.OutsideLineWidth = MSword.WdLineWidth.wdLineWidth150pt;
                newTable2.Borders.InsideLineStyle = MSword.WdLineStyle.wdLineStyleSingle;
                newTable2.Rows[1].Height = 20f;
                newTable2.Rows[1].Range.Font.Size = 12;
                newTable2.Rows[1].HeightRule = MSword.WdRowHeightRule.wdRowHeightExactly;//设置行高值为固定

                newTable2.Borders.OutsideLineStyle = MSword.WdLineStyle.wdLineStyleSingle;
                newTable2.Borders.OutsideLineWidth = MSword.WdLineWidth.wdLineWidth100pt;
                newTable2.Borders.InsideLineStyle = MSword.WdLineStyle.wdLineStyleSingle;
               // newTable2.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth025pt;
                newTable2.Columns[1].Width = 35f;
               

                //for (int i = 1; i <= r; i++)
                //{
                //    newTable2.Cell(i, 1).Range.Font.Size = 14;
                //    newTable2.Cell(i, 1).Range.Font.Bold = 1;
                //}

                newTable2.Range.Font.Size = 10;
                newTable2.Rows[1].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                newTable2.Rows[1].Range.ParagraphFormat.LineSpacingRule = MSword.WdLineSpacing.wdLineSpaceExactly;
                newTable2.Rows[1].Range.ParagraphFormat.LineSpacing = 10f;
                newTable2.Rows[1].Range.Font.Size = 10;
                newTable2.Rows[1].Range.Font.Bold = 1;


                //newTable2.Cell(1, 1).Range.Text = dt.Columns[0].ToString();
                for (int i = 1; i <= c; i++)//列 名
                {
                    newTable2.Cell(1, i).Range.Text = dt.Columns[i - 1].ToString();
                }

                for (int i = 2; i <= r; i++)
                {
                    newTable2.Rows[i].Height = 20f;
                    for (int j = 2; j <=c; j++)
                    {
                        newTable2.Cell(i, j).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                }
                for (int k = 1; k <=r; k++)
                {
                    for (int i = 1; i <= c; i++)
                    {
                        newTable2.Cell(k + 1, i).Range.Text = dt.Rows[k - 1][i - 1].ToString();
                        newTable2.Cell(k + 1, i).Range.ParagraphFormat.Alignment = MSword.WdParagraphAlignment.wdAlignParagraphLeft;
                    }
                }
                WordDoc.SaveAs(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                        ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                        ref Nothing, ref Nothing);


                WordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
                WordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
                GC.Collect();

                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
