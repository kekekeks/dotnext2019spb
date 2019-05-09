using System.Runtime.InteropServices;
using Common;

namespace SimpleMono
{
    static class WeirdObjectDemo
    {
        #region Magic
        [ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("b48e6d94-f943-42cd-9181-f9fb900df203"),
        ComImport]
        #endregion
        public interface IFoo
        {
            [PreserveSig]
            void Test(int times, [MarshalAs(UnmanagedType.LPUTF8Str)] string x);
        }

        #region Magic
        [return: MarshalAs(UnmanagedType.Interface)]
        #endregion
        [DllImport(Consts.LibraryPath)]
        static extern IFoo CreateWeirdObject();
        
        public static void Demo()
        {
            var foo = CreateWeirdObject();
            foo.Test(5, "Hello world");
        }
    }
}