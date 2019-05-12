using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SimpleMono
{
    public class DllImportCallbackDemo
    {
        public delegate void MyCallback(int arg);

        [DllImport(Consts.LibraryPath)]
        static extern void SetCCallback(MyCallback cb);

        [DllImport(Consts.LibraryPath)]
        static extern void CallCCallback(int v);

        static void DoCall()
        {
            SetCCallback(i => Console.WriteLine("Called: " + i));
            CallCCallback(2);
        }
        
        public static void Demo()
        {
            DoCall();
            Console.WriteLine("Done");
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            

            Test.Init();
            for(var c=0; c<100;c++)
            {
                var x = new byte[1024 * 1024 * 1024];
                CallCCallback(c);
                GC.Collect(2);
                GC.Collect(2);
                GC.Collect(2);
                GC.WaitForPendingFinalizers();
            }
            
        }

        class Test
        {
            private List<int> _lst = new List<int>();
            public void Callback(int c)
            {
                _lst.Add(c);
                Console.WriteLine("Called: " + c + " " + _lst.Count);
            }

            public static void Init() => SetCCallback(new Test().Callback);
        }
    }
}