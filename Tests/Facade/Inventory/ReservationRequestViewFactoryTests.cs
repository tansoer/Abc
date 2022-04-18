using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Inventory;
using Abc.Tests.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Inventory {
    [TestClass]
    public class ReservationRequestViewFactoryTests :ViewFactoryTests<ReservationRequestViewFactory, 
        ReservationRequestData, ReservationRequest, ReservationRequestView> {
    }
}
