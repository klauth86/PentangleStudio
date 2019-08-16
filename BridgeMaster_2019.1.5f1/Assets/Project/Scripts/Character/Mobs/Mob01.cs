using BridgeMaster.Characters.AI;
using BridgeMaster.Engine;

namespace BridgeMaster.Characters.Mobs {
    public class Mob01 : AIMaster {

        #region CTOR

        public Mob01() : base(CharacterStateFactory.GetMob01State()) {}

        #endregion
    }
}
