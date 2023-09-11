using JavaAutoNet.Core.Actions.NativeActions;
using JavaAutoNet.Core.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaAutoNet.Core
{
    public interface IJavaAutomation : IDisposable
    {
        /// <summary>
        /// Finds an element in a java window through its xpath.
        /// </summary>
        /// <param name="javaWindow">The java window in which the element is located.</param>
        /// <param name="xpath">The element's xpath from the java window.</param>
        /// <returns></returns>
        IJavaElement? FindJavaElement(IJavaElement javaWindow, string xpath);
        /// <summary>
        /// Finds an element in a java window through its native java object handle/pointer.
        /// </summary>
        /// <param name="javaWindow">The java window in which the element is located.</param>
        /// <param name="javaObjHandle">The native java object handle/pointer</param>
        /// <returns></returns>
        IJavaElement? FindJavaElement(IJavaElement javaWindow, IntPtr javaObjHandle);
        /// <summary>
        /// Returns the parent (if any) of the given java object native handle.
        /// </summary>
        /// <param name="vmID">The vm's id.</param>
        /// <param name="referenceJavaObjHandle">The reference java object's native handle.</param>
        /// <returns></returns>
        IJavaElement? FindParentElement(int vmID, IntPtr referenceJavaObjHandle);
        /// <summary>
        /// Returns the child element at a given index.
        /// </summary>
        /// <param name="vmID">The vm's id.</param>
        /// <param name="referenceJavaObjHandle">The reference java object's native handle.</param>
        /// <param name="childId">The child's index in the reference java object</param>
        /// <returns></returns>
        IJavaElement? FindChildElement(int vmID, IntPtr referenceJavaObjHandle, int childId);
        /// <summary>
        /// Returns a top-level, IJavaElement-marshalled java window through a window handle.
        /// </summary>
        /// <param name="windowHandle">The window's native handle.</param>
        /// <returns></returns>
        IJavaElement? FindJavaWindow(IntPtr windowHandle);
        /// <summary>
        /// Finds a top-level java window that has a matching window title. If more than one window is found, returns the one with the given index.
        /// </summary>
        /// <param name="windowTitle">The title of the window to be searched.</param>
        /// <param name="id">Optional: The id of the desired window if more than one window with a matching title is found.</param>
        /// <returns></returns>
        IJavaElement? FindJavaWindow(string windowTitle, int id = 0);
        /// <summary>
        /// Returns all top-level java windows. Warning: might be slow.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IJavaElement> FindAllJavaWindows();
    }
}
