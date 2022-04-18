using Abc.Domain.Common;

namespace Abc.Domain.Parties.Names {

    public interface INamePrefixesRepo : IRepo<NamePrefix> {
        int NextIndex(string masterId);
    }
}