using JavaAutoNet.Core.AccessBridgeAPI;
using JavaAutoNet.Core.Actions.NativeActions;
using JavaAutoNet.Core.Enums.NativeActions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JavaAutoNet.Core.Elements
{
    public abstract class BaseJavaElement : IJavaElement
    {
        private IntPtr _javaObjHandle;
        public int VmID { get; }
        private readonly INativeActionDriver _actionDriver;
        private readonly IJavaAutomation _javaAutomation;
        public string Name { get; }
        public string Description { get; }
        public string Role { get; }
        public string RoleInEnglish { get; }
        public string States { get; }
        public string StatesInEnglish { get; }
        public int IndexInParent { get; }
        public int ChildrenCount { get; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public string Text { get; }
        public bool AccessibleComponent { get; }
        public bool AccessibleAction { get; }
        public bool AccessibleSelection { get; }
        public bool AccessibleText { get; }
        private bool disposedValue;

        protected BaseJavaElement(IntPtr javaObjHandle, int vmID, INativeActionDriver actionDriver, IJavaAutomation javaAutomation, AccessibleContextInfo accessibleContextInfo, string text)
        {
            _javaObjHandle = javaObjHandle;
            VmID = vmID;
            _actionDriver = actionDriver;
            _javaAutomation = javaAutomation;
            Name = accessibleContextInfo.Name;
            Description = accessibleContextInfo.Description;
            Role = accessibleContextInfo.Role;
            RoleInEnglish = accessibleContextInfo.RoleInEnglish;
            States = accessibleContextInfo.States;
            StatesInEnglish = accessibleContextInfo.StatesInEnglish;
            IndexInParent = accessibleContextInfo.IndexInParent;
            ChildrenCount = accessibleContextInfo.ChildrenCount;
            X = accessibleContextInfo.X;
            Y = accessibleContextInfo.Y;
            Width = accessibleContextInfo.Width;
            Height = accessibleContextInfo.Height;
            AccessibleComponent = accessibleContextInfo.AccessibleComponent;
            AccessibleAction = accessibleContextInfo.AccessibleAction;
            AccessibleSelection = accessibleContextInfo.AccessibleSelection;
            AccessibleText = accessibleContextInfo.AccessibleText;
            Text = text;
        }

        /*public virtual string GetText()
        {
            //Pointer to an AccessibleTextItemInfo
            IntPtr atiInfoPtr = IntPtr.Zero;
            try
            {
                //Reserve memory
                atiInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(new AccessibleTextItemsInfo()));
                //Call DLL.
                if (!AccessBridge.GetAccessibleTextItems(_vmID, _javaObjHandle, atiInfoPtr, 0))
                    return "";
                //Create object
                AccessibleTextItemsInfo atInfo = (AccessibleTextItemsInfo)Marshal.PtrToStructure(atiInfoPtr, typeof(AccessibleTextItemsInfo));
                return atInfo.Sentence;
            }
            finally
            {
                //Free memory       
                if (atiInfoPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(atiInfoPtr);
            }
        }*/
        /*
        public virtual string GetXPath()
        {
            return IndexInParent == -1 ? "" : GetXPathRecursively("", this);
        }
        protected virtual string GetXPathRecursively(string currentXPath, IElement currentElement)
        {
            if (currentElement.IndexInParent == -1)
                return currentXPath.TrimEnd('/');

            IElement parent = currentElement.GetParent();
            string currentRole = currentElement.Role;
            IEnumerable<IElement> siblings = parent.GetChildren();
            IEnumerable<IElement> twins = siblings.Where(sibling => sibling.Role == currentRole); // The twins IEnumerable is used to determine how many Java siblings have the same role.
            int index = currentElement.IndexInParent - twins.First().IndexInParent; // The same parent might have other children that aren't twins. Therefore, the right index is the result of the substraction between the IndexInParent of the desired twin and the IndexInParent of the first twin.
            if (currentElement != this)
                currentElement.Dispose();
            foreach (IElement sibling in siblings)
                sibling.Dispose(); // Since IElement implements IDisposable, all siblings must be disposed to release memory. Since the twins are a reference to the siblings, they will be disposed automatically.

            currentXPath = $"{currentRole}[{index}]/{currentXPath}";
            return GetXPathRecursively(currentXPath, parent);
        }*/

        public virtual void SetText(string text)
        {
            _actionDriver.SetText(VmID, _javaObjHandle, text);
        }

        public virtual IJavaElement? GetParent()
        {
            return IndexInParent != -1 ? _javaAutomation.FindParentElement(VmID, _javaObjHandle) : null;
        }

        public virtual IJavaElement? GetTopLevelWindow()
        {
            return GetTopLevelWindowRecursively(this, false);
        }

        protected virtual IJavaElement? GetTopLevelWindowRecursively(IJavaElement currElement, bool shouldDisposeCurrElement = false)
        {
            if(currElement.IndexInParent == -1)
                return currElement;

            IJavaElement? parent = currElement.GetParent();
            if(shouldDisposeCurrElement)
                currElement.Dispose();

            return parent != null ? GetTopLevelWindowRecursively(parent, true) : null;
        }

        public virtual IEnumerable<IJavaElement> GetChildren()
        {
            List<IJavaElement> children = new List<IJavaElement>();
            for(int i = 0; i < ChildrenCount; i++)
            {
                IJavaElement? childElement = _javaAutomation.FindChildElement(VmID, _javaObjHandle, i);
                if (childElement != null)
                    children.Add(childElement);
            }
            return children;
        }

        public virtual void Click()
        {
            DoNativeAction(NativeAction.Click);
        }

        public virtual void DoNativeAction(NativeAction nativeAction)
        {
            _actionDriver.DoNativeAction(VmID, _javaObjHandle, nativeAction);
        }

        public virtual IEnumerable<NativeAction> GetPossibleNativeActions()
        {
            return _actionDriver.GetPossibleNativeActions(VmID, _javaObjHandle);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                if(_javaObjHandle != IntPtr.Zero)
                {
                    AccessBridge.ReleaseJavaObject(VmID, _javaObjHandle);
                    _javaObjHandle = IntPtr.Zero;
                }
                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~BaseJavaElement()
        {
            //Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
