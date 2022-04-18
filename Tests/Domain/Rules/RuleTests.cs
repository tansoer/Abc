using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class RuleTests : SealedTests<Rule, BaseRule> {

        //TODO: Is not implemented

        //[TestMethod]
        //public void LogicOperations()
        //{
        //    void Test(bool x, bool y, double z, double q, bool r)
        //    {
        //        composeBooleanRule(obj);
        //        var c = composeBooleanRuleContext(x, y, z, q);
        //        var o = obj.Compute(c) as BooleanVariable;
        //        Assert.IsNotNull(o);
        //        Assert.AreEqual(obj.Name, o.Name);
        //        Assert.AreEqual(r, o.Value);
        //    }
        //    Test(true, false, 4.5, 5.0, true);
        //    Test(false, false, 4.5, 5.0, false);
        //    Test(true, false, 5.5, 5.0, false);
        //}
        //internal static RuleContext composeBooleanRuleContext(bool x, bool y, double z, double q)
        //{
        //    var c = new RuleContext();
        //    c.Variables.Add(Variable.Create("IsColdCardHolder", x));
        //    c.Variables.Add(Variable.Create("IsSilverCardHolder", y));
        //    c.Variables.Add(Variable.Create("CarryOnBagageKg", z));
        //    c.Variables.Add(Variable.Create("AllowedBagageKg", q));
        //    return c;
        //}
        //internal static void composeBooleanRule(Rule r)
        //{
        //    r.Elements.Clear();
        //    r.Elements.Add(new Operand { Name = "IsColdCardHolder" });
        //    r.Elements.Add(new Operand { Name = "IsSilverCardHolder" });
        //    r.Elements.Add(new Operator { Operation = Operation.Or });
        //    r.Elements.Add(new Operand { Name = "CarryOnBagageKg" });
        //    r.Elements.Add(new Operand { Name = "AllowedBagageKg" });
        //    r.Elements.Add(new Operator { Operation = Operation.Less });
        //    r.Elements.Add(new Operator { Operation = Operation.And });
        //}


    }

}
