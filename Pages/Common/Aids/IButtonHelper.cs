namespace Abc.Pages.Common.Aids {
    public interface IButtonHelper {
        string Action { get; }
        string Caption { get; }
        string GetUrlString(Args a);
    }
}
