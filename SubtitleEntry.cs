using System;
using System.Collections.Generic;
using System.Globalization;

namespace SRTTimeShifter
{
    /// <summary>
    /// Representation of a SRT file entry
    /// </summary>
    /// 
    public class SubtitleEntry
    {
        #region SRT Entry Representation
        public string timestamp { get; set; }
        public string num { get; set; }
        public List<String> lines { get; set; }
        #endregion SRT Entry Representation

        public SubtitleEntry(string num, string timestamp, List<string> lines)
        {
            this.num = num;
            this.lines = lines;
            this.timestamp = timestamp;
        }

        /// <summary>
        /// Builds the string representation of the given subtitle in SRT format.
        /// </summary>
        /// <returns></returns>
        public string buildString()
        {
            string str = $"{num.ToString()}\n{timestamp}\n";
            foreach (string s in lines)
                str += $"{s}\n";

            return str;
        }

        /// <summary>
        /// Adds millisecods to the given subtitle entry
        /// </summary>
        /// <param name="msOffset">Offset in milliseconds. Can also be negative.</param>
        public void addMs(int msOffset)
        {
            string[] times = timestamp.SplitTrim(" --> ");

            DateTime startDateTime = DateTime.Parse(times[0].Replace(',', '.'),  CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.Parse(times[1].Replace(',', '.'), CultureInfo.InvariantCulture);

            DateTime adjustedStart = startDateTime.AddMilliseconds(msOffset);
            DateTime adjustedEnd = endDateTime.AddMilliseconds(msOffset);

            this.timestamp = $"{adjustedStart.Hour.ToString("00")}:{adjustedStart.Minute.ToString("00")}:{adjustedStart.Second.ToString("00")},{adjustedStart.Millisecond.ToString("000")} --> {adjustedEnd.Hour.ToString("00")}:{adjustedEnd.Minute.ToString("00")}:{adjustedEnd.Second.ToString("00")},{adjustedEnd.Millisecond.ToString("000")}";
        }
    }
}
