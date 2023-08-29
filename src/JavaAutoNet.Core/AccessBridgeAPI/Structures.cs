using System.Runtime.InteropServices;

namespace JavaAutoNet.Core.AccessBridgeAPI
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct AccessibleContextInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string Name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string Description;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Role;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string RoleInEnglish;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string States;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string StatesInEnglish;
        public int IndexInParent;
        public int ChildrenCount;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public bool AccessibleComponent;
        public bool AccessibleAction;
        public bool AccessibleSelection;
        public bool AccessibleText;
        public bool AccessibleInterfaces;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct AccessibleTextItemsInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
        public string Letter;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Word;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string Sentence;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct AccessibleTextAttributesInfo
    {
        public bool Bold;
        public bool Italic;
        public bool Underline;
        public bool Strikethrough;
        public bool Superscript;
        public bool Subscript;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string BackgroundColor;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string ForegroundColor;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string FontFamily;
        public int FontSize;
        public int Alignment;
        public int BidiLevel;
        public float FirstLineIndent;
        public float LeftIndent;
        public float RightIndent;
        public float LineSpacing;
        public float SpaceAbove;
        public float SpaceBelow;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string FullAttributesString;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct AccessibleActionInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Name;        // action name
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct AccessibleActions
    {
        public int ActionsCount;                                // number of actions
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public AccessibleActionInfo[] ActionInfo; // the action information
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct AccessibleActionsToDo
    {
        public int ActionsCount;                              // number of actions to do
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public AccessibleActionInfo[] Actions;// the accessible actions to do
    }
}
