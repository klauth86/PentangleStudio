using BridgeMaster.Base;
using BridgeMaster.Dicts;

namespace BridgeMaster.Location {
    public class LocationMaster : EventRoot {

        public Locations PreviousLocation;
        public Locations CurrentLocation;

        public event EventHandler EnterLocationEvent;
        public event EventHandler LocationEnteredEvent;

        public event EventHandler<Locations> ExitLocationEvent;
        public event EventHandler<Locations> LocationExitedEvent;

        public void EnterLocation() {
            EnterLocationEvent?.Invoke();
        }

        public void LocationEntered() {
            LocationEnteredEvent?.Invoke();
        }

        public void ExitLocation(Locations nextLocation) {
            ExitLocationEvent?.Invoke(nextLocation);
        }

        public void LocationExited(Locations nextLocation) {
            LocationExitedEvent?.Invoke(nextLocation);
        }

        #region SINGLETON

        public static LocationMaster Instance;

        protected override void CallOnAwakeEvent() {

            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }

            base.CallOnAwakeEvent();
        }

        private LocationMaster() {}

        #endregion
    }
}