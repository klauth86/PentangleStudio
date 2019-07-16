using System.Linq;
using BridgeMaster.Characters.Player;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Location {

    public class Location_Starter : MonoBehaviour {

        [SerializeField] private Locations _currentLocation;

        private void Start() {
            Location_Master.LocationSession.CurrentLocation = _currentLocation;
            PlaceThePlayer(Location_Master.LocationSession.PreviousLocation);
            Location_Master.LocationSession.EnterLocation();
        }

        private void PlaceThePlayer(Locations previousLocation) {
            var tPoints = transform.root.GetComponentsInChildren<Location_TransitionPoint>();
            var startPoint = tPoints.FirstOrDefault(e => e.NextLocation == previousLocation);
            if (startPoint) {
                startPoint.SetPreviousHit();
                Player.Instance.MyTransform.position = startPoint.transform.position;
                Player.Instance.MyTransform.localScale = startPoint.transform.localScale;
            }
        }
    }
}
