using Accord.Math;
using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadVideoPlayer
{
    class VideoFrameProvider
    {
        private VideoFileReader vfr = new VideoFileReader();

        public double FrameRate
        {
            get
            {
                return vfr.FrameRate.ToDouble();
            }
        }

        public VideoFrameProvider(string filepath)
        {
            vfr.Open(filepath);
        }

        public Bitmap GetNextFrame()
        {
            return vfr.ReadVideoFrame();
        }
    }
}
