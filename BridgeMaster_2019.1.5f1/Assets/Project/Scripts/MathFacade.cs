using System;
using UnityEngine;

namespace BridgeMaster {
    public static class MathFacade {
        public static float Min(params float[] values) {
            return Mathf.Min(values);
        }

        public static float Max(params float[] values) {
            return Mathf.Max(values);
        }

        public static float Sign(float value) {
            return Mathf.Sign(value);
        }
    }
}
