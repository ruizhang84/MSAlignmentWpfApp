using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSAlignmentClassLibrary.Aligner;

namespace MSAlignmentUnitTest
{
    [TestClass]
    public class AlignmentTest
    {
        [TestMethod]
        public void SequenceAlignTest()
        {
            IAligner aligner = new DPAligner(12);
            Dictionary<int, int> seq1 = new Dictionary<int, int>()
            {
                {1, 1}, {2, 2}, {3, 3}
            };

            Dictionary<int, int> seq2 = new Dictionary<int, int>()
            {
                {1, 1}, {2, 12}, {3, 3}
            };

            Dictionary<int, int> map = aligner.Align(seq1, seq2);

            foreach(int key in map.Keys)
            {
                Console.WriteLine(key.ToString() + ": " + map[key].ToString());
            }

        }
    }
}
