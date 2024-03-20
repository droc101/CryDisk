namespace CryDiskUi
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
            LibCryDisk.CryDisk.Salt = "FPKuWxoGTvdznz7cl8YHg9KTcmUOTFv3";
            Application.Run(new Form1());
        }
    }
}