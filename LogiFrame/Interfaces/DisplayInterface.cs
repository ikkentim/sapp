namespace LogiFrame.Interfaces
{
    internal interface DisplayInterface
    {
        void SetUpdatePriority(UpdatePriority priority);
        void UpdateScreen(System.Drawing.Bitmap bitmap);
        void UpdateScreen(byte[] bytemap);
        long ReadSoftButtons();
        int GetButtons();
        void Close();
    }
}
