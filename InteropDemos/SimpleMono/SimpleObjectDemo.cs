using System;
using System.Runtime.InteropServices;
using Common;

namespace SimpleMono
{
    public unsafe class SimpleObjectDemo
    {
        struct IFooVtable
        {
            public IntPtr Test;
        }

        [DllImport(Consts.LibraryPath)]
        static extern IFooVtable** CreateSimpleObject();

        delegate void TestDelegate(IFooVtable** self, int times, [MarshalAs(UnmanagedType.LPUTF8Str)] string text);
        
        public static void Demo()
        {
            var foo = CreateSimpleObject();
            var test = Marshal.GetDelegateForFunctionPointer<TestDelegate>(
                (*foo)->Test);
            
            test(foo, 5, "Hello world!");

        }
    }
}