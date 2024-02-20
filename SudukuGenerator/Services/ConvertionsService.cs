using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace SudukuGenerator.Services
{
    internal class ConvertionsService
    {


        public string ConvertSudukuMatrixToHtml(int[,] input, string level, int quizNumber)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string htmlPath = Path.Combine(basePath, @"Resources\SudukuHtmlTemplate.html");
            string html = File.ReadAllText(htmlPath);

            int count = 1;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Calculate the product of i and j
                    int value = input[i, j];

                    // Display the product with proper formatting
                    string inputValue = "";
                    if (value != 0)
                    {
                        inputValue = value.ToString();
                    }

                    html = html.Replace($"[{count}]", inputValue);
                    count++;
                }
            }
            html = html.Replace($"[Level]", $"{level}");
            html = html.Replace($"[QuizNumber]", $"{quizNumber}");




            return html;
        }

        public string GetSudukuLevel(int N, int numberOfEmptyCell)
        {
            string level = "Unknown";
            var totalCells = N * N;
            int percentComplete = (int)Math.Round((double)(100 * numberOfEmptyCell) / totalCells);
            if (percentComplete is >= 0 and <= 19)
            {
                level = "Elementary";
            }
            else if (percentComplete is >= 20 and <= 39)
            {
                level = "Easy";
            }
            else if (percentComplete is >= 40 and <= 59)
            {
                level = "intermediate";
            }
            else if (percentComplete is >= 60 and <= 79)
            {
                level = "Hard ";
            }
            else if (percentComplete is >= 80 and <= 99)
            {
                level = "Expert ";
            }
            else if (percentComplete == 100)
            {
                level = "Creator (Build your own)";
            }


            return level;
        }

        public string ConvertHtmlStringToPdf(string html, string fileName, int type)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pdfPath = Path.Combine(basePath, $"Resources\\PDFs\\Quiz\\{fileName}.pdf");
            if (type == 1)
            {
                pdfPath = Path.Combine(basePath, $"Resources\\PDFs\\Solution\\{fileName}.pdf");
            }
           
            
            string cssPath = "";

            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;

            // create a new pdf document converting an html string
            PdfDocument doc = converter.ConvertHtmlString(html, cssPath);

            // save pdf document
            doc.Save(pdfPath);



            // close pdf document
            doc.Close();

            return pdfPath;
        }

        
    }
}
