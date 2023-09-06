using JavaAutoNet.Core.Enums.NativeActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaAutoNet.Core.Elements
{
    public interface IElement : IDisposable
    {
        /// <summary>
        /// The AccessibleName of the object
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The AccessibleDescription of the object.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Localized AccesibleRole string. Usually in the language of the system's region.
        /// </summary>
        string Role { get; }
        /// <summary>
        /// Localized AccesibleRole string. Use this one instead of Role in order to avoid language-related differences.
        /// </summary>
        string RoleInEnglish { get; }
        /// <summary>
        /// Localized AccesibleStateSet string. Usually in the language of the system's region.
        /// </summary>
        string States { get; }
        /// <summary>
        /// Localized AccesibleStateSet string. Use this one instead of States in order to avoid language-related differences.
        /// </summary>
        string StatesInEnglish { get; }
        /// <summary>
        /// The index of this element in its parent. Note that the IndexInParent is different from the one in the element's XPath, as the former is the index referring to all of the parent's children, while the latter only takes into account the parent's children with the same Role. If the result is -1, it means that there is no parent.
        /// </summary>
        int IndexInParent { get; }
        /// <summary>
        /// Number of children, if any.
        /// </summary>
        int ChildrenCount { get; }
        /// <summary>
        /// Element's top-left x-axis co-ordinate in pixels.
        /// </summary>
        int X { get; }
        /// <summary>
        /// Element's top-left y-axis co-ordinate in pixels.
        /// </summary>
        int Y { get; }
        /// <summary>
        /// Element's width in pixels.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Element's height in pixels.
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Has accessible component.
        /// </summary>
        bool AccessibleComponent { get; }
        /// <summary>
        /// Has accessible action.
        /// </summary>
        bool AccessibleAction { get; }
        /// <summary>
        /// Has accessible selection.
        /// </summary>
        bool AccessibleSelection { get; }
        /// <summary>
        /// Has accessible text.
        /// </summary>
        bool AccessibleText { get; }
        /// <summary>
        /// The IJavaAutomation object that shall be disposed by whichever class owns it.
        /// </summary>
        IJavaAutomation JavaAutomation { get; }
        /*
        /// <summary>
        /// Gets the XPath to the element (relative to the Java window).
        /// </summary>
        /// <returns>The element's XPath.</returns>
        string GetXPath(); */
        /// <summary>
        /// Gets the element's accessible text.
        /// </summary>
        /// <returns>The element's text.</returns>
        string GetText();
        /// <summary>
        /// Attempts to set the given text into the element. Returns true if it succeeds, false otherwise.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        bool SetText(string text);
        /// <summary>
        /// Returns this element's parent.
        /// </summary>
        /// <returns></returns>
        IElement GetParent();
        /// <summary>
        /// Returns this element's Java window.
        /// </summary>
        /// <returns></returns>
        IElement GetTopLevelWindow();
        /// <summary>
        /// Returns this element's children.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IElement> GetChildren();
        /// <summary>
        /// Clicks on the element through its native function call.
        /// </summary>
        void Click();
        /// <summary>
        /// Executes a given native action. In order to avoid potential errors, be sure that the native action is available for this element by checking the return of GetPossibleNativeActions().
        /// </summary>
        /// <param name="nativeAction"></param>
        void DoNativeAction(NativeAction nativeAction);
        /// <summary>
        /// Returns an IEnumerable containing all of the native actions that can be performed by/into this element.
        /// </summary>
        /// <returns></returns>
        IEnumerable<NativeAction> GetPossibleNativeActions();
    }
}
