using System;
using System.Collections.Generic;
using System.Text;

namespace MSAlignmentClassLibrary.Engine
{
    public interface ISequencer
    {
        Dictionary<int, int> MakeSequence(string path);
    }
}
