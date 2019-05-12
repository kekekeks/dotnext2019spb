using System;
using System.Runtime.InteropServices;
using System.Text;
using SharpGen.Runtime;

namespace CoreDemos.Interop
{
    
    
    
    partial interface IUtf8String
    {
        string String { get; }
    }
    
    public unsafe partial class IUtf8StringNative
    {
        private string _clrString;
        private bool _converted;

        public string String
        {
            get
            {
                if (!_converted)
                {
                    var data = GetData();
                    if (data != IntPtr.Zero)
                        _clrString = Encoding.UTF8.GetString((byte*) data.ToPointer(), GetLength());
                    _converted = true;
                }

                return _clrString;
            }
        }
    }


    unsafe class Utf8String : ComCallbackBase, IUtf8String
    {
        private readonly string _s;
        private bool _converted;
        private IntPtr _data;
        private int _len;

        public Utf8String(string s)
        {
            _s = s;
        }

        public string String => _s;

        void EnsureConverted()
        {
            if (!_converted)
            {
                if (_s != null)
                {
                    _len = Encoding.UTF8.GetByteCount(_s);
                    _data = Marshal.AllocHGlobal(_len + 1);
                    var p = (byte*) _data;
                    fixed (char* c = _s) 
                        Encoding.UTF8.GetBytes(c, _s.Length, p, _len);
                    p[_len] = 0;
                }

                _converted = true;
            }
        }
        
        public int GetLength()
        {
            EnsureConverted();
            return _len;
        }

        public IntPtr GetData()
        {
            EnsureConverted();
            return _data;
        }

        public static implicit operator Utf8String(string s) => new Utf8String(s);


        protected override void OnDestroyed()
        {
            if (_data != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_data);
                _data = IntPtr.Zero;
                _len = 0;
            }
        }
    }
    
}