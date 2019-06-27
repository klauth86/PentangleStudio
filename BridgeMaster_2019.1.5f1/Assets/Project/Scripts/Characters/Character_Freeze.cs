namespace BridgeMaster.Characters {
    class Character_Freeze : Base<Master> {
        public void ToggleFreeze() {
            Master.IsFreezed = !Master.IsFreezed;
            Master.ToggleFreeze();
        }
    }
}