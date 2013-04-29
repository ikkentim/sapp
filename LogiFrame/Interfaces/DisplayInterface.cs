namespace LogiFrame.Interfaces
{
    internal interface DisplayInterface
    {
        void SetUpdatePriority(UpdatePriority priority);
        void UpdateScreen(System.Drawing.Bitmap bitmap);
        long ReadSoftButtons();
        int GetButtons();
        void Close();
    }
}
