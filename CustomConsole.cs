using System.Runtime.InteropServices;

namespace cgame
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct _CONSOLE_FONT_INFO_EX
    {
        internal uint cbSize;
        internal uint nFont;
        internal COORD dwFontSize;
        internal int FontFamily;
        internal int FontWeight; 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct COORD
    {
        internal short X;
        internal short Y;
        public COORD(short x, short y)
        {
            X = x;
            Y = y;
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct _CONSOLE_CURSOR_INFO
    {
        internal uint dwSize;
        internal bool bIsVisible;
    }

    internal class CustomConsole
    {
        IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        const int STD_OUTPUT_HANDLE = -11;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int dwType);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(
            IntPtr consoleOutput,
            bool maximumWindow,
            ref _CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetCurrentConsoleFontEx(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            ref _CONSOLE_FONT_INFO_EX console);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleCursorInfo(
            IntPtr hConsoleOutput,
            ref _CONSOLE_CURSOR_INFO lpConsoleCursorInfo
            );

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleCursorInfo(
            IntPtr hConsoleOutput,
            ref _CONSOLE_CURSOR_INFO lpConsoleCursorInfo
            );

        internal void SetConsoleFont(string fontName = "Arial")
        {
            //default font=Consolas
            //Dword fontSize = 7 / 16
            IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
            if (handle != INVALID_HANDLE_VALUE)
            {
                _CONSOLE_FONT_INFO_EX currentConsoleInfo = new _CONSOLE_FONT_INFO_EX();
                currentConsoleInfo.cbSize = (uint)Marshal.SizeOf(currentConsoleInfo);

                GetCurrentConsoleFontEx(handle, false, ref currentConsoleInfo);
                /*
                currentConsoleInfo.FaceName = "Consolas";
                currentConsoleInfo.dwFontSize.X = 7;
                currentConsoleInfo.dwFontSize.Y = 16;

                SetCurrentConsoleFontEx(handle, false, ref currentConsoleInfo);
                */

                _CONSOLE_CURSOR_INFO currentConsoleCursorInfo = new _CONSOLE_CURSOR_INFO();
                
                GetConsoleCursorInfo(handle, ref currentConsoleCursorInfo);
                currentConsoleCursorInfo.dwSize = 25;
                SetConsoleCursorInfo(handle, ref currentConsoleCursorInfo);
                //default: 25
            }
        }
    }
}
