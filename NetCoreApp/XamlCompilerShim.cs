#if COMPILING_XAML
using System;

namespace WinRT
{
    public static class TypeExtensions { }

    public class WinRTExposedTypeAttribute(params Type[] winrtExposedInterfaces) : Attribute { }

    public class WinRTRuntimeClassName(string runtimeClassName) : Attribute { }

    public interface IWinRTExposedTypeDetails { }
}

namespace System.Runtime.InteropServices.ComWrappers
{
    public struct ComInterfaceEntry
    { 
        public Guid IID;
        public IntPtr Vtable;
    }
}

namespace ABI.System.ComponentModel
{
    public class INotifyPropertyChangedMethods
    {
        public static Guid IID { get; set; }
        public static IntPtr AbiToProjectionVftablePtr { get; }
    }
}

namespace ABI.System.Collections.Specialized
{
    public class INotifyCollectionChangedMethods
    {
        public static Guid IID { get; set; }
        public static IntPtr AbiToProjectionVftablePtr { get; }
    }
}
#endif
