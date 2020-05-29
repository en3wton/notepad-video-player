using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotepadVideoPlayer
{
    class Notepad
    {
        const uint WM_SETTEXT = 0x000C;

        private IntPtr editWindow;

        public Notepad()
        {
            // get notepad parent window
            IntPtr window = FindWindowA("Notepad", null);
            Console.WriteLine("notepad window: " + window);

            // get edit window from notepad parent
            editWindow = FindWindowExA(window, IntPtr.Zero, "Edit", null);
            Console.WriteLine("edit window: " + editWindow);

            if(editWindow.ToInt32() == 0)
            {
                throw new FileNotFoundException("notepad not open");
            }
        }

        // set notepad window text
        public void setText(string text)
        {
            if(editWindow != null)
            {
                SendMessage(editWindow, WM_SETTEXT, IntPtr.Zero, text);
            }
        }

        [DllImport("User32.dll")]
        static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("User32.dll")]
        static extern IntPtr FindWindowExA(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, string lpczWindow);

        [DllImport("User32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);
    }
}
