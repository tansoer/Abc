using Abc.Domain.Orders;
using Abc.Domain.Orders.Terms;
using System.Collections.Generic;

namespace Abc.Tests.Domain.Orders.Terms {
    public sealed class MockTermsAndConditionsManager :MockOrderManager, ITermsAndConditionsManager {
        public MockTermsAndConditionsManager(IOrder o) : base(o) { }

        public IReadOnlyList<TermsAndConditions> Terms => registerMethod<IReadOnlyList<TermsAndConditions>>();

        public bool Amend(AmendTermsAndConditionsEvent e) => registerMethod<bool>(e);
    }
}