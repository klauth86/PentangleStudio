using BridgeMaster.Dicts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BridgeMaster.GameLocation {

    public class GameLocation_Loader : MonoBehaviour {

        private void OnEnable() {
            Master.Session.LocationExitedEvent += LocationExited;
        }

        private void OnDisable() {
            Master.Session.LocationExitedEvent -= LocationExited;
        }

        private void LocationExited(Location currentLocation, Location nextLocation) {
            Master.Session.PreviousLocation = currentLocation;
            SceneManager.LoadScene((int)nextLocation);
        }
    }
}
