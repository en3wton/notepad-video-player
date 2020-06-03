using Accord;
using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotepadVideoPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting program");
            Notepad notepad = new Notepad();

            // initialise frame provider
            VideoFrameProvider vfp = new VideoFrameProvider("C:/Users/ellio/Downloads/test.mkv");

            // calculate frame time
            Console.WriteLine(vfp.FrameRate + " fps");
            int frameTime = (int)(10000000 / vfp.FrameRate);
            var interval = new TimeSpan(frameTime);

            // initialise ascii filter and framebuffer
            AsciiFilter filter = new AsciiFilter(vfp, (float).05);
            FrameBuffer frameBuffer = new FrameBuffer(filter, 30);

            // measure time
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var nextTick = DateTime.Now + interval;
            string frame = "";
            while (frame != null)
            {
                while(DateTime.Now < nextTick)
                {
                    // do nothing
                }
                nextTick += interval;

                frame = frameBuffer.GetNextFrame();
                notepad.setText(frame);
                nextTick += interval;
            }

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + " milliseconds");

            // keep window open
            Console.ReadLine();
        }
    }
}
