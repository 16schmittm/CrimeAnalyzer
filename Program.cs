using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Crime_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            String file1;
            String wFile;
            int count=0;
            List<Crimes> list = new List<Crimes>();
            try 
            {
                if (args.Length != 2) 
                {
                    Console.WriteLine("Please correct the format!\n");
                }
                file1=args[0];
                if (File.Exists(file1) == false)
                {
                    Console.WriteLine("File Does Not Exist. ");
                    Environment.Exit(-1);
                }
                using (var reader = new StreamReader(file1))
                {
                    string header = reader.ReadLine();
                    var hValues = header.Split(',');
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        int robbery = Convert.ToInt32(values[0]);
                        int assault = Convert.ToInt32(values[1]);
                        int property = Convert.ToInt32(values[2]);
                        int burglary = Convert.ToInt32(values[3]);
                        int theft = Convert.ToInt32(values[4]);
                        int vehicle = Convert.ToInt32(values[5]);
                        int year = Convert.ToInt32(values[6]);
                        int murder = Convert.ToInt32(values[7]);
                        int rape = Convert.ToInt32(values[8]);
                        int population = Convert.ToInt32(values[9]);
                        int violentcrimes = Convert.ToInt32(values[10]);
                        list.Add(new Crimes(robbery, assault, property, burglary, theft, vehicle, year, murder, rape, population, violentcrimes));
                    }
                }
            }
            catch (Exception)
            {
                Console.Write("Error");
            }
            string report = "";
            var years = from Crimes in list select Crimes.year;
            foreach (var x in years)
            {
                count++;
            }
            var q3Murders = from Crimes in list where Crimes.murder < 15000 select Crimes.year;
            var q4Robberies = from Crimes in list where Crimes.robbery > 500000 select new { Crimes.year, Crimes.robbery };
            var q5Violence = from Crimes in list where Crimes.year == 2010 select Crimes.violentcrimes;
            var q5Capita = from Crimes in list where Crimes.year == 2010 select Crimes.population;
            double a = 0;
            double b = 0;
            foreach (var c in q5Violence)
            {
                a = (double)c;
            }
            foreach (var c in q5Capita)
            {
                b = (double)c;
            }
            double q5Answer = a / b;

            var q6 = from Crimes in list select Crimes.murder;
            double q6Murder = 0;
            foreach (var c in q6)
            {
                q6Murder += c;
            }
            double q6Answer = q6Murder / count;

            var q7 = from Crimes in list where Crimes.year >= 1994 && Crimes.year <= 1997 select Crimes.murder;
            double q7Murder = 0;
            int q7Count = 0;
            foreach (var c in q7)
            {
                q7Murder += c;
                q7Count++;
            }
            double q7Answer = q7Murder / q7Count;

            var q8 = from Crimes in list where Crimes.year >= 2010 && Crimes.year <= 2013 select Crimes.murder;
            double q8Murder = 0;
            int q8Count = 0;
            foreach (var c in q8)
            {
                q8Murder += c;
                q8Count++;
            }
            double q8Answer = q8Murder / q8Count;

            var q9 = from Crimes in list where Crimes.year >= 1999 && Crimes.year <= 2004 select Crimes.theft;
            int q9Answer = q9.Min();
            var q10 = from Crimes in list where Crimes.year >= 1999 && Crimes.year <= 2004 select Crimes.theft;
            int q10Answer = q10.Max();
            var q11 = from Crimes in list select new { Crimes.year, Crimes.vehicle };
            int q11Answer = 0;
            int temp = 0;
            foreach (var c in q11)
            {
                if (c.vehicle > temp)
                {
                    q11Answer = c.year;
                    temp = c.vehicle;
                }
            }
            report += "The Range Of Years Include " + years.Min() + " - " + years.Max() + " (" + count + " years) \n";
            report += "Years Murders Per Year < 15000: ";
            foreach (var c in q3Murders)
            {
                report += c + " ";
            }
            report += "\n";
            report += "Robberies Per Year > 500000: ";
            foreach (var c in q4Robberies)
            {
                report += string.Format("{0} = {1}, ", c.year, c.robbery);
            }
            report += "\n";
            report += "Violent Crime Per Capita Rate (2010): " + q5Answer + "\n";
            report += "Average Murder Per Year (Across All Years): " + q6Answer + "\n";
            report += "Average Murder Per Year (1994 To 1997): " + q7Answer + "\n";
            report += "Average Murder Per Year (2010 To 2013): " + q8Answer + "\n";
            report += "Minimum Thefts Per Year (1999 To 2004): " + q9Answer + "\n";
            report += "Maximum Thefts Per Year (1999 To 2004): " + q10Answer + "\n";
            report += "Year Of Highest Number Of Motor Vehicle Thefts: " + q11Answer + "\n";
            wFile = "Output.txt";

            StreamWriter sw = new StreamWriter(wFile);
            try
            {
                sw.WriteLine(report);
            }
            catch (Exception)
            {
                Console.Write("Error");
            }
            finally
            {
                sw.Close();
            }
        }
    }
    class Crimes
    {
        public int robbery, assault, property, burglary, theft, vehicle, year, murder, rape, population, violentcrimes;
        public Crimes(int robbery, int assault, int property, int burglary, int theft, int vehicle, int year, int murder, int rape, int population, int violentcrimes)
        {
            this.robbery=robbery;
            this.assault=assault;
            this.property=property;
            this.burglary=burglary;
            this.theft=theft;
            this.vehicle=vehicle;
            this.year=year;
            this.murder=murder;
            this.rape=rape;
            this.population=population;
            this.violentcrimes=violentcrimes;
        }
    }   
}