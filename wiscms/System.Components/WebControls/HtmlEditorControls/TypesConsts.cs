/// <copyright>
/// �汾���� (C) 2007-2008 �����������ǿƼ����޹�˾
/// </copyright>

namespace Wis.Toolkit.WebControls.HtmlEditorControls
{
    /// <summary>
    /// �������ͼ��
    /// </summary>
    public enum Panels
    {
        /// <summary>
        /// �����ͼ
        /// </summary>		
        Design = 0,
        /// <summary>
        /// Դ������ͼ
        /// </summary>		
        Source = 1,
        /// <summary>
        /// ���ı���ͼ
        /// </summary>
        PlainText = 2,
        /// <summary>
        /// Ԥ����ͼ
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