using MSAlignmentClassLibrary.Spectrum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAlignmentClassLibrary.Finder
{
    public interface IFinder
    {
        double GetTolerance();
        void SetTolerance(double ppm);
        int FindGlucoseUnits(ISpectrum spectrum);

    }
}
