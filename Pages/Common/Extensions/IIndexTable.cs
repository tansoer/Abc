using Abc.Pages.Common.Aids;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Abc.Pages.Common.Extensions {
    public interface IIndexTable<TPage> {
        int ColumnsCount { get; }
        int RowsCount { get; }
        void SetItem(int i);
        string NameFor(IHtmlHelper<TPage> h, int i);
        IHtmlContent ValueFor(IHtmlHelper<TPage> h, int i);
        Uri SortStringFor(int i);
        string ItemId { get; }
        Uri PageUrl { get; }
        string SortOrder { get; }
        string SearchString { get; }
        int PageIndex { get; }
        string FixedFilter { get; }
        string FixedValue { get; }
        string SelectedId { get; set; }
        public void LoadDetails();
        int ButtonsCount { get; }
        IButtonHelper GetButton(int i);
    }
}
