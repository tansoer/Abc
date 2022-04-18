using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abc.Pages.Common {

    public abstract class PagedPage<TP, TR, TO, TV, TD> : ItemsPage<TP, TR, TO, TV, TD>
        where TP : PageModel
        where TR : class, IRepo<TO>
        where TO : IBaseEntity<TD>
        where TV : BaseView
        where TD : BaseData, new() {
        protected PagedPage(TR r) : base(r) { }
        public string SelectedId { get; set; }
        public bool HasPreviousPage => db.HasPreviousPage;
        public bool HasNextPage => db.HasNextPage;
        public int TotalPages => db.TotalPages;
        protected internal void setPageValues( string sortOrder, string searchString, 
            in int? pageIndex, string fixedFilter, dynamic fixedValue, string[] fixedValues = null) {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            SortOrder = sortOrder;
            SearchString = searchString;
            PageIndex = pageIndex ?? 0;
            FixedValues = fixedValues;
        }
        internal static int getPageIndex(string currentFilter, string searchString, int? pageIdx) {
            if (searchString != currentFilter) return 1;
            return pageIdx ?? 1;
        }
        protected internal async Task getList(string sortOrder, string currentFilter, string searchString,
            int? pageIndex, string fixedFilter, string fixedValue, string[] fixedValues) {
            setPageValues(sortOrder, searchString, pageIndex, fixedFilter, fixedValue, fixedValues);
            CurrentFilter = searchString;
            PageIndex = getPageIndex(currentFilter, searchString, pageIndex);
            Items = await getList();
        }
    }
}
