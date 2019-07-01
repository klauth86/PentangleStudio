using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.GameLocation {
    public class Master : MonoBehaviour {

        public Location PreviousLocation;
        public Location CurrentLocation;

        public event GameEventHandler EnterLocationEvent;
        public event GameEventHandler LocationEnteredEvent;

        public event GameEventHandler<Location, Location> ExitLocationEvent;
        public event GameEventHandler<Location, Location> LocationExitedEvent;

        public void EnterLocation() {
            EnterLocationEvent?.Invoke();
        }

        public void LocationEntered() {
            LocationEnteredEvent?.Invoke();
        }

        public void ExitLocation(Location nextLocation) {
            ExitLocationEvent?.Invoke(CurrentLocation, nextLocation);
        }

        public void LocationExited(Location nextLocation) {
            LocationExitedEvent?.Invoke(CurrentLocation, nextLocation);
        }

        #region SINGLE SESSION

        public static Master LocationSession;

        private void Awake() {
            if (LocationSession == null) {
                LocationSession = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}