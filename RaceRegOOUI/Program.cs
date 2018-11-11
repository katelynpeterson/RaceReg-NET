using Ooui;
using System;
using Xamarin.Forms;

namespace RaceRegOOUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Forms.Init();
            UI.Port = 12345;
            UI.Host = "localhost";
            UI.Publish("/", new Page1() { BindingContext = new ViewModel() }.GetOouiElement());
            Console.ReadLine();
        }
    }
}
