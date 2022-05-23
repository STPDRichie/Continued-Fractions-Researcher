using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace ContinuedFractionsResearcher;

public static class ReportMaker
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
            (ExcelChartSerie)(researchGraph.Series.Add(source.Cells[range.ValueRange],
                source.Cells[range.AccuracyRange]));

        // researchData.Header = "Continued fractions count";

        // source.Protection.IsProtected = true;
        // sheet.Protection.IsProtected = true;


        return package.GetAsByteArray();
    }

    private static Range CreateRange(ExcelWorksheet source, Dictionary<decimal, int> research)
    {
        source.Cells["A1"].Value = "Accuracy";
        source.Cells["A1"].Style.Font.Bold = true;

        source.Cells["B1"].Value = "Count";
        source.Cells["B1"].Style.Font.Bold = true;

        source.Cells["C1"].Value = "Relative";
        source.Cells["C1"].Style.Font.Bold = true;

        source.Cells["D1"].Value = "Sum";
        source.Cells["D1"].Style.Font.Bold = true;

        var rangeCount = 2;
        var sumFrequency = 0.0;
        var fractionCount = research.Sum(x => x.Value);
        foreach (var (key, value) in research.OrderBy(x => x.Key))
        {
            sumFrequency += (double)value / fractionCount;
            source.Cells["A" + rangeCount].Value = key;
            source.Cells["B" + rangeCount].Value = value;
            source.Cells["C" + rangeCount].Value = (double)value / fractionCount;
            source.Cells["D" + rangeCount].Value = sumFrequency;
            rangeCount++;
        }

        return new Range("C2:C" + rangeCount, "A2:A" + rangeCount);
    }

    private struct Range
    {
        public readonly string ValueRange;
        public readonly string AccuracyRange;

        public Range(string valueRange, string accuracyRange)
        {
            ValueRange = valueRange;
            AccuracyRange = accuracyRange;
        }
    }
}