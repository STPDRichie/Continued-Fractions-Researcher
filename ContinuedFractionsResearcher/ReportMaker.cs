using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace ContinuedFractionsResearcher;

public static class ReportMaker
{
    private static readonly ExcelPackage Package = new ExcelPackage();

    public static byte[] GetPackage()
    {
        return Package.GetAsByteArray();
    }

    public static void Generate<T>(Dictionary<decimal, T> research, string sheetName, string title, string xAxisName,
        string yAxisName)
    {
        var sheet = Package.Workbook.Worksheets.Add(sheetName);
        var sourceSheet = Package.Workbook.Worksheets.Add($"{sheetName}_source");

        var graph = sheet.Drawings.AddChart(sheetName, eChartType.LineStacked);

        var range = CreateRange(sourceSheet, research, xAxisName, yAxisName);

        graph.Title.Text = title;
        graph.SetPosition(3, 0, 3, 0);
        graph.SetSize(1000, 650);

        graph.Series.Add(sourceSheet.Cells[range.ValueRange],
            sourceSheet.Cells[range.AccuracyRange]);
    }

    private static Range CreateRange<T>(ExcelWorksheet source, Dictionary<decimal, T> research, string xAxisName,
        string yAxisName)
    {
        source.Cells["A1"].Value = xAxisName;
        source.Cells["A1"].Style.Font.Bold = true;

        source.Cells["B1"].Value = yAxisName;
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
        public readonly string ValueRange;
        public readonly string AccuracyRange;

        public Range(string valueRange, string accuracyRange)
        {
            ValueRange = valueRange;
            AccuracyRange = accuracyRange;
        }
    }
}