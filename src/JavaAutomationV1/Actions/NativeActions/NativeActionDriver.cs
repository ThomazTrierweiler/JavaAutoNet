using JavaAutoNet.Core.AccessBridgeAPI;
using JavaAutoNet.Core.Actions.NativeActions;
using JavaAutoNet.Core.Enums.NativeActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JavaAutomationV1.Actions.NativeActions
{
    internal class NativeActionDriver : INativeActionDriver
    {
        private Dictionary<string, NativeAction> _nativeActionsDict;

        public NativeActionDriver()
        {
            _nativeActionsDict = new Dictionary<string, NativeAction>()
            {
                { "click", NativeAction.Click },
                { "", NativeAction.Undefined }
            };
        }

        public void DoNativeAction(int vmID, IntPtr referenceJavaObjHandle, NativeAction nativeAction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NativeAction> GetPossibleNativeActions(int vmID, IntPtr referenceJavaObjHandle)
        {
            IntPtr accessibleActionsPtr = IntPtr.Zero;
            try
            {
                //Reserve memory for the accessible actions pointer
                accessibleActionsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new AccessibleActions()));
                //Call DLL.
                AccessBridge.GetAccessibleActions(vmID, referenceJavaObjHandle, accessibleActionsPtr);
                //Creat object
                object? accessibleActionsObj = Marshal.PtrToStructure(accessibleActionsPtr, typeof(AccessibleActions));
                if (accessibleActionsObj == null)
                    return new List<NativeAction>();

                AccessibleActions accessibleActions = (AccessibleActions)accessibleActionsObj;
                string[] actionStrArray = accessibleActions.ActionInfo.Select(a => a.Name).Where(a => a!="").ToArray();
                return actionStrArray.Select(a => _nativeActionsDict.First(i => a.Contains(i.Key)).Value);
            }
            finally
            {
                //Free memory       
                if (accessibleActionsPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(accessibleActionsPtr);
            }
        }

        public void SetText(int vmID, IntPtr referenceJavaObjHandle, string text)
        {
            AccessBridge.SetTextContents(vmID, referenceJavaObjHandle, text);
        }
    }
}
