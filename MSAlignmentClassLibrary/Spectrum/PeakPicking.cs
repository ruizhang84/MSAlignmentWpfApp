using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSAlignmentClassLibrary.Spectrum
{
    public class PeakPicking : ISpectrumProcess
    {
        int maxPeaks;
        public PeakPicking(int maxPeaks = 100)
        {
            this.maxPeaks = maxPeaks;
        }

        public void Process(ISpectrum spectrum)
        {
            List<IPeak> peaks = spectrum.GetPeaks()
                .OrderByDescending(pk => pk.GetIntensity())
                .Take(maxPeaks)
                .OrderBy(pk => pk.GetMZ()).ToList(); ;
            spectrum.SetPeaks(peaks);  
        }
    }
}
