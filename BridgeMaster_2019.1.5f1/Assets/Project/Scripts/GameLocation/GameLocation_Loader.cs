using BridgeMaster.Dicts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BridgeMaster.GameLocation {

    public class GameLocation_Loader : MonoBehaviour {

        private void OnEnable() {
            Master.LocationSession.LocationExitedEvent += LocationExited;
        }

        private void OnDisable() {
            Master.LocationSession.LocationExitedEvent -= LocationExited;
        }

        private void LocationExited(Location currentLocation, Location nextLocation) {
            Master.LocationSession.PreviousLocation = currentLocation;
            SceneManager.LoadScene((int)nextLocation);
        }
    }
}
