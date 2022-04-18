using System;
using Abc.Aids.Logging;
using System.Threading.Tasks;

namespace Abc.Aids.Methods {

    public static class Safe {
        private static readonly object key = new();
        public static T Run<T>(Func<T> function, T valueOnException,
            bool useLock = false) => useLock
            ? lockedRun(function, valueOnException)
            : run(function, valueOnException);
        public static T Run<T>(Func<T> function, Func<string, T> valueOnException,
            bool useLock = false) => useLock
            ? lockedRun(function, valueOnException)
            : run(function, valueOnException);
        public static void Run(Action action, bool useLock = false) {
            if (useLock) lockedRun(action);
            else run(action);
        }
        public static async Task<T> RunAsync<T>(Func<Task<T>> function, T valueOnException)
            => await runAsync(function, valueOnException);
        public static async Task<T> RunAsync<T>(Func<Task<T>> function, Func<string, T> valueOnException)
            => await runAsync(function, valueOnException);
        public static async Task RunAsync(Func<Task> action) => await runAsync(action);
        private static T lockedRun<T>(Func<T> function, T valueOnException) {
            lock (key) { return run(function, valueOnException); }
        }
        private static T lockedRun<T>(Func<T> function, Func<string, T> valueOnException) {
            lock (key) { return run(function, valueOnException); }
        }
        private static void lockedRun(Action action) {
            lock (key) { run(action); }
        }
        private static T run<T>(Func<T> function, T valueOnException) {
            try { return function(); } catch (Exception e) {
                Log.Exception(e);

                return valueOnException;
            }
        }
        private static T run<T>(Func<T> function, Func<string, T> valueOnException) {
            try { return function(); } catch (Exception e) {
                Log.Exception(e);
                return valueOnException is null ? default : valueOnException(e.Message);
            }
        }
        private static void run(Action action) {
            try { action(); } catch (Exception e) { Log.Exception(e); }
        }
        private static async Task<T> runAsync<T>(Func<Task<T>> function, T valueOnException) {
            try { return await function(); } catch (Exception e) {
                Log.Exception(e);
                return valueOnException;
            }
        }
        private static async Task<T> runAsync<T>(Func<Task<T>> function, Func<string, T> valueOnException) {
            try { return await function(); } catch (Exception e) {
                Log.Exception(e);

                return valueOnException is null ? default : valueOnException(e.Message);
            }
        }
        private static async Task runAsync(Func<Task> action) {
            try { await action(); } catch (Exception e) { Log.Exception(e); }
        }
    }
}