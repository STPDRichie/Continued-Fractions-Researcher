namespace ContinuedFractionsResearcher;

public static class Program
{
    public static void Main(string[] args)
    {
        var rootDirectory = AppContext.BaseDirectory
            [..AppContext.BaseDirectory.IndexOf("ContinuedFractionsResearcher", StringComparison.Ordinal)];

        var fileInput = Path.Combine(rootDirectory, "input.txt");
        var fileErrorLog = Path.Combine(rootDirectory, "error_log.txt");
        var fileReport = Path.Combine(rootDirectory, "Report.xlsx");

        try
        {
            var inputArray = File.ReadAllLines(fileInput);

            if (inputArray.Length != 4)
            {
                File.WriteAllText(fileErrorLog, "Incorrect input. Lines count is not 4.");
                return;
            }

            var input = new string[4];
            for (var i = 0; i < 4; i++)
                input[i] = inputArray[i].Split(": ")[1];

            var researcher = new Researcher(int.Parse(input[3]));

            var partialQuotientsCountRangeInput = input[1].Split("-");
            var partialQuotientsCountRange = partialQuotientsCountRangeInput.Length == 2
                ? (int.Parse(partialQuotientsCountRangeInput[0]), int.Parse(partialQuotientsCountRangeInput[1]))
                : (int.Parse(partialQuotientsCountRangeInput[0]), int.Parse(partialQuotientsCountRangeInput[0]));

            var partialQuotientsValueRangeInput = input[2].Split("-");
            var partialQuotientsValueRange = (int.Parse(partialQuotientsValueRangeInput[0]),
                int.Parse(partialQuotientsValueRangeInput[1]));

            researcher.GenerateContinuedFractions(int.Parse(input[0]), partialQuotientsCountRange, partialQuotientsValueRange);

            var reportExcel = ReportMaker.Generate(researcher.Chart, int.Parse(input[3]));
                
            File.WriteAllBytes(fileReport, reportExcel);
        }
        catch (FileNotFoundException)
        {
            File.WriteAllText(fileErrorLog, "File not found.");
        }
        catch (FormatException)
        {
            File.WriteAllText(fileErrorLog, "Incorrect input. Values is not numbers.");
        }
        catch (ArgumentException)
        {
            File.WriteAllText(fileErrorLog, "Incorrect input. Values is not right numbers.");
        }
        catch (IndexOutOfRangeException)
        {
            File.WriteAllText(fileErrorLog, "Incorrect input.");
        }
    }
}