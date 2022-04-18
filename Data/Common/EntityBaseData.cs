namespace Abc.Data.Common {
    public abstract class EntityBaseData: DetailedData, IEntityBaseData  {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
