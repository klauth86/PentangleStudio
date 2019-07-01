using BridgeMaster.Dicts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BridgeMaster.GameLocation {

    public class GameLocation_Loader : MonoBehaviour {

        private void OnEnable() {
            GameLocation_Master.LocationSession.LocationExitedEvent += LocationExited;
        }

        private void OnDisable() {
            GameLocation_Master.LocationSession.LocationExitedEvent -= LocationExited;
        }

        private void LocationExited(Location currentLocation, Location nextLocation) {
            GameLocation_Master.LocationSession.PreviousLocation = currentLocation;
            SceneManager.LoadScene((int)nextLocation);
        }
    }
}
