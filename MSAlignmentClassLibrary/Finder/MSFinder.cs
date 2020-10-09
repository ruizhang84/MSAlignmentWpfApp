using System;
using System.Collections.Generic;
using System.Text;
using MSAlignmentClassLibrary.Spectrum;
using MSAlignmentClassLibrary.Util;

namespace MSAlignmentClassLibrary.Finder
{
    public class MSFinder : IFinder
    {
        public MSFinder(double ppm)
        {
            tol = ppm;
        }

        public static readonly double[] Glucose 
            = { 470.2727, 674.3725, 878.4723, 1082.572, 1286.6718, 1490.7716,
            1694.8714, 1898.9711, 2103.0709, 2307.1707, 2511.2704};

        protected double tol = 10.0;

        public double GetTolerance()
        {
            return tol;
        }

        public void SetTolerance(double ppm)
        {
            tol = ppm;
        }

        private int Compare(IPeak peak, double mz)
        {
            double ppm = IonMassCalc.Instance.ComputePPM(mz, peak.GetMZ());
            if (ppm < tol) return 0;
            if (peak.GetMZ() > mz) return 1;
            return -1;
        }

        // 
        private int ExtendSearchPoints(List<IPeak> peaks, double mz, int index)
        {
            int best = index;
            int left = index;
            while (left > 0 && Compare(peaks[--left], mz) == 0)
            {
                if (peaks[best].GetIntensity() < peaks[left].GetIntensity())
                {
                    best = left;
                }
            }
            int right = index;
            while (right < peaks.Count-1 && Compare(peaks[++right], mz) == 0)
            {
                if (peaks[best].GetIntensity() < peaks[right].GetIntensity())
                {
                    best = right;
                }
            }
            return best;
        }

        private int BinarySearchPoints(List<IPeak> peaks, double mz)
        {
            int start = 0;
            int end = peaks.Count - 1;

            while (start <= end)
            {
                int mid = end + (start - end) / 2;
                int cmp = Compare(peaks[mid], mz);
                if (cmp == 0)
                {
                    return ExtendSearchPoints(peaks, mz, mid);
                }
                else if (cmp > 0)
                {
                    end = mid - 1;
                }
                else
                {
                    start = mid + 1;
                }
            }

            return -1;
        }
  
        public int FindGlucoseUnits(ISpectrum spectrum)
        {
            List<IPeak> peaks = spectrum.GetPeaks();

            double bestIntensity = 0;
            int units = 0;

            for(int i = 0; i < Glucose.Length; i++)
            {
                double mass = Glucose[i];
                List<double> mzCandidates = IonMassCalc.Instance.ComputeMZ(mass);
                foreach(double mz in mzCandidates)
                {
                    int idx = BinarySearchPoints(peaks, mz);
                    if (idx > 0 && peaks[idx].GetIntensity() > bestIntensity)
                    {
                        bestIntensity = peaks[idx].GetIntensity();
                        units = i + 2;
                    }
                }
            }

            return units;
        }
    }
}
