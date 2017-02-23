
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace ThreeGlasses
{
    public class ThreeGlassesDllInterface
    {
        private const string Dllname = "SZVRUnityPlugin";

        // hmd ---------------------------------------------------------------------------------------
        // init & destroy
        [DllImport(Dllname)]
        public static extern void SZVRPluginInit();
        [DllImport(Dllname)]
        public static extern void SZVRPluginDestroy();

        // self attr
        [DllImport(Dllname)]
        public static extern uint SZVR_GetHMDConnectionStatus(ref bool result);
        [DllImport(Dllname)]// IntPtr must Marshal.AllocHGlobal(64), Marshal.PtrToStringAnsi to string
        public static extern uint SZVR_GetHMDDevName(System.IntPtr name);
        // if light
        [DllImport(Dllname)]
        public static extern uint SZVR_GetHMDPresent(ref bool result);

        // ATW
        [DllImport(Dllname)]
        public static extern void SZVRPluginEnableATW();
        [DllImport(Dllname)]
        public static extern void SZVRPluginDiasbleATW();

        // hmd rotation & position
        [DllImport(Dllname)]
        public static extern uint SZVR_GetHMDRotate(float[] rotate);//3
        [DllImport(Dllname)]
        public static extern uint SZVR_GetHMDPos(float[] pos);//4

        // hmd touchpad
        [DllImport(Dllname)]
        public static extern uint SZVR_GetHMDTouchpad(byte[] result);//2

        // hmd button
        [DllImport(Dllname)]
        public static extern uint SZVR_GetHMDMenuButton(ref bool result);
        [DllImport(Dllname)]
        public static extern uint SZVR_GetHMDExitButton(ref bool result);


        // wand --------------------------------------------------------------------------------
        // status
        [DllImport(Dllname)]
        public static extern uint SZVR_GetWandConnectionStatus(byte[] status);

        // rotate & position
        [DllImport(Dllname)]
        public static extern uint SZVR_GetWandRotate(float[] result);//8
        [DllImport(Dllname)]
        public static extern uint SZVR_GetWandPos(float[] result);//6

        // button
        /*
          first wand
          0 Menu button;
          1 Back button;
          2 Left handle button;
          3 Right handle button;
          4 Trigger press down;
          5 Trigger press all the way down;
          second wand
          6 Menu button;
          7 Back button;
          8 Left handle button;
          9 Right handle button;
          10 Trigger press down;
          11 Trigger press all the way down;
        */
        [DllImport(Dllname)]
        public static extern uint SZVR_GetWandButton(byte[] result);

        // trigger
        [DllImport(Dllname)]
        public static extern uint SZVR_GetWandTriggerProcess(byte[] result); //2
        // stick
        [DllImport(Dllname)]
        public static extern uint SZVR_GetWandStick(byte[] result);//4


        // render ------------------------------------------------------------------------------
        [DllImport(Dllname)]
        public static extern void GetRenderSize(uint[] bufferSize);

        [DllImport(Dllname)]
        public static extern void UpdateTextureFromUnity(
                                                         System.IntPtr leftIntPtr,
                                                         System.IntPtr rigthIntPtr);
        [DllImport(Dllname)]
        public static extern void StereoRenderBegin();

        [DllImport(Dllname)]
        public static extern System.IntPtr GetRenderEventFunc();

        [DllImport(Dllname)]
        public static extern float GetHMDRenderFov();


		// 3glasses sever -------------------------------------------------------------------------
        [DllImport(Dllname)]
		public static extern uint szvrInitDevices();


		[DllImport(Dllname)]
		public static extern uint szvrShutdownDevices();

    }

}
