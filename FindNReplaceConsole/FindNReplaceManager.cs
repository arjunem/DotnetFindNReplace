using CsvHelper;
using System.Globalization;
namespace FindNReplaceConsole;

public class FindNReplaceManager
{
    /// <summary>
    /// Gets the Find and Replace Pairs CSV data
    /// </summary>
    /// <param name="csvFilePath"></param>
    /// <returns></returns>
    public List<FindNReplaceModel> ReadFindReplaceModelData(string csvFilePath)
    {
        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return new List<FindNReplaceModel>(csv.GetRecords<FindNReplaceModel>());
        }
    }

    /// <summary>
    /// Process the find and Replcaed data
    /// </summary>
    /// <param name="textFilePath"></param>
    /// <param name="findReplaceList"></param>
    public void Process(string textFilePath,string textFileOutPath, List<FindNReplaceModel> findReplaceList)
    {
        var lines = File.ReadAllLines(textFilePath);
        var updatedLines = new List<string>();
        bool isReplaced = false;
        for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
        {
            isReplaced = false;
            string line = lines[lineIndex];
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int wordIndex = 0; wordIndex < words.Length; wordIndex++)
            {
                string word = words[wordIndex];
                isReplaced = false;

                foreach (var findReplace in findReplaceList)
                {
                    if (word.Equals(findReplace.Find, StringComparison.OrdinalIgnoreCase))
                    {
                        word = findReplace.Replace;
                        isReplaced = true;
                    }
                }

                // Replace word in line
                words[wordIndex] = word;

                // Find word's start position (index) in the line
                int wordPosition = line.IndexOf(words[wordIndex]);

                Console.WriteLine($"Word: '{word}' | Line: {lineIndex + 1}, Position: {wordPosition} | Replaced: {isReplaced}");
            }

            // Rebuild the modified line
            updatedLines.Add(string.Join(' ', words));
        }

        // Write updated content back to the file
        File.WriteAllText(textFileOutPath, string.Join("\n", updatedLines));
        Console.WriteLine($"\nThe file '{textFileOutPath}' has been updated with the replaced text.");

    }
}


