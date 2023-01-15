using System.Runtime.InteropServices;

namespace cgame
{
    internal class CustomConsole
    {
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

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleScreenBufferInfo(
            IntPtr hConsoleOutput,
            ref _CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo
            );

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(
            IntPtr handle,
            int nCmdShow);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        
        internal void SetConsole()
        {
            IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
            const int STD_OUTPUT_HANDLE = -11;
            const int SW_SHOWMAXIMIZED = 3;

            IntPtr wConsoleHandle = GetConsoleWindow();
            IntPtr stdoutHandle = GetStdHandle(STD_OUTPUT_HANDLE);

            if (wConsoleHandle == INVALID_HANDLE_VALUE)
                return;//error
            if (stdoutHandle == INVALID_HANDLE_VALUE)
                return;//error

            SetConsoleWindow(wConsoleHandle, SW_SHOWMAXIMIZED);
            SetConsoleCursor(stdoutHandle, 55);
            SetConsoleFont(stdoutHandle, "System", 24, 32);
        }
        private void SetConsoleWindow(IntPtr handle, int mode)
        {
            ShowWindow(handle, mode);
        }
        private void SetConsoleCursor(IntPtr handle, uint size)
        {
            _CONSOLE_CURSOR_INFO currentConsoleCursorInfo = new _CONSOLE_CURSOR_INFO();
            GetConsoleCursorInfo(handle, ref currentConsoleCursorInfo);
            currentConsoleCursorInfo.dwSize = size; //default: 25
            SetConsoleCursorInfo(handle, ref currentConsoleCursorInfo);
        }
        private _CONSOLE_SCREEN_BUFFER_INFO GetConsoleScreenBuffer(IntPtr handle)
        {
            _CONSOLE_SCREEN_BUFFER_INFO currentConsoleScreenBuffer = new _CONSOLE_SCREEN_BUFFER_INFO();
            GetConsoleScreenBufferInfo(handle, ref currentConsoleScreenBuffer);
            return currentConsoleScreenBuffer;
        }
        private void SetConsoleFont(IntPtr handle, string faceName, short width, short height)
        {
            //default font=Consolas
            //Dword fontSize = 7 / 16

            _CONSOLE_FONT_INFO_EX currentConsoleInfo = new _CONSOLE_FONT_INFO_EX();
            currentConsoleInfo.cbSize = (uint)Marshal.SizeOf(currentConsoleInfo);
            GetCurrentConsoleFontEx(handle, false, ref currentConsoleInfo);

            currentConsoleInfo.FaceName = faceName;
            currentConsoleInfo.dwFontSize.X = width;
            currentConsoleInfo.dwFontSize.Y = height;

            SetCurrentConsoleFontEx(handle, false, ref currentConsoleInfo);
        }
    }
}
