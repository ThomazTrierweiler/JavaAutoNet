using JavaAutoNet.Core.Enums.NativeActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaAutoNet.Core.Actions.NativeActions
{
    public interface INativeActionDriver
    {
        /// <summary>
        /// Returns an IEumerable with all the possible actions for a given java element.
        /// </summary>
        /// <param name="vmID">The vm's id.</param>
        /// <param name="referenceJavaObjHandle">The reference java object's native handle.</param>
        /// <returns></returns>
        IEnumerable<NativeAction> GetPossibleNativeActions(int vmID, IntPtr referenceJavaObjHandle);
        /// <summary>
        /// Executes a given native action on the element.
        /// </summary>
        /// <param name="vmID">The vm's id.</param>
        /// <param name="referenceJavaObjHandle">The reference java object's native handle.</param>
        /// <param name="nativeAction">The desired native action to be performed.</param>
        void DoNativeAction(int vmID, IntPtr referenceJavaObjHandle, NativeAction nativeAction);
        /// <summary>
        /// Sets a given text to an element.
        /// </summary>
        /// <param name="vmID">The vm's id.</param>
        /// <param name="referenceJavaObjHandle">The reference java object's native handle.</param>
        /// <param name="text">The text to set on the element.</param>
        void SetText(int vmID, IntPtr referenceJavaObjHandle, string text);
    }
}
