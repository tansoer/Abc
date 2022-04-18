using System.Collections.Generic;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Methods;
using Abc.Aids.Reflection;
using Abc.Data.Currencies;
using Abc.Data.Quantities;
using Abc.Data.Rules;
using Abc.Domain.Currencies;
using Abc.Domain.Quantities;
using Abc.Domain.Rules;
using Abc.Facade.Rules;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Cryptography.X509Certificates;
using Abc.Aids.Calculator;
using Abc.Pages.Common.Aids;

namespace Abc.Pages.Rules {

    public sealed class ElementsPage :ViewsPage<ElementsPage, IRuleElementsRepo,
        IRuleElement, RuleElementView, RuleElementData, RuleElementType> {
        protected override string title => RuleTitles.RuleElements;
        public ElementsPage(IRuleElementsRepo e) : base(e) {}
        private IEnumerable<SelectListItem> rules;
        private IEnumerable<SelectListItem> contexts;
        private IEnumerable<SelectListItem> currencies;
        private IEnumerable<SelectListItem> units;
        public IEnumerable<SelectListItem> Rules => rules ??= selectListByName<IRulesRepo, BaseRule, RuleData>();
        public IEnumerable<SelectListItem> Contexts => contexts ??= selectListByName<IRuleContextsRepo, RuleContext, RuleContextData>();
        public IEnumerable<SelectListItem> Currencies => currencies ??= selectListByName<ICurrencyRepo, Currency, CurrencyData>();
        public IEnumerable<SelectListItem> Units => units ??= selectListByName<IUnitsRepo, Unit, UnitData>();
        public IEnumerable<SelectListItem> Operations => new SelectList(Enum.GetValues(typeof(Operation)));
        public string ContextName(string id) => itemName(Contexts, id);
        public string RuleName(string id) => itemName(Rules, id);
        public RuleElementType RuleElementType { get; internal set; }

        protected override List<ExpressionHelper> hiddenInputs() => new() {
            field(x => Item.RuleElementType),
            field(x => Item.Id),
            field(x => Item.Name),
        };

        protected override void tableColumns() {
            tableColumn(x => x.Item.Index);
            tableColumn(x => x.Item.RuleElementType);
            tableColumn(x => x.Item.Name);
            tableColumn(x => x.Item.Value);
            tableColumn(x => x.Item.Details);
            tableColumn(x => x.Item.ValidFrom);
            tableColumn(x => x.Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Index);
            valueViewer(x => Item.RuleElementType);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Value);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Index);
            valueEditor(x => Item.RuleId, () => Rules);
            valueEditor(x => Item.RuleContextId, () => Contexts);

            switch (Item.RuleElementType) {
                case RuleElementType.Operand:
                    Item.Name = null;
                    //valueEditor(x => Item.Name);
                    break;
                case RuleElementType.Operator:
                    //Item.Name = null;
                    valueEditor(x => Item.Operation, () => Operations);
                    break;
                case RuleElementType.Boolean:
                    valueEditor(x => Item.BooleanValue);
                    break;
                case RuleElementType.DateTime:
                    valueEditor(x => Item.DateTimeValue);
                    break;
                case RuleElementType.Decimal:
                    valueEditor(x => Item.DecimalValue);
                    break;
                case RuleElementType.Money:
                    valueEditor(x => Item.DecimalValue);
                    valueEditor(x => Item.CurrencyId, () => Currencies);
                    break;
                case RuleElementType.Double:
                    valueEditor(x => Item.DoubleValue);
                    break;
                case RuleElementType.Quantity:
                    valueEditor(x => Item.DoubleValue);
                    valueEditor(x => Item.UnitId, () => Units);
                    break;
                case RuleElementType.Integer:
                    valueEditor(x => Item.IntegerValue);
                    break;
                case RuleElementType.String or RuleElementType.Error or RuleElementType.Unspecified:
                    valueEditor(x => Item.StringValue);
                    break;
            }

            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override IRuleElement toObject(RuleElementView v) => RuleElementViewFactory.Create(v);
        protected internal override RuleElementView toView(IRuleElement o) {
            RuleElementType = o.Type;
            return RuleElementViewFactory.Create(o);
        }
        protected internal override string pageUrl => RuleUrls.RuleElements;
        protected internal override string subtitle =>
            isRuleElements() ? $"{RuleName(FixedValue)}" : $"{ContextName(FixedValue)}";
        private bool isRuleElements()
            => FixedFilter == GetMember.Name<RuleElementView>(x => x.RuleId);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            RuleElementType = ruleElementType(switchOfCreate);
            createItem(fixedFilter, fixedValue);
        }

        private void createItem(string fixedFilter, string fixedValue) {
            Item = new RuleElementView {
                RuleElementType = RuleElementType
            };
            Item.Name = Item.RuleElementType.ToString();
            var isRuleElement = IsRuleElement(fixedFilter);
            if (IsRuleElement(fixedFilter))
                Item.RuleId = fixedValue;
            else Item.RuleContextId = fixedValue;
            Item.Index = db.GetNextElementIndex(isRuleElement, fixedValue);
        }
        public bool IsRuleElement(string fixedFilter)
            => nameof(Item.RuleId) == fixedFilter;
        internal static RuleElementType ruleElementType(int? elementType)
            => Safe.Run(() => (RuleElementType)(elementType ?? 1000), RuleElementType.Unspecified);
    }
}