using JavaAutoNet.Core.AccessBridgeAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaAutoNet.Core.Elements
{
    public class JavaElement : BaseJavaElement
    {
        public JavaElement(IntPtr javaObjHandle, int vmID, IJavaAutomation javaAutomation, AccessibleContextInfo accessibleContextInfo) : base(javaObjHandle, vmID, javaAutomation, accessibleContextInfo)
        {
        }
    }
}
