using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMIP6Processor {
    internal class Program {
        static void Main(string[] args) {
            // this program processes CMIP6 annual model data as provided by the university of melbourne
            // https://cmip6.science.unimelb.edu.au/search

            // argument validation
            if (args.Length == 0) { ExitInvalid(); return; }
            string dir = args[0];
            if (string.IsNullOrWhiteSpace(dir)) { ExitInvalid(); return; }
            if (!Directory.Exists(dir)) { ExitInvalid(); return; }
            
            DirectoryInfo dirinfo = new DirectoryInfo(dir);
            DirectoryInfo workingdir = dirinfo;

            if (dirinfo.Name != "average-year-mid-year") {
                bool valid = false;
                foreach (DirectoryInfo i in dirinfo.GetDirectories()) {
                    if (i.Name == "average-year-mid-year") {
                        workingdir = i;
                        valid = true; }}
                if (!valid) { ExitInvalid(); return; }}

            DirectoryInfo models = new DirectoryInfo($"{workingdir.FullName}\\CMIP6\\ScenarioMIP\\");
            List<FileInfo> mags = GetMAGFiles(models);

            Dictionary<int, List<decimal>> annualData = new Dictionary<int, List<decimal>>();

            foreach (FileInfo file in mags) {
                string data = File.ReadAllText(file.FullName);
                data = data.Substring(data.IndexOf("YEARS"));
                data = data.Substring(data.IndexOf("\n") + 1);
                string[] bdata = data.Split('\n');

                foreach (string year in bdata) {
                    string y = year.TrimStart();

                    // get year
                    if (!char.IsDigit(y.ToCharArray()[0])) continue;
                    int ny = Convert.ToInt32(y.Substring(0, y.IndexOf(' ')));
                    
                    // get kelvin
                    y = y.Substring(y.IndexOf(ny.ToString() + ny.ToString().Length + 1)).TrimStart();
                    if (!char.IsDigit(y.ToCharArray()[0])) continue;
                    double kelvin = Convert.ToDouble(y.Substring(0, y.IndexOf('+'))) * 100d;


                }
            }
        }
        
        static List<FileInfo> GetMAGFiles(DirectoryInfo Directory) {
            List<FileInfo> files = new List<FileInfo>();
            foreach (FileInfo file in Directory.GetFiles())
                if (file.Extension == "MAG") files.Add(file);
            foreach (DirectoryInfo dir in Directory.GetDirectories())
                files.AddRange(GetMAGFiles(dir));
            return files; }

        static void ExitInvalid() => Console.WriteLine("Invalid directory argument. CMIP Data not found or invalid.");
        
        }}
