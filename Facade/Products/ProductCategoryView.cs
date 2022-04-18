using System.ComponentModel;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class ProductCategoryView : EntityBaseView {

        [DisplayName("Base Category")]
        public string BaseCategoryId { get; set; }


    }

}