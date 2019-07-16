using BridgeMaster.Dicts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BridgeMaster.Location {

    public class Location_Loader : MonoBehaviour {

        private void OnEnable() {
            Location_Master.LocationSession.LocationExitedEvent += LocationExited;
        }

        private void OnDisable() {
            Location_Master.LocationSession.LocationExitedEvent -= LocationExited;
        }

        private void LocationExited(Locations currentLocation, Locations nextLocation) {
            Location_Master.LocationSession.PreviousLocation = currentLocation;
            SceneManager.LoadScene((int)nextLocation);
        }
    }
}
