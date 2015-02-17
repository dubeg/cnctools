using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Misc
{
    public static class DialogHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll")]
        static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        private static List<IntPtr> GetAllChildrenWindowHandles(IntPtr hParent)
        {
            List<IntPtr> result = new List<IntPtr>();
            IntPtr prevChild = IntPtr.Zero;
            IntPtr currChild = IntPtr.Zero;
            while (true)
            {
                currChild = FindWindowEx(hParent, prevChild, null, null);
                if (currChild == IntPtr.Zero)
                {
                    break;
                }
                result.Add(currChild);
                prevChild = currChild;
            }
            return result;
        }

        public static void FocusFileDialog(string windowTitle)
        {
            bool windowFound = false;
            while (!windowFound)
            {
                IntPtr od = FindWindow(null, windowTitle);

                //found main dialog
                if (od != IntPtr.Zero)
                {
                    IntPtr od1 = FindWindowEx(od, IntPtr.Zero, "DUIViewWndClassName", null);

                    if (od1 != IntPtr.Zero)
                    {
                        IntPtr od2 = FindWindowEx(od1, IntPtr.Zero, "DirectUIHWND", null);

                        if (od2 != IntPtr.Zero)
                        {
                            List<IntPtr> results = GetAllChildrenWindowHandles(od2);

                            results.ForEach(hwd =>
                            {
                                IntPtr od3 = FindWindowEx(hwd, IntPtr.Zero, "SHELLDLL_DefView", null);

                                if (od3 != IntPtr.Zero)
                                {
                                    IntPtr found = FindWindowEx(od3, IntPtr.Zero, "DirectUIHWND", null);

                                    if (found != IntPtr.Zero)
                                    {
                                        SetForegroundWindow(found);
                                        SetFocus(found);
                                        windowFound = true;
                                    }
                                }
                            });
                        }
                    }
                }
            }
        }
    }
}
