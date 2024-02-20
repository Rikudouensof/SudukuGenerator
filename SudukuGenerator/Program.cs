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

            int questionNumber = 1;
            int N = 9;
            List<int> biaceList = new List<int>() {13, 29, 46,62, 71 };
            foreach (var item in biaceList)
            {
                int K = item;
                for (int i = 0; i < 150; i++)
                {
                    SudukuGeneratorService sudoku = new SudukuGeneratorService(N, K);
                    sudoku.fillValues();
                    var sudukuarray = sudoku.generateSudoku();
                    var level = convertionsService.GetSudukuLevel(N, K);
                    var quizhtmlString = convertionsService.ConvertSudukuMatrixToHtml(sudukuarray.Quiz, level, questionNumber);
                    var solutionhtmlString = convertionsService.ConvertSudukuMatrixToHtml(sudukuarray.Solution, level, questionNumber);
                    var quizpdfPath = convertionsService.ConvertHtmlStringToPdf(quizhtmlString, $"Quiz {questionNumber}", 2);
                    var solutionpdfPath = convertionsService.ConvertHtmlStringToPdf(solutionhtmlString, $"Quiz {questionNumber}", 1);
                    questionNumber++;
                }
            }
            
           

           

            


        }
    }
}
