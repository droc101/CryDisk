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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            LibCryDisk.CryDisk.Salt = "FPKuWxoGTvdznz7cl8YHg9KTcmUOTFv3";
            Application.Run(new Form1());
        }
    }
}