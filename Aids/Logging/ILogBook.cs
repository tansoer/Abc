using System;

namespace Abc.Aids.Logging {

    public interface ILogBook {
        void WriteEntry(string message);

        void WriteEntry(Exception e);
    }

}


