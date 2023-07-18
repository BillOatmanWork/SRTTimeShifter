using System;
using System.Collections.Generic;
using System.IO;

namespace SRTTimeShifter
{
    public class SRTTimeShifter
    {
        private List<SubtitleEntry> subtitles;

        static void Main(string[] args)
        {
            SRTTimeShifter p = new SRTTimeShifter();
            p.RealMainAsync(args);
        }

        public void RealMainAsync(string[] args)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            Console.WriteLine($"{asm.GetName().Name} {asm.GetName().Version}");

            string srtFile = string.Empty;
            int shiftMS = 0;

            if (args.Length == 2)
            {
                srtFile = args[0];

                bool successShift = int.TryParse(args[1], out shiftMS);
                if(!successShift) 
                {
                    Console.WriteLine($"Could not parse missilsecond shif {args[1]} to an integer.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("SRTTimeShifter SrtFile [-]MillisecondsToShift");
                return;
            }

            subtitles = new List<SubtitleEntry>();

            Console.WriteLine($"Loading original file {srtFile}");

            bool fileRead = LoadFile(srtFile);
            if (!fileRead)
                return;

            foreach (SubtitleEntry s in subtitles)
                s.addMs(shiftMS);

            string directoryName = Path.GetDirectoryName(srtFile);
            string fileName = Path.GetFileNameWithoutExtension(srtFile);
            string extension = Path.GetExtension(srtFile);
            string newFilwName = Path.Combine(directoryName, fileName + "_shifted" + extension);

            bool fileWrite = SaveFile(newFilwName);
            if (!fileWrite)
                return;

            Console.WriteLine($"Original file {srtFile} shifted {shiftMS.ToString()} miliseconds and saved as {newFilwName}.");
        }

        private bool LoadFile(string srtFile)
        {
            bool retval = true;
            try
            {
                using (StreamReader file = new StreamReader(srtFile))
                {
                    string line;
                    while (true)
                    {
                        line = file.ReadLine();
                        if (line == null)
                        {
                            break;
                        }

                        if (line.Length == 0)
                            continue;

                        if (onlyDigits(line))
                        {
                            string timestamp = file.ReadLine();
                            string ll = file.ReadLine();
                            List<string> lines = new List<string>();
                            while (ll != null && ll.Length != 0)
                            {
                                lines.Add(ll);
                                ll = file.ReadLine();
                            }

                            subtitles.Add(new SubtitleEntry(line, timestamp, lines));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception loading original file {ex.Message}");
                retval = false;
            }

            return retval;
        }

        private bool SaveFile(string srtFile)
        {
            bool retval = true;

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(srtFile))
                {
                    foreach (SubtitleEntry sub in subtitles)
                    {
                        streamWriter.WriteLine(sub.buildString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception writing shifted file {ex.Message}");
                retval = false;
            }

            return retval;
        }

        private bool onlyDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            return true;
        }
    }

  
}
