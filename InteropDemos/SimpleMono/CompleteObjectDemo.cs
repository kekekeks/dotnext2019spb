using System;
using System.Runtime.InteropServices;
using Common;

namespace SimpleMono
{
    static class CompleteObjectDemo
    {
        [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
         Guid("b48e6d94-f943-42cd-9181-f9fb900df203"), 
         ComImport]
        public interface IFoo
        {
            [PreserveSig]
            void Test(int times, [MarshalAs(UnmanagedType.LPUTF8Str)] string x);
        }

        #region Magic
        [return: MarshalAs(UnmanagedType.Interface)]
        #endregion
        [DllImport(Consts.LibraryPath)]
        static extern IFoo CreateCompleteObject();

        public static void Demo()
        {
            var foo = CreateCompleteObject();
            foo.Test(5, "Hello world");
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        public static void Demo2()
        {
            Demo();
            
            // M0N0
            GC.Collect(2);
            GC.Collect(2);
            GC.Collect(2);
            GC.WaitForPendingFinalizers();
            Console.WriteLine("Finish");
            
        }
        
        
        
        public static void Demo3()
        {
            var foo = CreateCompleteObject();
            foo.Test(5, "Hello world");
            Marshal.FinalReleaseComObject(foo);
            foo = null;
            Console.WriteLine("Finish");
        }
    }
}