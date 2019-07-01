namespace BridgeMaster.Characters {
    class Character_Freeze : Base<Character_Master> {
        public void ToggleFreeze() {
            Master.IsFreezed = !Master.IsFreezed;
            Master.ToggleFreeze();
        }
    }
}