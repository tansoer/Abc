
using System;

namespace Abc.Domain.Common {
    public interface IEntity: IBaseEntity {
        string Name { get; }
        string Code { get; }
        string Details { get; }
    }
    public interface IBaseEntity {
        string Id { get; }
        DateTime ValidFrom { get; }
        DateTime ValidTo { get; }
        bool IsUnspecified { get; }
    }
    public interface IEntity<TData> : IBaseEntity<TData>, IEntity { }

    public interface IBaseEntity<TData> :IBaseEntity {
        TData Data { get; }
        void SetData(TData d);
    }
}
