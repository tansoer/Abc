using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
namespace Abc.Tests
{
    public class Mock : TestsAids{ 
        public List<string> CalledMethods { get; } = new ();
        public StackTrace Stack {  get; private set; }
        public Dictionary<string, object[]> Parameters { get; } = new();
        public T Run<T>(string method, T result) {
            Run(method);
            return result;
        }
        public void Run(string method) {
            CalledMethods.Add(method);
        }
        public async Task<T> RunAsync<T>(string method, T result) {
            await RunAsync(method);
            return result;
        }
        public async Task RunAsync(string method) {
            CalledMethods.Add(method);
            await Task.CompletedTask;
        }
        public void Action(params object[] parameters) {
            Stack = getStack();
            var method = getNameAfter(Stack, nameof(Action));
            CalledMethods.Add(method);
            Parameters.Add(method, parameters);
        }
        public T Func<T>(params object[] parameters) {
            Stack = getStack();
            var method = getNameAfter(Stack, nameof(Func));
            CalledMethods.Add(method);
            Parameters.Add(method, parameters);
            return default;
        }
    }
}