using Abc.Data.Quantities;
using Abc.Domain.Quantities;
using Abc.Infra.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Infra.Quantities {

    public sealed class UnitsRepo : PagedRepo<Unit, UnitData>, IUnitsRepo {
        public UnitsRepo(QuantityDb c = null) : base(c, c?.Units) { }
        protected internal override Unit toDomainObject(UnitData d) => UnitFactory.Create(d);
        public Unit GetByCode(string code) => getList().Find(x => x.Code == code);
        public async Task<Unit> GetByCodeAsync(string code) {
            var units = await getListAsync();
            return units.FirstOrDefault(x => x.Code == code);
        }
        public List<Unit> GetByCodes(List<string> codes) 
            => codes != null ? getList().Where(x => codes.Any(y => y == x.Code)).ToList() : null;
        public async Task<List<Unit>> GetByCodesAsync(List<string> codes) {
            if (codes == null) return null;
            var units = await getListAsync();
            return units.Where(x => codes.Any(y => y == x.Code)).ToList();
        }
    }
}
