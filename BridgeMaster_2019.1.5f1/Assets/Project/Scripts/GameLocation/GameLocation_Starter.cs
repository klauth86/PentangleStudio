using System.Linq;
using BridgeMaster.Characters.Player;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.GameLocation {

    public class GameLocation_Starter : MonoBehaviour {

        [SerializeField] private Location _currentLocation;

        private void Start() {
            GameLocation_Master.LocationSession.CurrentLocation = _currentLocation;
            PlaceThePlayer(GameLocation_Master.LocationSession.PreviousLocation);
            GameLocation_Master.LocationSession.EnterLocation();
        }

        private void PlaceThePlayer(Location previousLocation) {
            var tPoints = transform.root.GetComponentsInChildren<GameLocation_TransitionPoint>();
            var startPoint = tPoints.FirstOrDefault(e => e.NextLocation == previousLocation);
            if (startPoint) {
                startPoint.SetPreviousHit();
                Player.PlayerSession.MyTransform.position = startPoint.transform.position;
                Player.PlayerSession.MyTransform.localScale = startPoint.transform.localScale;
            }
        }
    }
}
