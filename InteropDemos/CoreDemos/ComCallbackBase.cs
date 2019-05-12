using System;
using System.Threading;
using SharpGen.Runtime;

namespace CoreDemos.Interop
{
    public class ComCallbackBase : DisposeBase, ICallbackable
    {
        private int refCount = 1;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Release();
            }
        }

        public uint AddRef()
        {
            var old = refCount;
            while (true)
            {
                if (old == 0)
                {
                    throw new ObjectDisposedException("Cannot add a reference to a nonreferenced item");
                }
                var current = Interlocked.CompareExchange(ref refCount, old + 1, old);
                if (current == old)
                {
                    return (uint)(old + 1);
                }
                old = current;
            }
        }

        public uint Release()
        {
            var old = refCount;
            while (true)
            {
                var current = Interlocked.CompareExchange(ref refCount, old - 1, old);

                if (current == old)
                {
                    if (old == 1)
                    {
                        // Dispose native resources
                        var callback = ((ICallbackable)this);
                        callback.Shadow = null;
                        OnDestroyed();
                    }
                    return (uint)(old - 1);
                }
                old = current;
            }
        }

        private ShadowContainer shadow;

        ShadowContainer ICallbackable.Shadow
        {
            get
            {
                return Volatile.Read(ref shadow);
            }
            set
            {
                if (value != null)
                {
                    // Only set the shadow container if it is not already set.
                    if (Interlocked.CompareExchange(ref shadow, value, null) != null)
                    {
                        value.Dispose();
                    }
                }
                else
                {
                    Interlocked.Exchange(ref shadow, value).Dispose();
                }
            }
        }

        protected virtual void OnDestroyed()
        {
            
        }
    }
}