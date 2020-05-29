using Accord.Video.FFMPEG;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace NotepadVideoPlayer
{
    class FrameBuffer
    {
        private int bufferSize;
        private BlockingCollection<string> buff;
        private AsciiFilter asciiFilter;

        private bool stop;

        public FrameBuffer(AsciiFilter asciiFilter, int bufferSize)
        {
            this.bufferSize = bufferSize;
            this.asciiFilter = asciiFilter;
            buff = new BlockingCollection<string>(bufferSize);

            ThreadStart threadRef = new ThreadStart(bufferFrames);
            Thread thread = new Thread(threadRef);
            thread.Start();
        }

        public string GetNextFrame()
        {
            if (!buff.IsCompleted)
            {
                return buff.Take();
            }
            return null;
        }

        private void bufferFrames()
        {
            while (!stop)
            {
                while (!stop && buff.Count < bufferSize)
                {
                    string frame = asciiFilter.GetNextFrame();
                    if (frame != null)
                    {
                        buff.Add(asciiFilter.GetNextFrame());
                    }
                    else
                    {
                        stop = true;
                        buff.CompleteAdding();
                    }

                }
            }
        }
    }
}
