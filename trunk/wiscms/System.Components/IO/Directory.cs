
namespace Wis.Toolkit.IO
{
    public class Directory
    {
        /// <summary>
        /// Creates all directories and subdirectories as specified and return false in case of an error.
        /// </summary>
        /// <param name="physicalPath"></param>
        /// <returns></returns>
        public static bool CreateDirectory(string physicalPath)
        {
            bool returnValue = true;

            if (!System.IO.Directory.Exists(physicalPath))
            {
                try { System.IO.Directory.CreateDirectory(physicalPath); }
                catch { returnValue = false; }
            }
            return returnValue;
        }

        //
    }
}
