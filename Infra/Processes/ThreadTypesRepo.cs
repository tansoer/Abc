﻿using Abc.Data.Processes;
using Abc.Domain.Processes;
using Abc.Infra.Common;

namespace Abc.Infra.Processes {
    public sealed class ThreadTypesRepo :EntityRepo<ThreadType, ThreadTypeData>, IThreadTypesRepo {
        public ThreadTypesRepo(ProcessDb c = null) : base(c, c?.ThreadTypes) { }
    }
}
