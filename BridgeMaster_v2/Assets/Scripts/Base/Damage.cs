namespace Base {
    public enum DamageType {
        Nonmagic, Air, Earth, Fire, Water
    }

    [System.Serializable]
    public class Damage {
        public DamageType Type;
        public int Hit;
    }
}


