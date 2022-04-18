using Abc.Data.Orders;
using System;
using System.Collections.Generic;

namespace Abc.Domain.Orders.Lines {
    public abstract class TaxableLine : BaseOrderLine {
        protected TaxableLine() : this(null) { }
        protected TaxableLine(OrderLineData d) : base(d) { }
        public IReadOnlyList<TaxLine> TaxLines => list<TaxLine, IOrderLine>(relatedLines);
        protected internal bool add(TaxLine l) => !isTaxLine(l) && add<IOrderLinesRepo, IOrderLine>(newTaxLine(l));
        protected internal bool remove(TaxLine l) => isTaxLine(l) && deleteTaxLine(l);
        internal static bool deleteTaxLine(TaxLine l) {
            if (l is null) return false;
            var d = l.Data;
            d.ValidTo = DateTime.Now;
            return update<IOrderLinesRepo, IOrderLine>(new TaxLine(d));
        }
        //TODO Gunnar: Me ei pane siia vigade töötlust sisse. 
        //Vigade töötluse teeme hiljem eraldi funktsionaalsuse, Kui AmendLine väärtust on false
        //siis käivitub AmendOrderLineEvent analüüs, et kõik vead korraga kasutajale raporteerida 
        public virtual bool AmendLine(AmendOrderLineEvent e) {
            if (!e.IsCorrect(OrderLineType.TaxLine)) return false;
            if (e.IsNewEvent) return add(e.OrderLine as TaxLine);
            if (e.IsRemoveEvent) return remove(e.OldOrderLine as TaxLine);
            return remove(e.OldOrderLine as TaxLine) && add(e.OrderLine as TaxLine);
        }
        internal bool isTaxLine(TaxLine l) => isListed(TaxLines, l.Id);
        internal TaxLine newTaxLine(TaxLine l)
            => OrderLineFactory.Create(newLine(l, OrderLineType.TaxLine)) as TaxLine;
    }
}