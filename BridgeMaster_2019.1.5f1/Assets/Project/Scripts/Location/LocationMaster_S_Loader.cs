using BridgeMaster.Base;
using BridgeMaster.Characters.Player;
using BridgeMaster.Dicts;
using System.Linq;
using UnityEngine.SceneManagement;

namespace BridgeMaster.Location {

    public class LocationMaster_S_Loader : ANY_Subscriber<LocationMaster> {

        #region CTOR

        public LocationMaster_S_Loader() : base((eventRoot) => LocationMaster.Instance) { }

        #endregion

        private void Start() {
            Master.CurrentLocation = (Locations)SceneManager.GetActiveScene().buildIndex;

            PlaceThePlayer(Master.PreviousLocation);
            Master.EnterLocation();
        }

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.LocationExitedEvent += LocationExited;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.LocationExitedEvent -= LocationExited;
                base.Unsubscribe();
            }
        }

        private void LocationExited(Locations nextLocation) {
            Master.PreviousLocation = Master.CurrentLocation;
            Master.CurrentLocation = Locations.None;
            SceneManager.LoadScene((int)nextLocation);
        }

        private void PlaceThePlayer(Locations previousLocation) {
            var tPoints = transform.root.GetComponentsInChildren<LocationMaster_H_TransitionPoint>();
            var startPoint = tPoints.FirstOrDefault(e => e.NextLocation == previousLocation);
            if (startPoint) {
                startPoint.HasPrevHit = true;
                PlayerMaster.Instance.Transform.position = startPoint.transform.position;
                PlayerMaster.Instance.Transform.localScale = startPoint.transform.localScale;
            }
        }
    }
}
