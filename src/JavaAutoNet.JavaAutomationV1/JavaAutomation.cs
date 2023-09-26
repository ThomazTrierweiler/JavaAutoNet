using JavaAutoNet.Core;
using JavaAutoNet.Core.AccessBridgeAPI;
using JavaAutoNet.Core.Actions.NativeActions;
using JavaAutoNet.Core.Elements;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.Xml.Linq;
using System.Reflection;
using JavaAutomationV1.Actions.NativeActions;
using JavaAutoNet.Core.WindowsAPI;

namespace JavaAutomationV1
{
    public class JavaAutomation : IJavaAutomation
    {
        private readonly INativeActionDriver _nativeActionDriver;
        private bool disposedValue;

        public JavaAutomation()
        {
            AccessBridge.WindowsRun();
            _nativeActionDriver = new NativeActionDriver();
        }

        public IEnumerable<IJavaElement> FindAllJavaWindows()
        {
            List<IntPtr> nativeWindowHandles = new();
            GCHandle? gcHandler = null;
            try
            {
                User32.EnumWindowsProc enumWindowsFunc = (IntPtr hWnd, int lParam) => { nativeWindowHandles.Add(hWnd); return true; };
                gcHandler = GCHandle.Alloc(enumWindowsFunc);
                User32.EnumWindows(enumWindowsFunc, 0);
            }
            catch (Exception e)
            {
                throw;
            }
            finally 
            { 
                gcHandler?.Free(); 
            }

            List<IJavaElement> possibleJavaWindows = new List<IJavaElement>();
            foreach(IntPtr nativeWindowHandle in nativeWindowHandles)
            {
                IJavaElement? possibleJavaWindow = FindJavaWindow(nativeWindowHandle);
                if (possibleJavaWindow != null)
                    possibleJavaWindows.Add(possibleJavaWindow);
            }
            return possibleJavaWindows;
        }

        public IJavaElement? FindChildElement(int vmID, IntPtr referenceJavaObjHandle, int childId)
        {
            IntPtr acChildPtr = AccessBridge.GetAccessibleChildFromContext(vmID, referenceJavaObjHandle, childId);
            return GetJavaElementFromNativeHandle(vmID, acChildPtr);
        }

        public IJavaElement? FindJavaElement(IJavaElement javaWindow, string xpath)
        {
            List<string> xpathSections = xpath.Split('/').ToList();
            return FindJavaElementRecursively(javaWindow, xpathSections, false);
        }

        private IJavaElement? FindJavaElementRecursively(IJavaElement currElement, List<string> xpathSections, bool shouldDisposeCurrentElement = true)
        {
            if (xpathSections.Count == 0)
                return currElement;

            string currXPathSection = xpathSections.First();
            string currXPathSectionRole = new string(currXPathSection.Where(c => char.IsLetter(c) | c==' ').ToArray());

            int currXPathSectionRoleIndex = int.Parse(new string(currXPathSection.Where(c => char.IsDigit(c)).ToArray()));

            List<IJavaElement> children = currElement.GetChildren().ToList();
            List<IJavaElement> sameRoleChildren = new List<IJavaElement>();
            foreach (var child in children)
            {
                if (child.Role == currXPathSectionRole)
                    sameRoleChildren.Add(child);
            }
            try
            {
                if (sameRoleChildren.Count <= currXPathSectionRoleIndex)
                    return null;

                IJavaElement childElement = sameRoleChildren[currXPathSectionRoleIndex];
                xpathSections.RemoveAt(0);
                children.Remove(childElement);
                return FindJavaElementRecursively(childElement, xpathSections, true);
            }
            finally
            {
                foreach (var child in children)
                    child.Dispose();

                if (shouldDisposeCurrentElement)
                    currElement.Dispose();
            }
        }

        public IJavaElement? FindJavaElement(IJavaElement javaWindow, IntPtr javaObjHandle)
        {
            return GetJavaElementFromNativeHandle(javaWindow.VmID, javaObjHandle);
        }

        public IJavaElement? FindJavaWindow(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero | AccessBridge.IsJavaWindow(windowHandle) != 1)
                return null;

            AccessBridge.GetAccessibleContextFromHWND(windowHandle, out Int32 vmID, out IntPtr javaWindowHandle);
            if(vmID==0 | javaWindowHandle==IntPtr.Zero)
                return null;

            return GetJavaElementFromNativeHandle(vmID, javaWindowHandle);
        }

        private IJavaElement? GetJavaElementFromNativeHandle(int vmID, IntPtr javaObjHandle)
        {
            IntPtr acInfoPtr = IntPtr.Zero;
            try
            {
                acInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new AccessibleContextInfo()));
                Marshal.StructureToPtr(new AccessibleContextInfo(), acInfoPtr, true);
                AccessBridge.GetAccessibleContextInfo(vmID, javaObjHandle, acInfoPtr);
                object? accessibleContextInfoObj = Marshal.PtrToStructure(acInfoPtr, typeof(AccessibleContextInfo));
                if (accessibleContextInfoObj == null)
                    return null;

                AccessibleContextInfo accessibleContextInfo = (AccessibleContextInfo)accessibleContextInfoObj;
                string elementText = (accessibleContextInfo.AccessibleText) ? GetTextFromAccessibleContext(vmID, javaObjHandle) : "";
                return new JavaElement(javaObjHandle, vmID, _nativeActionDriver, this, accessibleContextInfo, elementText);
            }
            finally
            {
                if (acInfoPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(acInfoPtr);
            }
        }

        private string GetTextFromAccessibleContext(int vmID, IntPtr javaObjHandle)
        {
            //Reserve memory for the accessible text items info pointer
            IntPtr atiiPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new AccessibleTextItemsInfo()));
            //Call DLL.
            AccessBridge.GetAccessibleTextItems(vmID, javaObjHandle, atiiPtr, 0);
            //Creat object
            object? accessibleTextItemsInfoObj = Marshal.PtrToStructure(atiiPtr, typeof(AccessibleTextItemsInfo));
            if (accessibleTextItemsInfoObj == null)
                return "";
            AccessibleTextItemsInfo atii = (AccessibleTextItemsInfo)accessibleTextItemsInfoObj;
            //Free memory       
            if (atiiPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(atiiPtr);

            return atii.Sentence;
        }

        public IJavaElement? FindJavaWindow(string windowTitle, int id = 0)
        {
            List<IntPtr> possibleWindowHandles = new List<IntPtr>();
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(windowTitle))
                    possibleWindowHandles.Add(pList.MainWindowHandle);
            }

            if (possibleWindowHandles.Count <= id) 
                return null;

            return FindJavaWindow(possibleWindowHandles[id]);
        }

        public IJavaElement? FindParentElement(int vmID, IntPtr referenceJavaObjHandle)
        {
            IntPtr acParentPtr = AccessBridge.GetAccessibleParentFromContext(vmID, referenceJavaObjHandle);
            return GetJavaElementFromNativeHandle(vmID, acParentPtr);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~JavaAutomationV1()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}