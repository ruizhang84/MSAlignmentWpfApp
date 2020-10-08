using System;
using System.Collections.Generic;
using System.Text;

namespace MSAlignmentClassLibrary.Spectrum
{
    public interface ISpectrumProcess
    {
        void Process(ISpectrum spectrum);
    }
}
