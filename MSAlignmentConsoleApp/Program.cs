using MSAlignmentClassLibrary.Aligner;
using MSAlignmentClassLibrary.Engine;
using MSAlignmentClassLibrary.Finder;
using MSAlignmentClassLibrary.Reader;

using MSAlignmentClassLibrary.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAlignmentConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string basePath = @"C:\Users\Rui Zhang\Downloads\Serum_1_C18_03292019_Ali.raw";
            string alignedPath = @"C:\Users\Rui Zhang\Downloads\Serum_1_C18_04142019.raw";
            string output = @"C:\Users\Rui Zhang\Downloads\aligned2.csv";

            ISequencer sequencer = new SpectraSequencer();

            Dictionary<int, int> baseSeq = sequencer.MakeSequence(basePath);
            Dictionary<int, int> alignedSeq = sequencer.MakeSequence(alignedPath);

            IAligner aligner = new DPAligner();

            Dictionary<int, int> mapping = aligner.Align(baseSeq, alignedSeq);

            try
            {
                FileStream ostrm = new FileStream(output, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(ostrm);
                writer.Write(basePath + ",");
                string temp = "";
                foreach (int i in mapping.OrderBy(x => x.Key).Select(x => x.Key))
                {
                    temp += i.ToString() + ",";
                }
                writer.WriteLine(temp);
                writer.Flush();

                writer.Write(alignedPath + ",");
                temp = "";
                foreach (int i in mapping.OrderBy(x => x.Key).Select(x => x.Value))
                {
                    temp += i.ToString() + ",";
                }
                writer.WriteLine(temp);
                writer.Flush();

            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open file for writing log!");
                Console.WriteLine(e.Message);
            }

            //Console.ReadLine();

        }
    }
}
