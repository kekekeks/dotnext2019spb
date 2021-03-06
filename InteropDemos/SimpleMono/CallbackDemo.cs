using System;
using System.Runtime.InteropServices;

namespace SimpleMono
{
    public class CallbackDemo
    {
        [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
         Guid("0565100c-a798-4909-9bf4-d42caab9e734"), 
         ComImport]
        
        public interface ITest
        {
            void Test(int times, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
            void SetCallback([MarshalAs(UnmanagedType.Interface)]ITestCallback cb);
            void DoCallback([MarshalAs(UnmanagedType.LPUTF8Str)]string text);
        }
        
        [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
         Guid("073738b9-e093-4482-8ec0-e5d9254cc7eb"), 
         ComImport]
        public interface ITestCallback
        {
            void CallMe([MarshalAs(UnmanagedType.LPUTF8Str)] string text, [MarshalAs(UnmanagedType.Interface)]ITest caller);
        }
        
        [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
            Guid("4ff9f886-3292-4191-b931-d0ebbcd12afe"),
            ComImport
        ]
        public interface INativeFactory
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            ITest CreateTest();
        }
        

        
        
        [ComVisible(true)]
        class MyCallback : ITestCallback
        {
            public void CallMe(string text, ITest caller)
            {
                Console.WriteLine("Called back with:  " + text);
                caller.Test(1, "Some text");
            }
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        [DllImport(Consts.LibraryPath)]
        static extern INativeFactory CreateNativeFactory();


        public static void Demo()
        {
            var factory = CreateNativeFactory();

            var native = factory.CreateTest();
            native.SetCallback(new MyCallback());
            native.DoCallback("Test?");
            //native.SetCallback(null);
            //native.DoCallback("Test2");
        }
    }
}