namespace Abc.Core.Units {

    public class TermInfo {

        public TermInfo(string termId, sbyte power = 1) {
            TermId = termId;
            Power = power;
        }
        public string TermId;
        public sbyte Power;

    }

}