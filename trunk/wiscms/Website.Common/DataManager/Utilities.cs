using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Cs2Gen.BusinessObjects
{
    public class Utilities
    {
        public static byte[] ConvertImageTobyte(string path)
        {
            byte[] result = null;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                result = new byte[fileStream.Length];
                fileStream.Read(result, 0, (int)fileStream.Length);
            }

            return result;
        }

    }
}
