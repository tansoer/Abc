using Abc.Domain.Common;

namespace Abc.Domain.Parties.Names {

    public interface INameSuffixesRepo : IRepo<NameSuffix> {
        int NextIndex(string masterId);
    }
}