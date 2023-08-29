using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JavaAutoNet.Core.AccessBridgeAPI
{
    public static class AccessBridge
    {
        public const string WinAccessBridgeDll = @"WindowsAccessBridge-64.dll";

        #region Access Bridge Funcs To C# Methods

        /// <summary>
        /// Inits the JAB.
        /// </summary>
        public static void WindowsRun()
        {
            Windows_run();
        }

        public static unsafe int IsJavaWindow(IntPtr hwnd)
        {
            return isJavaWindow(hwnd);
        }

        public static void ReleaseJavaObject(Int32 vmID, IntPtr javaObject)
        {
            releaseJavaObject(vmID, javaObject);
        }

        public static void SetTextContents(Int32 vmID, IntPtr ac, [MarshalAs(UnmanagedType.LPWStr)] string text)
        {
            setTextContents(vmID, ac, text);
        }

        public static unsafe bool GetAccessibleContextFromHWND(IntPtr hwnd, out Int32 vmID, out IntPtr ac)
        {
            return getAccessibleContextFromHWND(hwnd, out vmID, out ac);
        }

        public static unsafe IntPtr GetAccessibleChildFromContext(Int32 vmID, IntPtr ac, Int32 index)
        {
            return getAccessibleChildFromContext(vmID, ac, index);
        }

        public static unsafe IntPtr GetAccessibleParentFromContext(Int32 vmID, IntPtr ac)
        {
            return getAccessibleParentFromContext(vmID, ac);
        }

        public static unsafe bool GetAccessibleContextInfo(Int32 vmID, IntPtr accessibleContext, IntPtr acInfo)
        {
            return getAccessibleContextInfo(vmID, accessibleContext, acInfo);
        }

        public static unsafe bool GetAccessibleTextItems(Int32 vmID, IntPtr AccessibleContext, IntPtr textItems, Int32 index)
        {
            return getAccessibleTextItems(vmID, AccessibleContext, textItems, index);
        }

        public static bool DoAccessibleActions(Int32 vmID, IntPtr AccessibleContext, IntPtr actionsToDo, ref Int32 failure)
        {
            return doAccessibleActions(vmID, AccessibleContext, actionsToDo, ref failure);
        }
        #endregion

        #region Access Bridge Funcs

        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static void Windows_run();

        //Checks if window is JavaWindow.
        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static unsafe Int32 isJavaWindow(IntPtr hwnd);

        //Releases the specified java object.
        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static void releaseJavaObject(Int32 vmID, IntPtr javaObject);

        //Sets the text of the given accessible context.
        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static void setTextContents(Int32 vmID, IntPtr ac, [MarshalAs(UnmanagedType.LPWStr)] string text);

        //Returns ac from window handle.
        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static unsafe Boolean getAccessibleContextFromHWND(IntPtr hwnd, out Int32 vmID, out IntPtr ac);

        //Returns an AccessibleContext object that represents the nth child of the object ac, where n is specified by the value index.
        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static unsafe IntPtr getAccessibleChildFromContext(Int32 vmID, IntPtr ac, Int32 index);

        //Returns an AccessibleContext object that represents the parent of the specified object.
        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static unsafe IntPtr getAccessibleParentFromContext(Int32 vmID, IntPtr ac);

        //Returns detailed information about an AccessibleContext object belonging to the JVM
        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static unsafe Boolean getAccessibleContextInfo(Int32 vmID, IntPtr accessibleContext, IntPtr acInfo);

        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static unsafe Boolean getAccessibleTextItems(Int32 vmID, IntPtr AccessibleContext, IntPtr textItems, Int32 index);

        [DllImport(WinAccessBridgeDll, SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        private extern static Boolean doAccessibleActions(Int32 vmID, IntPtr AccessibleContext, IntPtr actionsToDo, ref Int32 failure);
        #endregion
    }
}
