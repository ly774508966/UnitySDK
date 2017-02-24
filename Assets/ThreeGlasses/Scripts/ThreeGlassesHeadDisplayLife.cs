﻿//#define VR_SHOW
using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Runtime.InteropServices;


namespace ThreeGlasses
{
    public class ThreeGlassesHeadDisplayLife : MonoBehaviour
    {
        [DllImport("3GlassesTracker.dll")]
        static extern void main();

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        static extern IntPtr SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern IntPtr ShowWindow(IntPtr hwnd, int cmdShow);

        [DllImport("3GlassesTracker")]
        static extern int HMDPresent(ref bool hmdPresent);


        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);     [DllImport("user32.dll")]

        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpEnumFunc, IntPtr lParam);


        private const string UnityWindowClassName = "UnityWndClass";

#if !UNITY_EDITOR
        private IntPtr _windowHandle = IntPtr.Zero;
        private const int SW_SHOWNORMAL = 1;
#endif

        void Awake ()
        {
#if !UNITY_EDITOR
            var threadId = GetCurrentThreadId();
            EnumThreadWindows(threadId, (hWnd, lParam) =>
            {
                var classText = new StringBuilder(UnityWindowClassName.Length + 1);
                GetClassName(hWnd, classText, classText.Capacity);
                if (classText.ToString() != UnityWindowClassName)
                    return true;
                _windowHandle = hWnd;
                return false;
            }, IntPtr.Zero);
#endif
            bool result = false;
            HMDPresent(ref result);

            ThreeGlassesUtils.Log("ThreeGlassesHeadDisplayLife init");

            ThreeGlassesDllInterface.SZVRPluginInit();

#if !UNITY_EDITOR
            if (_windowHandle != IntPtr.Zero)
            {
                ShowWindow(_windowHandle, SW_SHOWNORMAL);
                SetForegroundWindow(_windowHandle);
            }
#endif
        }

        void OnApplicationQuit()
        {
            ThreeGlassesUtils.Log("ThreeGlassesHeadDisplayLife application quit");
            ThreeGlassesDllInterface.SZVRPluginDestroy();
        }
    }
}
