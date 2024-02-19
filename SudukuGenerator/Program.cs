using SudukuGenerator.Services;
using System.Reflection.Emit;

namespace SudukuGenerator
{
    internal class Program
    {
        // Driver code
        public static void Main(string[] args)
        {
            ConvertionsService convertionsService = new();


            int N = 9, K = 70;
            for (int i = 0; i < 30; i++)
            {
                SudukuGeneratorService sudoku = new SudukuGeneratorService(N, K);
                sudoku.fillValues();
                var sudukuarray = sudoku.generateSudoku();
                var level = convertionsService.GetSudukuLevel(N,K);
                var htmlString = convertionsService.ConvertSudukuMatrixToHtml(sudukuarray, level);
                var pdfPath = convertionsService.ConvertHtmlStringToPdf(htmlString, $"{level}{i}");

            }

           


        }
    }
}
