using MSAlignmentWpfApp.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAlignmentWpfApp.Reader
{
    public interface ISpectrumReader
    {
        void Init(string fileName);
        int GetFirstScan();
        int GetLastScan();
        int GetMSnOrder(int scanNum);
        ISpectrum GetSpectrum(int scanNum);
    }
}
