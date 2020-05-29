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
            VideoFrameProvider vfp = new VideoFrameProvider("C:/Users/ellio/Downloads/test.mp4");
        
            // calculate frame time
            Console.WriteLine(vfp.FrameRate + " fps");
            int frameTime = (int)(1000 / vfp.FrameRate);
            Console.WriteLine(frameTime + " ms");

            // initialise ascii filter and framebuffer
            AsciiFilter filter = new AsciiFilter(vfp, (float).2);
            FrameBuffer frameBuffer = new FrameBuffer(filter, 30);

            long startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            string frame = "";
            while (frame != null)
            {
                //Thread.Sleep(frameTime);
                frame = frameBuffer.GetNextFrame();
                notepad.setText(frame);
            }

            Console.WriteLine(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - startTime);
            Thread.Sleep(10000);
        }
    }
}
