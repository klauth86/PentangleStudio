using System;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.GameLocation {

    public class GameLocation_Starter : MonoBehaviour {

        [SerializeField] private Location _currentLocation;

        private void Start() {
            Master.Session.CurrentLocation = _currentLocation;
            PlaceThePlayer(Master.Session.PreviousLocation, Master.Session.CurrentLocation);
            Master.Session.EnterLocation();
        }

        private void PlaceThePlayer(Location previousLocation, Location currentLocation) {
            throw new NotImplementedException();
        }
    }
}
