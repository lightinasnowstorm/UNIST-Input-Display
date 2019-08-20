//https://blogs.msdn.microsoft.com/toub/2006/05/03/low-level-keyboard-hook-in-c/
//https://github.com/rvknth043/Global-Low-Level-Key-Board-And-Mouse-Hook/blob/master/GlobalLowLevelHooks/KeyboardHook.cs
//I do not claim for this to be my own code.
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Input;


namespace UNISTInputDisplay
{
    class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYUP = 0x105;
        private IntPtr _hookID = IntPtr.Zero;
        private readonly LowLevelKeyboardProc _proc;

        private readonly UNISTKeysInput input;
        public InterceptKeys(UNISTKeysInput Input)
        {
            input = Input;
            _proc = HookCallback;
            _hookID = SetHook(_proc);

        }



        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())

            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }


        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                Keys key = (Keys)Marshal.ReadInt32(lParam);
                int iwParam = wParam.ToInt32();

                if (iwParam == WM_KEYDOWN || iwParam == WM_SYSKEYDOWN || iwParam == WM_KEYUP || iwParam == WM_SYSKEYUP)
                {
                    bool PressState = iwParam == WM_KEYDOWN || iwParam == WM_SYSKEYDOWN;
                    if (key == input.A)
                    {
                        input.APressed = PressState;
                    }
                    else if (key == input.B)
                    {
                        input.BPressed = PressState;
                    }
                    else if (key == input.C)
                    {
                        input.CPressed = PressState;
                    }
                    else if (key == input.D)
                    {
                        input.DPressed = PressState;
                    }
                    else if (key == input.Left)
                    {
                        input.LeftPressed = PressState;
                    }
                    else if (key == input.Right)
                    {
                        input.RightPressed = PressState;
                    }
                    else if (key == input.Up)
                    {
                        input.UpPressed = PressState;
                    }
                    else if (key == input.Down)
                    {
                        input.DownPressed = PressState;
                    }
                }

            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    }
}
