using JavaAutoNet.Core.AccessBridgeAPI;
using JavaAutoNet.Core.Actions.NativeActions;
using JavaAutoNet.Core.Enums.NativeActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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
                { "page-up", NativeAction.PageUp },
                { "page-down", NativeAction.PageDown },
                { "caret-forward", NativeAction.CaretForward },
                { "caret-backward", NativeAction.CaretBackward },
                { "selection-down", NativeAction.SelectionDown },
                { "selection-up", NativeAction.SelectionUp },
                { "select-line", NativeAction.SelectLine },
                { "unselect", NativeAction.Unselect },
                { "copy-to-clipboard", NativeAction.CopyToClipboard },
                { "past-from-clipboard", NativeAction.PasteFromClipboard },
                { "insert-content", NativeAction.InsertContent },
                { "select-word", NativeAction.SelectWord },
                { "", NativeAction.Undefined }
            };
        }

        public void DoNativeAction(int vmID, IntPtr referenceJavaObjHandle, NativeAction nativeAction)
        {
            IntPtr accessibleActionsToDoPTR = IntPtr.Zero;
            try
            {
                if (nativeAction == NativeAction.Undefined)
                    throw new NotImplementedException();

                AccessibleActionsToDo accessibleActionsToDo = new AccessibleActionsToDo();
                accessibleActionsToDo.ActionsCount = 1;
                accessibleActionsToDo.Actions = new AccessibleActionInfo[32];
                string action = _nativeActionsDict.First(i => nativeAction == i.Value).Key;
                accessibleActionsToDo.Actions[0].Name = action;
                accessibleActionsToDoPTR = Marshal.AllocHGlobal(Marshal.SizeOf(accessibleActionsToDo));
                Marshal.StructureToPtr(accessibleActionsToDo, accessibleActionsToDoPTR, true);
                int failure = -1;
                bool result = AccessBridge.DoAccessibleActions(vmID, referenceJavaObjHandle, accessibleActionsToDoPTR, ref failure);
                if (failure >= 0)
                    throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally 
            {
                //Free memory       
                if (accessibleActionsToDoPTR != IntPtr.Zero)
                    Marshal.FreeHGlobal(accessibleActionsToDoPTR);
            }
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
                return actionStrArray.Select(a => _nativeActionsDict.First(i => a.Contains(i.Key)).Value).Distinct();
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
