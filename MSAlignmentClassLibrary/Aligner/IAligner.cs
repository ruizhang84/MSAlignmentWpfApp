using System;
using System.Collections.Generic;
using System.Text;

namespace MSAlignmentClassLibrary.Aligner
{
    public interface IAligner
    {
        // inputs Dict<scan-num, glucose-units>
        // outputs: Dict<scan-num1 : scan-num2>
        Dictionary<int, int> Align(Dictionary<int, int> seq1, Dictionary<int, int> seq2);
    }
}
