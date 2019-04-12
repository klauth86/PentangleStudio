using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class House : MonoBehaviour {
    [SerializeField] private Sprite _playerIn;
    [SerializeField] private Sprite _playerOut;

    private SpriteRenderer _spriteRenderer;

    private bool _playerInside;

    public bool PlayerInside {
        get {
            return _playerInside;
        }
        set {
            _playerInside = value;
            _spriteRenderer.sprite = PlayerInside ? _playerIn : _playerOut;
        }
    }

    // Use this for initialization
    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerInside = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            PlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            PlayerInside = false;
        }
    }
}