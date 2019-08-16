using BridgeMaster.Base;
using BridgeMaster.Dicts;

namespace BridgeMaster.Location {
    public class LocationMaster : EventRoot {

        #region MASTER EVENTS

        public Locations PreviousLocation;
        public Locations CurrentLocation;

        public event EventHandler EnterLocationEvent;
        public void EnterLocation() {
            EnterLocationEvent?.Invoke();
        }

        public event EventHandler LocationEnteredEvent;
        public void LocationEntered() {
            LocationEnteredEvent?.Invoke();
        }



        public event EventHandler<Locations> ExitLocationEvent;
        public void ExitLocation(Locations nextLocation) {
            ExitLocationEvent?.Invoke(nextLocation);
        }

        public event EventHandler<Locations> LocationExitedEvent;
        public void LocationExited(Locations nextLocation) {
            LocationExitedEvent?.Invoke(nextLocation);
        }

        #endregion

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