/// <copyright>
/// 版本所有 (C) 2007-2008 北京东方常智科技有限公司
/// </copyright>

namespace Wis.Toolkit.WebControls.HtmlEditorControls
{
    /// <summary>
    /// 设计器视图。
    /// </summary>
    public enum Panels
    {
        /// <summary>
        /// 设计视图
        /// </summary>		
        Design = 0,
        /// <summary>
        /// 源代码视图
        /// </summary>		
        Source = 1,
        /// <summary>
        /// 纯文本视图
        /// </summary>
        PlainText = 2,
        /// <summary>
        /// 预览视图
        /// </summary>
        Preview = 3
    }

    /// <summary>
    /// Notification mode.
    /// </summary>
    public enum NotificationMode
    {
        /// <summary>
        /// In this mode, the editor will not show the tool tip with additional information at each switch event.
        /// </summary>
        Silent = 0,
        /// <summary>
        /// In this mode, the user will see all messages and is able to cancel the switch designer view.
        /// </summary>		
        Verbose = 1
    }
}