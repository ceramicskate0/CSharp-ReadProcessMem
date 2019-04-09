using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class ReadEXEOuput
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(int hProcess, long lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
   
    const int PROCESS_WM_READ = 0x0010;
   
    public static void Main(string[] args)
    {
        Method1(args);
    }

    public static void Method1(string[] args)//read process memory
    {
        Process process = Process.GetProcessesByName(args[0])[0];
        IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

        IntPtr startOffset = process.MainModule.BaseAddress;
        IntPtr endOffset = IntPtr.Add(startOffset, process.MainModule.ModuleMemorySize);

        int bytesRead = 0;
        byte[] buffer = new byte[process.MainModule.ModuleMemorySize];

        ReadProcessMemory((int)processHandle, (long)startOffset, buffer, buffer.Length, ref bytesRead);
        string Data = Encoding.Unicode.GetString(buffer);
        Console.WriteLine(Data);
    }
}
