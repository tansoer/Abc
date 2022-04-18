using System;
using System.Collections.Generic;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Parties.Signatures;

namespace Abc.Domain.Products {

    public sealed class Batch : Entity<BatchData> {
        internal static string batchId => nameOf<ProductData>(x => x.BatchId);
        public Batch() : this(null) { }
        public Batch(BatchData d) : base(d) { }
        internal string typeId => get(Data?.ProductTypeId);
        public string FirstSerialNumber => get(Data?.FirstSerialNumber);
        public string LastSerialNumber => get(Data?.LastSerialNumber);
        public IProductType BatchOf => item<IProductTypesRepo, IProductType>(typeId);
        public int ProductsCount => get(Products?.Count);
        public DateTime SellBy => get(Data?.SellBy);
        public DateTime UseBy => get(Data?.UseBy);
        public DateTime BestBefore => get(Data?.BestBefore);
        public DateTime DateProduced => get(Data?.DateProduced);
        public IReadOnlyList<IProduct> Products => list<IProductsRepo, IProduct>(batchId, Id);
        public IReadOnlyList<PartySignature> CheckedBy => list(checkedBy, e => e.PartySignature);
        internal IReadOnlyList<BatchCheckedBy> checkedBy => list<IBatchCheckedByRepo, BatchCheckedBy>(batchId, Id);
    }
}
