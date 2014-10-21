using System.Collections.Generic;
using System.IO;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public static class TextReaderExtension
    {
        public static IEnumerable<string[]> GetSplittedLines(this TextReader txtreader, char splitted)
        {
            string[] line = txtreader.ReadToEnd().Split('\n');

            foreach (var s in line)
            {
                yield return s.Split(splitted);
            }
        }
    }
}
