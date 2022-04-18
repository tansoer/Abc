using System;
using System.Collections.Generic;
namespace Abc.Tests
{
    public abstract class MockInterface: TestsAssertions {
        public List<string> Methods { get; } = new ();
        public object[] Params { get; private set; }
        protected void registerMethod(params object[] o) {
            Params = o;
            var name = getNameAfter("registerMethod");
            Methods.Add(name);
        }
        protected T registerMethod<T>(params object[] o) {
            registerMethod(o);
            return default;
        }
        protected T registerMethod<T>(T result, params object[] o) {
            registerMethod(o);
            return result;
        }
        public void ValidateMethods(object param, params string[] methods) {
            ValidateMethods(methods); 
            areEqual(Params[0], param);
        }
        public void ValidateMethods(params string[] methods) {
            areEqual(methods.Length, Methods.Count);
            for (var i = 0; i < methods.Length; i++) areEqual(methods[i], Methods[i]);
        }
        public void ValidateMethods(Action a, object param, params string[] methods) {
            a();
            ValidateMethods(param, methods);
        }
        public void ValidateMethods(Action a, params string[] methods) {
            a(); 
            ValidateMethods(methods);
        }
        public void ValidateMethods(Func<dynamic> f, object param, params string[] methods) {
            f();
            ValidateMethods(param, methods);
        }
        public void ValidateMethods(Func<dynamic> f, params string[] methods) {
            f();
            ValidateMethods(methods);
        }
    }
}