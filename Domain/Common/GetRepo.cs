using System;
using Abc.Aids.Methods;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Domain.Common {

    public static class GetRepo {
        internal static IServiceProvider services;
        public static void SetServiceProvider(IServiceProvider p) => services = p;
        public static T Instance<T>() => instance<T>(services);
        public static dynamic Instance(Type t) => instance(services, t);
        internal static T instance<T>(IServiceProvider h) => instance(h, typeof(T));
        internal static dynamic instance(IServiceProvider h, Type t)
            => Safe.Run(() => h?.GetRequiredService(t), null);
    }
}
