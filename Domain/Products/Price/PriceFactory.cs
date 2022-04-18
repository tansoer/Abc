using Abc.Data.Products;

namespace Abc.Domain.Products.Price {

    public static class PriceFactory {

        public static BasePrice Create(PriceData d) {
            d ??= new PriceData();

            return d.Kind switch
            {
                PriceKind.Unspecified => error(d),
                PriceKind.Possible => new PossiblePrice(d),
                PriceKind.Agreed => new AgreedPrice(d),
                PriceKind.Arbitrary => new ArbitraryPrice(d),
                PriceKind.Discount => new PriceDiscount(d),
                _ => error(d)
            };
        }
        private static BasePrice error(PriceData d) => new PriceError(d);


    }

}