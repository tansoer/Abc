using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;

namespace Abc.Aids.Reflection {

    public static class GetSolution {

        public static AppDomain Domain => AppDomain.CurrentDomain;

        public static List<Assembly> Assemblies =>
            Safe.Run(() => Domain.GetAssemblies().ToList(),
                new List<Assembly>());

        public static Assembly AssemblyByName(string name) {
            return Safe.Run(() => Assembly.Load(name), (Assembly) null);
        }

        public static List<Type> TypesForAssembly(string assemblyName) {
            var empty = new List<Type>();
            return Safe.Run(() => {
                var a = AssemblyByName(assemblyName);
                return a?.GetTypes().ToList() ?? empty;
            }, empty);
        }

        public static List<string> TypeNamesForAssembly(string assemblyName) {
            var empty = new List<string>();
            return Safe.Run(() => {
                var a = TypesForAssembly(assemblyName);
                return a?.Select(t => t.FullName).ToList() ?? empty;
            }, empty);
        }

        public static string Name =>
            GetClass.Namespace(typeof(GetSolution)).GetHead();
    }

}



