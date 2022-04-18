using System;

namespace Abc.Pages.Common.Extensions {
    public interface IButtonForCreateDropDown<TPage, TDropDownEnum> {
        int DropDownEntryCount { get; }
        Uri GetDropDownEntryUri(int i);
        string GetDropDownEntryName(int i);
    }
}
