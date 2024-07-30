using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class TransparentWindow : MonoBehaviour
{
    [DllImport("User32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("User32.dll")]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint flags);


    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    private IntPtr hWnd;

    private const int SW_HIDE = 0x00;
    private const int SW_SHOW = 0x05;
    private const int WS_EX_TOOLWINDOW = 0x0080;

    const int GWL_EXSTYLE = -20;
    const uint WS_EX_Layerd = 0x00080000;
    const uint WS_EX_Transparent = 0x00000020;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    void Start()
    {
        //MessageBox(new IntPtr(0), "Hello World!", "Hello Dialog", 0);

#if !UNITY_EDITOR

        this.hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(this.hWnd, ref margins);

        SetWindowPos(this.hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
        SetWindowLong(this.hWnd, GWL_EXSTYLE, WS_EX_Layerd | WS_EX_Transparent);
#endif
        Application.runInBackground = true;
    }

    //public static int ExecuteCommand(string Command, int Timeout)
    //{
    //    int exitCode;
    //    var processInfo = new ProcessStartInfo("cmd.exe", "/C " + Command);
    //    processInfo.CreateNoWindow = true;
    //    processInfo.UseShellExecute = false;
    //    Process process = Process.Start(processInfo);
    //    process.WaitForExit(Timeout);
    //    exitCode = process.ExitCode;
    //    process.Close();
    //    return exitCode;
    //}


    private void Update()
    {
#if !UNITY_EDITOR
        SetClickThrough(Physics2D.OverlapPoint(GetMouseWorldPosition()) == null);
#endif
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    private void SetClickThrough(bool clickThrough)
    {
        if (clickThrough)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_Layerd | WS_EX_Transparent);
        }
        else
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_Layerd);
        }
    }
}
