using Base;
using UnityEngine;

public class Game : MonoBehaviour {

    [SerializeField] private int _size;
    [SerializeField] private int _bombs;

    // Use this for initialization
    void Start () {
		
	}

    void CreateBoard() {
        var board = new Board(2, _size, _bombs);

    }
}
