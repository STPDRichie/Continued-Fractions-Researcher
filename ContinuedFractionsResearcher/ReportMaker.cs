using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace ContinuedFractionsResearcher;

public static class ReportMaker
{
    public static byte[] Generate(Dictionary<decimal, int> research, int accuracy)
    {
        var package = new ExcelPackage();

        var sheet = package.Workbook.Worksheets.Add("Research report");

        var researchGraph = sheet.Drawings.AddChart("Continued fractions count", eChartType.LineStacked);

        var i = 1;
        foreach (var e in research.Keys)
        {
            sheet.Cells["A" + i].Value = e;
            sheet.Cells["B" + i].Value = research[e];
            i++;
        }

        researchGraph.Title.Text = "Distribution of continued fractions";
        researchGraph.SetPosition(7, 0, 5, 0);
        researchGraph.SetSize(900, 600);

        var researchData = researchGraph.Series.Add(sheet.Cells["B1:B" + accuracy], sheet.Cells["A1:A" + accuracy]);

        researchData.Header = "Continued fractions count";


        return package.GetAsByteArray();
    }
}