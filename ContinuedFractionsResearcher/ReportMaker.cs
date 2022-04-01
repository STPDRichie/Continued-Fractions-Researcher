using Microsoft.VisualBasic;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace ContinuedFractionsResearcher;

public class ReportMaker
{
    public static byte[] Generate(Dictionary<decimal, int> research)
    {
        var package = new ExcelPackage();

        var sheet = package.Workbook.Worksheets.Add("Research report");
        var source = package.Workbook.Worksheets.Add("Source");

        var researchGraph = sheet.Drawings.AddChart("Continued fractions count", eChartType.LineStacked);

        var range = CreateRange(source, research);

        researchGraph.Title.Text = "Distribution of continued fractions";
        researchGraph.SetPosition(3, 0, 3, 0);
        researchGraph.SetSize(1000, 650);

        var researchData =
            (ExcelChartSerie)(researchGraph.Series.Add(source.Cells[range.valueRange],
                source.Cells[range.accuracyRange]));

        researchData.Header = "Continued fractions count";

        source.Protection.IsProtected = true;
        sheet.Protection.IsProtected = true;


        return package.GetAsByteArray();
    }

    private static Range CreateRange(ExcelWorksheet source, Dictionary<decimal, int> research)
    {
        source.Cells["A1"].Value = "Accuracy";
        source.Cells["A1"].Style.Font.Bold = true;

        source.Cells["B1"].Value = "Count";
        source.Cells["B1"].Style.Font.Bold = true;

        var rangeCount = 2;
        foreach (var (key, value) in research.OrderBy(x => x.Key))
        {
            source.Cells["A" + rangeCount].Value = key;
            source.Cells["B" + rangeCount].Value = value;
            rangeCount++;
        }

        return new Range("B2:B" + rangeCount, "A2:A" + rangeCount);
    }

    private struct Range
    {
        public readonly string valueRange;
        public readonly string accuracyRange;

        public Range(string valueRange, string accuracyRange)
        {
            this.valueRange = valueRange;
            this.accuracyRange = accuracyRange;
        }
    }
}