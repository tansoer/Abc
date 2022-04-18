using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Methods;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Features;

namespace Abc.Domain.Products {

    public abstract class PoleomorphProduct<T> : BaseProduct<T> where T : IProductType {

        protected PoleomorphProduct(ProductData d = null) : base(d) { }

        public override IReadOnlyList<Feature> Features {
            get {
                var l = new List<Feature>();

                if (Type is null) return l.AsReadOnly();

                foreach (var ft in Type.Features) {
                    l.AddRange(ft
                        .PossibleValues
                        .Select(v => copyMembers(v.Data))
                        .Select(d => new Feature(d)));
                }

                return l.AsReadOnly();
            }
        }

        private FeatureData copyMembers(PossibleFeatureValueData v) {
            var d = new FeatureData();
            Copy.Members(v, d);
            d.ProductId = Id;

            return d;
        }

        public override string Details => Type?.Details ?? Unspecified.String;
        public override string Name => Type?.Name ?? Unspecified.String;
        public override string Code => Type?.Code ?? Unspecified.String;
        public override DateTime ValidFrom => Type?.ValidFrom ?? Unspecified.ValidFromDate;
        public override DateTime ValidTo => Type?.ValidTo ?? Unspecified.ValidToDate;

    }

}