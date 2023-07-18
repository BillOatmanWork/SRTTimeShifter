namespace SRTTimeShifter
{
    public static class Extensions
    {
        /// <summary>
        /// Split string and trim all of the pieces
        /// </summary>
        /// <param name="data"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static string[] SplitTrim(this string data, string arg)
        {
            string[] ar = data.Split(arg);
            for (int i = 0; i < ar.Length; i++)
            {
                ar[i] = ar[i].Trim();
            }
            return ar;
        }
    }
}
