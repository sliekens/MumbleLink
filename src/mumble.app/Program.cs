using System;
using mumblelib;

namespace mumble.app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var file = MumbleLinkFile.CreateOrOpen())
            {
                var frame = file.ReadRaw();
                var length = frame.Length;
                Console.OpenStandardOutput(length).Write(frame, 0, length);
            }
        }
    }
}
