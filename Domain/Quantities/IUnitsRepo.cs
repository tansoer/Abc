using Abc.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Domain.Quantities {
    public interface IUnitsRepo :IRepo<Unit> {
        Unit GetByCode(string code);
        Task<Unit> GetByCodeAsync(string code);

        List<Unit> GetByCodes(List<string> codes);
        Task<List<Unit>> GetByCodesAsync(List<string> codes);
    }
}
