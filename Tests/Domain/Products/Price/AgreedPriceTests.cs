using Abc.Data.Products;
using Abc.Data.Rules;
using Abc.Domain.Products.Price;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Price {

    [TestClass] public class AgreedPriceTests :SealedTests<AgreedPrice, AppliedPrice> {
        [TestMethod] public void RuleOverrideIdTest() => isReadOnly(obj.Data.RuleOverrideId);
        [TestMethod] public void PossiblePriceIdTest() => isReadOnly(obj.Data.PossiblePriceId);

        [TestMethod] public async Task RuleOverrideTest() =>
            await testPartyAttributeAsync<RuleOverrideData, RuleOverride, IRuleOverridesRepo>(
                obj.RuleOverrideId, () => obj.RuleOverride.Data, d => new RuleOverride(d));

        [TestMethod] public async Task PossiblePriceTest() =>
            await testItemAsync<PriceData, IPrice, IPricesRepo>(
                obj.PossiblePriceId, () => obj.PossiblePrice.Data, d => {
                    if (d is null) return new PriceError();
                    d.Kind = PriceKind.Possible;

                    return new PossiblePrice(d);
                });

    }

}
