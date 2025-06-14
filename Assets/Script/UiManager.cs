using UnityEngine;
using System.Runtime.InteropServices;
using System;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{
    //有bug，我不知道怎么修

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

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd,int nIndex,long dwNewLong);

    [DllImport("user32.dll",SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd,IntPtr hWndInsertAfter,int X,int Y,int cx,int cy,uint uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd,uint crKey,byte bAlpha,uint dwFlags);

    const int GWL_EXSTYLE = -20;

    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const uint LWA_COLORKEY = 0x00000001;
    private IntPtr hWnd;
    public static void GameShutDown() {
        Application.Quit();
    }

    public static void GameStart()
    {
        SceneManager.LoadScene("ClickIdle");
    }

    public void Start()
    {
        //MessageBox(IntPtr.Zero,"Hello World","Hello",0);
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();
        MARGINS margins = new MARGINS { cxLeftWidth = -1};
        DwmExtendFrameIntoClientArea(hWnd,ref margins);
        SetWindowLong(hWnd,GWL_EXSTYLE,WS_EX_LAYERED);
        SetLayeredWindowAttributes(hWnd,0,0,LWA_COLORKEY);
        SetWindowPos(hWnd,HWND_TOPMOST,0,0,0,0,0);
#endif
        Application.runInBackground = true;
    }
}
