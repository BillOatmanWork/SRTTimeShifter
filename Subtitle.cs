﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRTTimeShifter
{
    public class Subtitle
    {
        public string timestamp { get; set; }
        public string num { get; set; }
        public List<String> lines { get; set; }

        public Subtitle(string num, string timestamp, List<string> lines)
        {
            this.num = num;
            this.lines = lines;
            this.timestamp = timestamp;
        }

        public string buildString()
        {
            string str = num + "\n" + timestamp + "\n";
            foreach (string s in lines)
            {
                str += s + "\n";
            }
            return str;
        }

        public void addMs(int msOffset)
        {
            string[] times = timestamp.Split(" --> ");

            DateTime startDateTime = DateTime.Parse(times[0].Replace(',', '.'),  CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.Parse(times[1].Replace(',', '.'), CultureInfo.InvariantCulture);

            DateTime adjustedStart = startDateTime.AddMilliseconds(msOffset);
            DateTime adjustedEnd = endDateTime.AddMilliseconds(msOffset);

            this.timestamp = adjustedStart.Hour.ToString("00") + ":" + adjustedStart.Minute.ToString("00") + ":" + adjustedStart.Second.ToString("00") + "," + adjustedStart.Millisecond.ToString("000") + " --> " + adjustedEnd.Hour.ToString("00") + ":" + adjustedEnd.Minute.ToString("00") + ":" + adjustedEnd.Second.ToString("00") + "," + adjustedEnd.Millisecond.ToString("000");
        }
    }
}