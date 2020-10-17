using System.IO;

namespace Medusa.Analyze1553B.UIServices
{
    public interface IDialogService
    {
        void ShowMessage(string message, string title = null);
        MemoryStream ShowOpenFileDialog();
        MemoryStream ShowSaveFileDialog();

        void CreateCustomWindow();
        void ScrollIntoView(object arg, int pos);
        void UpdateView(object arg);
        void AddText(object arg, string text);
        int CurrentPosition(object arg);
        void CreateChoosePageViewModelControl(object arg);

        string Filter { get; set; }
    }
}
