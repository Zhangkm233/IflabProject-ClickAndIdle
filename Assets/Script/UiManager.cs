using UnityEngine;
using System.Runtime.InteropServices;
using System;
public class UiManager : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd,string lpText,string lpCaption,uint uType);

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("Dwmapi.dll")]
    private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd,ref MARGINS pMarInset);

    public static void GameShutDown() {
        Application.Quit();
    }

    public void Start() {
        //MessageBox(IntPtr.Zero,"Hello World","Hello",0);

        IntPtr hWnd = GetActiveWindow();
        MARGINS margins = new MARGINS { cxLeftWidth = -1};
        DwmExtendFrameIntoClientArea(hWnd,ref margins);

    }
}
