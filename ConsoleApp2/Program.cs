using DevExpress.Pdf;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tesseract;

namespace ConsoleApp2
{
    public class Program
    {
      
        public static void Main(string[] args)
        {
            string inputfile;
            string outputfile;
            string tempfile;
            int largestEdgeLength = 1000;
            int pageNumbers = new int();
            if (args == null)
            {
                Console.WriteLine("args null");
            }
            else
            {
                                 
                  
                    inputfile = @"C:\Tesseract-OCR\test\" + args[0];
                    outputfile = @"C:\Tesseract-OCR\test\" + args[1];
                
                    System.IO.FileInfo ff = new System.IO.FileInfo(inputfile);
                    string DosyaUzantisi = ff.Extension;


                tempfile = Environment.GetEnvironmentVariable("temp") + @"\" + Guid.NewGuid().ToString() + ".tiff";
               // tempfile = outputfile;
                if (DosyaUzantisi==".pdf")
                {
                    using (PdfDocumentProcessor processor = new PdfDocumentProcessor())
                    {


                        processor.LoadDocument(inputfile);

                        pageNumbers = processor.Document.Pages.Count;
                        int[] PageNumbers = new int[pageNumbers];
                        for (int i = 0; i < pageNumbers ; i++)
                        {
                            PageNumbers[i] = i+1;
                        }
                        string tifftemp = @"C:\Tesseract-OCR\test\"+args[1]+".tiff";
                        processor.CreateTiff(tifftemp, largestEdgeLength, PageNumbers);
                       
                        tes(tifftemp, outputfile);
                        
                    }
                }                
                else if ((DosyaUzantisi == ".tiff")||(DosyaUzantisi == ".png")|| (DosyaUzantisi == ".jpeg")|| (DosyaUzantisi == ".bmp")|| (DosyaUzantisi == ".jpg"))
                {
                   
                       
                        tes(inputfile, outputfile);
                        


                }
                
               
            }
           
        }
         static void tes(string tempfile, string outputfile)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\Tesseract-OCR\tesseract.exe";
          startInfo.WorkingDirectory = @"C:\Tesseract-OCR";
            startInfo.Arguments =  tempfile + " " + outputfile + " " + "-l tur+eng";
            Console.Write(startInfo.Arguments);
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.CreateNoWindow = false;
            
            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata" + ex.Message.ToString() + "\n" + tempfile + "\n" + outputfile);
            };
            File.Delete(tempfile);

        }
    }
}