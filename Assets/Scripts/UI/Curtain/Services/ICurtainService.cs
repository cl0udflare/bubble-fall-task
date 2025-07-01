using Infrastructure.Bootstrapper;

namespace UI.Curtain.Services
{
    public interface ICurtainService : IInitializableAsync
    {
        void Show(string text = "Loading...");
        void Hide();
        void SetProgress(float value);
        void SetText(string text);
    }
}