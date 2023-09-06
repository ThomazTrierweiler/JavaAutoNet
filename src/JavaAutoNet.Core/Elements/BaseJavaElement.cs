using JavaAutoNet.Core.AccessBridgeAPI;
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
    public abstract class BaseJavaElement : IElement
    {
        private IntPtr _javaObjHandle;
        private int _vmID;
        public IJavaAutomation JavaAutomation { get; }
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
        public bool AccessibleComponent { get; }
        public bool AccessibleAction { get; }
        public bool AccessibleSelection { get; }
        public bool AccessibleText { get; }
        private bool disposedValue;

        protected BaseJavaElement(IntPtr javaObjHandle, int vmID, IJavaAutomation javaAutomation, AccessibleContextInfo accessibleContextInfo)
        {
            _javaObjHandle = javaObjHandle;
            _vmID = vmID;
            JavaAutomation = javaAutomation;
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
    }

        public virtual string GetText()
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
        }
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

        public virtual bool SetText(string text)
        {
            throw new NotImplementedException();
        }

        public virtual IElement GetParent()
        {
            throw new NotImplementedException();
        }

        public virtual IElement GetTopLevelWindow()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<IElement> GetChildren()
        {
            throw new NotImplementedException();
        }

        public virtual void Click()
        {
            throw new NotImplementedException();
        }

        public virtual void DoNativeAction(NativeAction nativeAction)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<NativeAction> GetPossibleNativeActions()
        {
            throw new NotImplementedException();
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
                    AccessBridge.ReleaseJavaObject(_vmID, _javaObjHandle);
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
