using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Common.Aids {
    public sealed class ComponentArgs {
        public ComponentArgs(string propertyName, bool isHidden = false, 
            IEnumerable<SelectListItem> selectList = null, object defaultValue = null) {
            PropertyName = propertyName;
            SelectList = selectList;
            IsHidden = isHidden;
            DefaultValue = defaultValue;
        }
        public string PropertyName { get; set; }
        public IEnumerable<SelectListItem> SelectList { get; set; }
        public bool IsHidden { get; set; }
        public object DefaultValue { get; set; }   
    }
}
