using System.Globalization;

namespace ContinuedFractionsResearcher
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var rootDirectory = AppContext.BaseDirectory
                    [..AppContext.BaseDirectory.IndexOf("ContinuedFractionsResearcher", StringComparison.Ordinal)];
                
                var fileInput = Path.Combine(rootDirectory, "input.txt");
                var fileOutput = Path.Combine(rootDirectory, "output.txt");
                
                var inputArray = File.ReadAllLines(fileInput);

                if (inputArray.Length != 4)
                {
                    File.WriteAllText(fileOutput, "Incorrect input. Lines count is not 4.");
                    return;
                }

                var input = new string[4];
                for (var i = 0; i < 4; i++)
                    input[i] = inputArray[i].Split(": ")[1];

                var researcher = new Researcher(int.Parse(input[3]));

                var partialQuotientsCountRangeInput = input[1].Split("-");
                var partialQuotientsCountRange = partialQuotientsCountRangeInput.Length == 2 ? 
                    (int.Parse(partialQuotientsCountRangeInput[0]), int.Parse(partialQuotientsCountRangeInput[1])) : 
                    (int.Parse(partialQuotientsCountRangeInput[0]), int.Parse(partialQuotientsCountRangeInput[0]));

                var partialQuotientsValueRangeInput = input[2].Split("-");
                var partialQuotientsValueRange = (int.Parse(partialQuotientsValueRangeInput[0]), int.Parse(partialQuotientsValueRangeInput[1]));
                
                researcher.GenerateContinuedFractions(int.Parse(input[0]), partialQuotientsCountRange, partialQuotientsValueRange);

                // var writer = File.AppendText(fileOutput);
                // foreach (var item in researcher.Chart.Values)
                //     writer.Write(item + "\n");
                
                File.WriteAllText(fileOutput, researcher.Chart.ToString());
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(Path.Combine(AppContext.BaseDirectory
                    [..AppContext.BaseDirectory.IndexOf("ContinuedFractionsResearcher", StringComparison.Ordinal)], "output.txt"), 
                    "File not found.");
            }
            catch (FormatException)
            {
                File.WriteAllText(Path.Combine(AppContext.BaseDirectory
                        [..AppContext.BaseDirectory.IndexOf("ContinuedFractionsResearcher", StringComparison.Ordinal)], "output.txt"), 
                    "Incorrect input. Values is not numbers.");
            }
            catch (ArgumentException)
            {
                File.WriteAllText(Path.Combine(AppContext.BaseDirectory
                        [..AppContext.BaseDirectory.IndexOf("ContinuedFractionsResearcher", StringComparison.Ordinal)], "output.txt"), 
                    "Incorrect input. Values is not right numbers.");
            }
            catch (IndexOutOfRangeException)
            {
                File.WriteAllText(Path.Combine(AppContext.BaseDirectory
                        [..AppContext.BaseDirectory.IndexOf("ContinuedFractionsResearcher", StringComparison.Ordinal)], "output.txt"), 
                    "Incorrect input.");
            }
        }
    }
}