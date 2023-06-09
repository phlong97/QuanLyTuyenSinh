using System.Globalization;

namespace QuanLyTuyenSinh
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("vi-VN");
            Application.Run(new Form.F_Login());
        }
    }
}