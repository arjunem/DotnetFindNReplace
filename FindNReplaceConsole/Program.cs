namespace FindNReplaceConsole;
public class Program
{
    private static readonly FindNReplaceManager _findNReplaceManager = new FindNReplaceManager();
    static void Main(string[] arg)
    {
        Console.Write("Enter the input csv file path: ");
        string csvFilePath = Console.ReadLine();

        Console.Write("Enter the text file to be modified path: ");
        string textFilePath = Console.ReadLine();

         Console.Write("Enter the modified path location: ");
        string textFileOutPath = Console.ReadLine();

        // Read the CSV file to get find-replace pairs
        var findReplaceList = _findNReplaceManager.ReadFindReplaceModelData(csvFilePath);

        // Process the text file
        _findNReplaceManager.Process(textFilePath,textFileOutPath, findReplaceList);
    }

}