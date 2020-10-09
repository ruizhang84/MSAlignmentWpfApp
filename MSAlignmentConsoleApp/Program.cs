using MSAlignmentClassLibrary.Reader;
using MSAlignmentClassLibrary.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAlignmentConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpectrumReader reader = new ThermoRawSpectrumReader();
            reader.Init(@"C:\Users\Rui Zhang\Downloads\Serum_1_C18_03292019_Ali.raw");
            ISpectrum spectrum = reader.GetSpectrum(542);

            Console.ReadLine();
            //for (int i = reader.GetFirstScan(); i < reader.GetLastScan(); i++)
            //{
            //    if (reader.GetMSnOrder(i) < 2)
            //    {
            //        List<IPeak> peaks = reader.GetSpectrum(i).GetPeaks();
            //    }

            //}

        }
    }
}
