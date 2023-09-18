using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaAutoNet.Core.Enums.NativeActions
{
    public enum NativeAction
    {
        Undefined = 0,
        Click = 1,
        PageUp = 2,
        PageDown = 3,
        CaretForward = 4,
        CaretBackward = 5,
        SelectionDown = 6,
        SelectionUp = 7,
        SelectLine = 8,
        Unselect = 9,
        CopyToClipboard = 10,
        InsertContent = 11,
        SelectWord = 12,
    }
}
