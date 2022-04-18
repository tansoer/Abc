
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using System;
using System.Collections.Generic;

namespace Abc.Pages.Common {
    public abstract class ButtonsPage<TR, TO, TV, TD> :BasePage<TR, TO, TD>
        where TR : class, IRepo<TO>
        where TO : IBaseEntity<TD>
        where TV : BaseView
        where TD : BaseData, new() {
        protected ButtonsPage(TR r) : base(r) => addButtons();
        public int ButtonsCount => Buttons.Count;
        public IButtonHelper GetButton(int i) => isCorrectIndex(i, Buttons) ? Buttons[i] : null;
        public List<IButtonHelper> Buttons { get; } = new List<IButtonHelper>();
        protected virtual void addButtons() {
            createLocalButton(Captions.Edit);
            createLocalButton(Captions.Details);
            createLocalButton(Captions.Delete);
        }
        internal void createLocalButton(string caption, string action = null)
            => Buttons.Add(new LocalButton(caption, action));
        internal void createForeignButton(string caption, string action, Uri url, string fixedFilter)
            => Buttons.Add(new ForeignButton(caption, action, url, fixedFilter));
    }
}
