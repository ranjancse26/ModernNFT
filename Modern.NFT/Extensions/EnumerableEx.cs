using System;
using System.Collections.Generic;

namespace Modern.NFT
{
    public static class EnumerableEx
    {
        /// <summary>
        /// https://stackoverflow.com/questions/1450774/splitting-a-string-into-chunks-of-a-certain-size
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkLength"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitBy(this string str,
            int chunkLength)
        {
            if (String.IsNullOrEmpty(str)) throw new ArgumentException();
            if (chunkLength < 1)
                throw new ArgumentException();

            for (int i = 0; i < str.Length; i += chunkLength)
            {
                if (chunkLength + i > str.Length)
                    chunkLength = str.Length - i;

                yield return str.Substring(i, chunkLength);
            }
        }
    }
}
