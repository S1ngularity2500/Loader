using System;

namespace DLLInjection {
    [Serializable]
    public class DLLInjectionFailedException : Exception {
        public DLLInjectionFailedException(string message) : base(message) { }
    }
}
