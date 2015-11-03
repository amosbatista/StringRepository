using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmosBatista.ComicsServer.Core.StringRepository;
using System.IO;

namespace AmosBatista.ComicServer.StringRepository
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading CSV file");

            //Openning file
            var csvFile = new StreamReader(Environment.CurrentDirectory + "\\ExcelTemplate\\TranslationTemplate.csv",Encoding.Default);
            string [] lineArray;
            string[] header;

            // Setting the repository saver
            var translationRep = new TextRepository_SQLToJSON(true);
            TextRepository translation;
            IdiomTranslation idiomTranslation;
            int contLines = 0;

            // Reading the header, to define how many idioms we got
            header = csvFile.ReadLine().Split(";".ToCharArray());

            // Reading all the content
            do
            {
                translation = new TextRepository();
                
                // Separate to array
                lineArray = csvFile.ReadLine().Split(";".ToCharArray());
                translation.DOMElementID = lineArray[0];

                // Only add a new translation, if it have a value
                if (translation.DOMElementID != "")
                {
                    //Adding all idiom that exists
                    for (int contColunms = 1; contColunms < header.Length; contColunms++)
                    {
                        idiomTranslation = new IdiomTranslation();
                        idiomTranslation.IdiomName = header[contColunms]; // Adding the title of the column, who have the idiom name
                        idiomTranslation.Translation = lineArray[contColunms];

                        translation.AddTranslation(idiomTranslation);
                    }

                    // Saving the configuration of the idiom to the repository
                    translationRep.SaveTextRepository(translation);
                }

                contLines ++;
                Console.WriteLine("Saving line " + contLines);
                
            } while (csvFile.EndOfStream == false );

            // Finishing the text repository
            Console.WriteLine("The save operation is finish.");
            Console.ReadLine();
        }
    }
}
