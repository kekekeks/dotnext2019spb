using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CoreDemos.Interop;
using SharpGen.Runtime;

namespace CoreDemos
{
    class Program
    {
        [DllImport("../../vtables/build/libdemos.so")]
        static extern IntPtr CreateNativeFactory();

        class MyCallback : ComCallbackBase, ITestCallback
        {
            public void CallMe(string text, ITest caller)
            {
                Console.WriteLine("Called with: " + text);
                caller.Test(2, "Hello world!");
            }

            protected override void OnDestroyed()
            {
                Console.WriteLine("Destroyed");
            }
        }

        static void Main(string[] args)
        {
            var fac = new INativeFactory(CreateNativeFactory());


            var test = fac.CreateTest();

            using (var cb = new MyCallback())
                test.SetCallback(cb);
            

            test.DoCallback("123");

            Console.WriteLine("Removing callback");
            test.SetCallback(null);
            Console.WriteLine("Removed callback");


            using (var concat = fac.Concat((Utf8String) "foo", (Utf8String) "bar"))
            {
                Console.WriteLine(concat.String);
            }

        }
    }
}