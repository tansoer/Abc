using Abc.Aids.Enums;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Abc.Tests.Soft.Areas.Party.Pages.BodyMetrics {
    public abstract class BodyMetricsTests :BasePartyTests<BodyMetricView, BodyMetricData> {
        protected MetricType? metricType;
        protected string partyId;
        protected List<SelectListItem> types 
            => db.BodyMetricTypes.Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected List<SelectListItem> units 
            => quantityDb.Units.Select(c => new SelectListItem(c.Name, c.Id)).ToList();
        protected override string baseUrl() => PartyUrls.BodyMetrics;
        protected override void addRelatedItems(BodyMetricData d) {
            if (d is null) return;
            addPartyNameForPartyId(d.PartyId);
            addBodyMetricType(d.TypeId);
            correctMetricValue(d);
        }
        private void correctMetricValue(BodyMetricData d) {
            if (d is null) return;
            if (isQuantity(d.MetricType)) {
                d.Value = random<double>().ToString(CultureInfo.InvariantCulture);
                addUnit(d.UnitId);
            } else if (isString(d.MetricType)) {
                d.UnitId = null;
            } else {
                d.Value = null;
                d.UnitId = null;
            }
        }
        private static void addBodyMetricType(string id) {
            var d = random<BodyMetricTypeData>();
            d.Id = id;
            add<IBodyMetricTypesRepo, BodyMetricType>(new BodyMetricType(d));
        }
        protected override IEnumerable<Expression<Func<BodyMetricView, object>>> indexPageColumns =>
            new Expression<Func<BodyMetricView, object>>[] {
                x => x.MetricType,
                x => x.TypeId,
                x => x.Name,
                x => x.Code,
                x => x.Details,
                x => x.SignatureId,
                x => x.Value,
                x => x.ValidFrom,
                x => x.ValidTo,
            };
        protected override void dataInDetailsPage() {
            var v = toView(item) as BodyMetricView;
            canView(v, x => x.MetricType);
            canView(v, x => x.TypeId, "Unspecified");
            canView(v, x => x.Name);
            canView(v, x => x.Code);
            canView(v, x => x.Details);
            canView(v, x => x.SignatureId);
            canView(v, x => x.Value,
                new BodyMetricViewFactory().Create(BodyMetricFactory.Create(item)).ToString());
            canView(v, x => x.ValidFrom);
            canView(v, x => x.ValidTo);
        }
        private static bool isQuantity(MetricType? t) => t == MetricType.Quantity;
        private static bool isString(MetricType? t) => t == MetricType.String;
        protected override BodyMetricView toView(BodyMetricData d) {
            var o = BodyMetricFactory.Create(d);
            var v = new BodyMetricViewFactory().Create(o);
            return v;
        }
        protected override void dataInEditPage() {
            var v = toView(item) as BodyMetricView;
            canSelect(v, x => x.TypeId, types);
            canEdit(v, x => x.Name, true);
            canEdit(v, x => x.Code);
            canEdit(v, x => x.Details);
            if (isString(v?.MetricType)) canEdit(v, x => x.StringValue);
            if (isQuantity(v?.MetricType)) canSelect(v, x => x.UnitId, units);
            if (isQuantity(v?.MetricType)) canEdit(v, x => x.QuantityValue, true, true);
            canEdit(v, x => x.ValidFrom);
            canEdit(v, x => x.ValidTo);
        }
        protected override void dataInCreatePage() {
            canSelect(null, x => x.TypeId, types);
            canEdit(null, x => x.Name, true);
            canEdit(null, x => x.Code);
            canEdit(null, x => x.Details);
            if (isString(metricType)) canEdit(null, x => x.StringValue);
            if (isQuantity(metricType)) canSelect(null, x => x.UnitId, units);
            if (isQuantity(metricType)) canEdit(null, x => x.QuantityValue, true, true, 0);
            canEdit(null, x => x.ValidFrom);
            canEdit(null, x => x.ValidTo);
        }
        protected override void dataHiddenInPage() {
            hasHidden(item, x => x.MetricType, true);
            hasHidden(item, x => x.Id, true);
            hasHidden(item, x => x.PartyId);
            hasHidden(item, x => x.SignatureId);
        }
        protected override void changeValuesExceptId(BodyMetricData d) {
            base.changeValuesExceptId(d);
            d.Value = random<double>().ToString(CultureInfo.InvariantCulture);
        }
        protected override BodyMetricData correctOriginal(BodyMetricData oldItem, BodyMetricData newItem) {
            oldItem.MetricType = newItem.MetricType;
            update<IBodyMetricsRepo, IBodyMetric>(BodyMetricFactory.Create(oldItem));
            return oldItem;
        }
        protected override void validateItems(BodyMetricData d1, BodyMetricData d2) {
            if (isString(d1.MetricType)) allPropertiesAreEqual(d1, d2, nameof(d1.UnitId));
            else if (isQuantity(d1.MetricType)) allPropertiesAreEqual(d1, d2);
            else allPropertiesAreEqual(d1, d2, nameof(d1.UnitId), nameof(d1.Value));
        }
        protected override BodyMetricData randomItem() {
            var d = base.randomItem();
            d.MetricType = metricType ?? d.MetricType;
            return d;
        }
        protected override string pageUrl() {
            var url = base.pageUrl();
            if (metricType is null) return url;
            partyId = random<string>();
            var typeIdx = Convert.ToInt32(metricType);
            url += $"&fixedFilter=PartyId&fixedValue={partyId}&switchOfCreate={typeIdx}";
            return url;
        }
        [DataTestMethod]
        [DataRow(MetricType.Unspecified)]
        [DataRow(MetricType.Quantity)]
        [DataRow(MetricType.String)]
        public void CanDisplayDataTest(MetricType t) {
            metricType = t;
            CanDisplayDataTest();
        }
    }
    [TestClass] public class CreatePageTests :BodyMetricsTests {
        [DataTestMethod]
        [DataRow(MetricType.Unspecified)]
        [DataRow(MetricType.Quantity)]
        [DataRow(MetricType.String)]
        public void CanClickCreateButtonTest(MetricType t) {
            metricType = t;
            canClickCreateButtonTest();
        }
    }
    [TestClass] public class DeletePageTests :BodyMetricsTests {
        [DataTestMethod]
        [DataRow(MetricType.Unspecified)]
        [DataRow(MetricType.Quantity)]
        [DataRow(MetricType.String)]
        public void CanClickDeleteButtonTest(MetricType t) {
            metricType = t;
            canClickDeleteButtonTest();
        }
    }
    [TestClass] public class DetailsPageTests :BodyMetricsTests {
        [DataTestMethod]
        [DataRow(MetricType.Unspecified)]
        [DataRow(MetricType.Quantity)]
        [DataRow(MetricType.String)]
        public void CanClickEditButtonInDetailsTest(MetricType t) {
            metricType = t;
            canClickEditButtonInDetailsTest();
        }
    }
    [TestClass] public class EditPageTests :BodyMetricsTests {
        [DataTestMethod]
        [DataRow(MetricType.Unspecified)]
        [DataRow(MetricType.Quantity)]
        [DataRow(MetricType.String)]
        public void CanClickEditButtonTest(MetricType t) {
            metricType = t;
            canClickEditButtonTest();
        }
    }
    [TestClass] public class IndexPageTests :BodyMetricsTests { }
}