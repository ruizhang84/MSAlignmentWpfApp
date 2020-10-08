using MSAlignmentWpfApp.Spectrum;
using MSFileReaderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAlignmentWpfApp.Reader
{
    public class ThermoRawSpectrumReader : ISpectrumReader
    {
        protected IXRawfile5 rawConnect;

        public ThermoRawSpectrumReader()
        {
            rawConnect = new MSFileReader_XRawfile() as IXRawfile5;
        }

        ~ThermoRawSpectrumReader()
        {
            rawConnect.Close();
        }

        public int GetFirstScan()
        {
            int startScanNum = 0;
            rawConnect.GetFirstSpectrumNumber(ref startScanNum);

            return startScanNum;
        }

        public int GetLastScan()
        {
            int lastScanNum = 0;
            rawConnect.GetLastSpectrumNumber(ref lastScanNum);
            return lastScanNum;
        }

        public int GetMSnOrder(int scanNum)
        {
            int msnOrder = 0;
            rawConnect.GetMSOrderForScanNum(scanNum, ref msnOrder);
            return msnOrder;
        }

        public ISpectrum GetSpectrum(int scanNum)
        {
            ISpectrum spectrum = new GeneralSpectrum(scanNum);

            string szFilter = "";
            int pnScanNumber = scanNum;
            int nIntensityCutoffType = 0;
            int nIntensityCutoffValue = 0;
            int nMaxNumberOfPeaks = 0;
            int bCentroidResult = 0;
            int pnArraySize = 0;
            double pdCentroidPeakWidth = 0;
            object pvarMassList = null;
            object pvarPeakFlags = null;

            rawConnect.GetMassListFromScanNum(ref pnScanNumber, szFilter, nIntensityCutoffType, nIntensityCutoffValue,
                nMaxNumberOfPeaks, bCentroidResult, ref pdCentroidPeakWidth, ref pvarMassList, ref pvarPeakFlags, ref pnArraySize);

            ////construct peaks
            double[,] value = (double[,])pvarMassList;
            for (int pn = 0; pn < pnArraySize; pn++)
            {
                double mass = value[0, pn];
                double intensity = value[1, pn];
                if (intensity > 0)
                {
                    spectrum.Add(new GeneralPeak(mass, intensity));
                }
            }

            return spectrum;
        }

        public void Init(string fileName)
        {
            rawConnect.Open(fileName);
            rawConnect.SetCurrentController(0, 1);
        }
    }
}
