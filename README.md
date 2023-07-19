# SRTTimeShifter

Console application for several operating systems that shifts all the time entries in the given SRT subtitle file by a given number of milliseconds.

SRTTimeShifter SrtFile [-]MillisecondsToShift

A new file is created with "_shifted" appended to the file name.

SRTTimeShifter "C:\Stuff\My Little Pony.srt" 5000
will create file C:\Stuff\My Little Pony_shifted.srt  with the times shifted ahead 5 seconds.

SRTTimeShifter "C:\Stuff\Files\My Little Pony.srt" -2000
will create file C:\Stuff\Files\My Little Pony_shifted.srt  with the times shifted back 2 seconds.
