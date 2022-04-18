
using System;

namespace Abc.Pages.Common.Aids {

    public sealed class Args {

        public Uri PageUrl { get; internal set; }
        public string ItemId { get; internal set; }
        public string SortOrder { get; internal set; }
        public string SearchString { get; internal set; }
        public int? PageIndex { get; internal set; }
        public string FixedFilter { get; internal set; }
        public string FixedValue { get; internal set; }
        public string Handler { get; internal set; }
        public string Title { get; internal set; }
        public string Action { get; internal set; }
        public string Disabled { get; internal set; }
        public string ControlId { get; internal set; }
        public string CurrentFilter { get; internal set; }

        public Args(
            Uri pageUrl = null, string itemId = null,
            string fixedFilter = null, string fixedValue = null,
            string sortOrder = null, string searchString = null,
            int? pageIndex = null, string currentFilter = null) {
            PageUrl = pageUrl;
            ItemId = itemId;
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            SearchString = searchString;
            CurrentFilter = currentFilter;
            SortOrder = sortOrder;
            PageIndex = pageIndex;
        }

    }

}
