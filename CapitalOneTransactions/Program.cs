using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace CapitalOneTransactions
{
    class Program
    {
        static void Main(string[] args)
        {
            // input file name:  transaction.txt
            string text = "";
            string str_ast;
            StreamReader inputfile = new StreamReader(@"D:\CapitalOneTransaction.txt");
            string outfilepath = @"D:\CapitalOneTranOut.txt";
            while (null != (str_ast = inputfile.ReadLine()))
            {
                text += str_ast;
            }
            //string Patern = @"\d{2}/\d{2}/\d{2,4}([\w\s\t\n#/\,\..-]+)\$\d+\.\d{2}";
            string Patern = @"\d{1,2}/\d{2}/\d{2,4}([^\$]+)(\$|(-\$))\d*,*\d+\.\d{2}";
            string DatePatern = @"\d{1,2}/\d{2}/\d{2,4}";
            Match match = Regex.Match(text, Patern);
            text = "";
            while (match.Success)
            {
                str_ast = match.Value;
                str_ast = str_ast.Replace("...1358", "\t");
                str_ast = str_ast.Replace("...3074", "\t");
                str_ast = str_ast.Replace("Merchandise", "");
                Match mat1 = Regex.Match(str_ast, DatePatern);
                str_ast = str_ast.Insert(mat1.Value.Length, "\t");
                mat1 = Regex.Match(str_ast, @"-\$");
                if (mat1.Success)
                    str_ast = str_ast.Insert(mat1.Index , "\t");
                text += str_ast + "\n";
                match = match.NextMatch();
            }
            File.WriteAllText(outfilepath, text);
            System.Console.WriteLine(text);
        }
    }
}
