using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace System
{
    public static class IAsyncOperationExtensions
    {
        public static TaskAwaiter<T> GetAwaiter<T>(this IAsyncOperation<T> op)
        {
            return default;
        }

        public static TaskAwaiter GetAwaiter(this IAsyncAction op)
        {
            return default;
        }
    }
}

namespace System.Runtime.InteropServices.WindowsRuntime
{
    public struct EventRegistrationToken { }

    public sealed class EventRegistrationTokenTable<T> where T : class
    {
        public T InvocationList { get; set; }
        public EventRegistrationToken AddEventHandler(T handler) { return default; }
        public static EventRegistrationTokenTable<T> GetOrCreateEventRegistrationTokenTable(ref EventRegistrationTokenTable<T> refEventTable) { return default; }
        public void RemoveEventHandler(EventRegistrationToken token) { }
        public void RemoveEventHandler(T handler) { }
    }
    public interface IActivationFactory { }
    public static class WindowsRuntimeMarshal
    {
        public static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler) { }
        public static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod) { }
        public static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler) { }
    }
}