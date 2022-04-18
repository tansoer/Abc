using System.Collections.Generic;

namespace Abc.Core.Units {

    public class UnitInfo {

        public UnitInfo(string id, string code, string name, string definition) {
            Id = id;
            Name = name ?? Id;
            Code = code ?? Name;
            Definition = definition;
            Terms = new List<TermInfo>();
        }

        public UnitInfo(string id, double factor) : this(id, null, null, null, factor) { }

        public UnitInfo(string id, string code, double factor) : this(id, code, null, null, factor) { }

        public UnitInfo(string id, string code, string name, double factor) : this(id, code, name, null, factor) { }

        public UnitInfo(string id, string code, string name, string definition, double factor) :
            this(id, code, name, definition) => Factor = factor;

        public UnitInfo(string id, params TermInfo[] terms) : this(id, null, null, null, terms) { }

        public UnitInfo(string id, string code, params TermInfo[] terms) : this(id, code, null, null, terms) { }

        public UnitInfo(string id, string code, string name, params TermInfo[] terms) :
            this(id, code, name, null, terms) { }

        public UnitInfo(string id, string code, string name, string definition, params TermInfo[] terms) : this(id, code,
            name, definition) =>
            Terms.AddRange(terms);

        public string Id;
        public string Code;
        public string Name;
        public string Definition;
        public double Factor;
        public List<TermInfo> Terms;

    }

}
