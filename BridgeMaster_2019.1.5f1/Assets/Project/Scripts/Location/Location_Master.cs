using BridgeMaster.Base;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Location {
    public class Location_Master : MonoBehaviour {

        public Locations PreviousLocation;
        public Locations CurrentLocation;

        public event EventHandler EnterLocationEvent;
        public event EventHandler LocationEnteredEvent;

        public event EventHandler<Locations, Locations> ExitLocationEvent;
        public event EventHandler<Locations, Locations> LocationExitedEvent;

        public void EnterLocation() {
            EnterLocationEvent?.Invoke();
        }

        public void LocationEntered() {
            LocationEnteredEvent?.Invoke();
        }

        public void ExitLocation(Locations nextLocation) {
            ExitLocationEvent?.Invoke(CurrentLocation, nextLocation);
        }

        public void LocationExited(Locations nextLocation) {
            LocationExitedEvent?.Invoke(CurrentLocation, nextLocation);
        }

        #region SINGLETON

        public static Location_Master LocationSession;

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

        private Location_Master() {}

        #endregion
    }
}