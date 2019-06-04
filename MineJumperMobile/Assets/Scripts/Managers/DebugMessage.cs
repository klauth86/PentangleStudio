using UnityEngine;

namespace Managers {
    public static class DebugMessage {
        public static void LogNotSetupWarningMessage(string name) {
            Debug.Log(name + " is not set up in Inspector");
        }
    }
}
