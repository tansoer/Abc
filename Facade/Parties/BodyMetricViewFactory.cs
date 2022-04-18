using System.Globalization;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Common;

namespace Abc.Facade.Parties {

    public sealed class BodyMetricViewFactory : AbstractViewFactory<BodyMetricData, IBodyMetric, BodyMetricView> {
        public override IBodyMetric Create(BodyMetricView v) {
            var d = new BodyMetricData();
            Copy.Members(v, d);

            d.Value = v.MetricType == MetricType.Quantity 
                ? v.QuantityValue.ToString(CultureInfo.InvariantCulture) 
                : v.StringValue;
            return toObject(d);
        }

        public override BodyMetricView Create(IBodyMetric o) {
            var v = base.Create(o);
            if (v.MetricType == MetricType.Quantity) v.QuantityValue = Doubles.ToDouble(o.Data.Value); 
            else v.StringValue = o.Data.Value;

            return v;
        }

        protected internal override IBodyMetric toObject(BodyMetricData d) {
            return BodyMetricFactory.Create(d);
        }
    }

}
