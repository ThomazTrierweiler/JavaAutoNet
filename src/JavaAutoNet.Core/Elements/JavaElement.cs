using JavaAutoNet.Core.AccessBridgeAPI;
using JavaAutoNet.Core.Actions.NativeActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaAutoNet.Core.Elements
{
    public class JavaElement : BaseJavaElement
    {
        public JavaElement(IntPtr javaObjHandle, int vmID, INativeActionDriver actionDriver, IJavaAutomation javaAutomation, AccessibleContextInfo accessibleContextInfo, string text) : base(javaObjHandle, vmID, actionDriver, javaAutomation, accessibleContextInfo, text)
        {
        }
    }
}
