using System.Collections.Generic;
using Abc.Aids.Methods;

namespace Abc.Domain.Common {

    public sealed class GetFrom<TRepo, TObject>
        where TRepo : IRepo<TObject> {
        internal static TRepo repo => GetRepo.Instance<TRepo>();

        public TObject ById(string id)
            => Safe.Run(() => repo.Get(id), default(TObject));

        public IReadOnlyList<TObject> ListBy(string field, string value) => list(repo, field, value);

        public IReadOnlyList<TObject> ListBy(string field, string value, string searchString) {
            var r = repo;
            r.SearchString = searchString;
            return list(r, field, value);
        }

        private static IReadOnlyList<TObject> list(TRepo r, string field, string value) {
            if (r is null) return new List<TObject>().AsReadOnly();
            r.FixedFilter = field;
            r.FixedValue = value;
            r.PageIndex = -1;
            return r.Get();
        }
    }
}