### Introduction
Microsoft's native UI Automation libraries are not able to handle Java UI and using the functions from the WindowsAccessBridge-64.dll can be quite challenging for developers who are not familiar with InteropServices.
JavaAutoNet is a Java UI automation library for .NET that aims to make it easier and safer to automate Java GUI's.
It uses Oracle's Java Access Bridge (so it requires the WindowsAccessBridge-64.dll) to automate Java elements.
This project has used some ideas from Google's Access Bridge Explorer and FlaUI.

### Usage
##### Installation
In order to use JavaAutoNet, you need install both JavaAutoNet.Core and JavaAutoNet.JavaAutomation1 from NuGet. 
Java Access Bridge must also be installed and activated. See: https://docs.oracle.com/javase/8/docs/technotes/guides/access/enable_and_test.html

##### Usage in Code
If you are not using this library in a Windows Forms Application then it will be necessary to call the method Application.DoEvents() from System.Windows.Forms.
 
```csharp
using JavaAutoNet.Core;
using JavaAutomationV1;
using JavaAutoNet.Core.Enums.NativeActions;



//The role-based element xpath can be obtained through Google's Java Access Bridge Explorer
string xpath = "root pane[0]/layered pane[0]/panel[0]/push button[1]";
using (IJavaAutomation javaAutomation = new JavaAutomation())
{
	//System.Windows.Forms.Application.DoEvents(); //Unecessary if you solution is a Windows Forms Application. IMPORTANT: Initialize JavaAutomation() BEFORE calling DoEvents()

	//First get a Java Window through your prefered method
	IJavaElement? javaWindow = javaAutomation.FindJavaWindow("JavaWindowName");

	//Get the desired java element
	IJavaElement? childElement = javaAutomation.FindJavaElement(javaWindow, xpath);

	//Execute the desired action
	childElement?.DoNativeAction(NativeAction.Click);

	//Do not forget to call Dispose() on each java window/element or wrap them with a using block
	javaWindow?.Dispose();
	childElement?.Dispose();
}
```

```csharp
using JavaAutomationV1;
using JavaAutoNet.Core.Elements;
using JavaAutoNet.Core;

//The role-based element xpath can be obtained through Google's Java Access Bridge Explorer
string xpath = "root pane[0]/layered pane[0]/panel[0]/panel[0]/push button[0]";
using (IJavaAutomation javaAutomation = new JavaAutomation())
{
    System.Windows.Forms.Application.DoEvents(); //Unecessary if you solution is a Windows Forms Application. IMPORTANT: Initialize JavaAutomation() BEFORE calling DoEvents()
    using(IJavaElement? javaWindow = javaAutomation.FindJavaWindow("JavaTest"))
    using(IJavaElement? javaElement = javaAutomation.FindJavaElement(javaWindow, xpath))
    {
        javaElement?.Click();
        //javaElement?.SetText("Texto to be set on the element");
    }
}
```

### Acknowledgements
#### FlaUI
Thanks to the creators and maintainers (https://github.com/FlaUI/FlaUI) since their project was an inspiration for ours.
#### Google
Thanks to Google for its Access Bridge Explorer (https://github.com/google/access-bridge-explorer), a reliable tool used to inspect Java UI elements and that we thoroughly used to develop this project.
