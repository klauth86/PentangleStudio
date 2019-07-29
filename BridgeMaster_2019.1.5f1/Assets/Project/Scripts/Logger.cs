using UnityEngine;

namespace BridgeMaster {
    public static class Logger {
        public static void LogIsNotSetInInspectorError(string varName, string monoBehaviourName, string gameObjectName) {
            Debug.LogError($"Variable {varName} in {monoBehaviourName} script on {gameObjectName} GameObject is not set in the Inspector!");
        }
    }
}
