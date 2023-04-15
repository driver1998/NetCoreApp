using System;
using Windows.UI.Xaml;

namespace NetCoreApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Application.Start((p) =>
            {
                new App();
            });
        }
    }
}
