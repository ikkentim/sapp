using LogiFrame;
namespace Sapp.Tabs
{
    interface Tab
    {
        bool IsShowAble();
        void Show(Frame lcd);
        void Hide(Frame lcd);
    }
}
