namespace Abc.Core.Rounding {
    public interface IRoundingPolicy {

        double Round(double amount);
        decimal Round(decimal amount);

    }
}
