using System;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ServiceReference1.Service1Client();
            var meth = client.GetType().GetMethod("sum");
            var returnValue = (int)meth.Invoke(client, new object[] { 15,15 });
        }
    }
}
