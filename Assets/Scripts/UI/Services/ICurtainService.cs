namespace UI.Services
{
    public interface ICurtainService
    {
        void Show(string text = "Loading...");
        void Hide();
        void SetProgress01(float value);
        void SetText(string text);
    }
}