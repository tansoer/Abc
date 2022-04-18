using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Facade.Classifiers;
using Abc.Pages.Classifiers;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using static Abc.Tests.Pages.Classifiers.ClassifierPageTests;

namespace Abc.Tests.Pages.Classifiers {
    public abstract class ClassifierPageBaseTests<TPage> :SealedViewPageTests<
        TPage, IClassifiersRepo, IClassifier, ClassifierView, ClassifierData> 
        where TPage : ClassifierPage<TPage> {
        private IClassifiersRepo repo;
        private int count;
        protected override IClassifier toObject(ClassifierData d) {
            d.ClassifierType = classifierType;
            return ClassifierFactory.Create(d);
        }
        protected override TPage createObject() {
            repo = new classifierRepo();
            count = addRandomItems(repo, classifierType);
            return createObject(repo);
        }
        protected override Type getBaseClass() => typeof(ClassifierPage<TPage>);
        internal abstract TPage createObject(IClassifiersRepo repo);
        internal abstract ClassifierType classifierType { get; }
        [TestMethod] public async Task OnGetIndexAsyncTest() {
            isNull(obj.Items);
            await obj.OnGetIndexAsync(null, null, null, null, null, null, null);
            areEqual(count, obj.Items.Count);
        }
    }
}
