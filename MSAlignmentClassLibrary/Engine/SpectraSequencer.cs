using MSAlignmentClassLibrary.Finder;
using MSAlignmentClassLibrary.Reader;
using MSAlignmentClassLibrary.Spectrum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAlignmentClassLibrary.Engine
{
    public class SpectraSequencer : ISequencer
    {
        protected double tol;

        public SpectraSequencer(double ppm=10.0)
        {
            tol = ppm;
        }

        public Dictionary<int, int> MakeSequence(string path)
        {
            Dictionary<int, int> sequence = new Dictionary<int, int>();
            ISpectrumReader reader = new ThermoRawSpectrumReader();
            reader.Init(path);
            ISpectrumProcess process = new PeakPicking();

            IFinder finder = new MSFinder(tol);
            for (int i = reader.GetFirstScan(); i < reader.GetLastScan(); i++)
            {
                if (reader.GetMSnOrder(i) < 2)
                {
                    ISpectrum spectrum = reader.GetSpectrum(i);
                    process.Process(spectrum);
                    int units = finder.FindGlucoseUnits(spectrum);
                    if (units > 0)
                        sequence[i] = units;
                }

            }

            return sequence;
        }
    }
}
