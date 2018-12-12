using UnityEngine;
using TMPro;

public class GameSingleton : MonoBehaviour {
    public static GameSingleton Singleton;

    [SerializeField] private SceneLoader _sceneLoader;
    public SceneLoader GetSceneLoader { get { return _sceneLoader; } }

    // Score
    [SerializeField] private ResourceUnit _resources;

    // UI
    [SerializeField] private TMP_Text _manaText;
    [SerializeField] private TMP_Text _oreText;
    [SerializeField] private TMP_Text _woodText;
    [SerializeField] private TMP_Text _ropeText;

    //Level
    [SerializeField] private Transform[] _islands;
    public Transform[] GetIslands { get { return _islands; } }

    private void Awake() {
        if (Singleton == null) {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            gameObject.SetActive(false);
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start() {
        UpdateReources();
    }

    public void TryCast(Spell selectedSpell) {
        if (selectedSpell) {
            if (_resources.Mana <= selectedSpell.Cost.Mana &&
                _resources.Ore <= selectedSpell.Cost.Ore &&
                _resources.Wood <= selectedSpell.Cost.Wood &&
                _resources.Rope <= selectedSpell.Cost.Rope) {
                _resources.Mana = _resources.Mana - selectedSpell.Cost.Mana;
                _resources.Ore = _resources.Ore - selectedSpell.Cost.Ore;
                _resources.Wood = _resources.Wood - selectedSpell.Cost.Wood;
                _resources.Rope = _resources.Rope - selectedSpell.Cost.Rope;
                selectedSpell.Cast();
            }
            UpdateReources();
        }
    }

    public void Pickup(ResourceUnit resourceUnit) {
        _resources.Mana = _resources.Mana + resourceUnit.Mana;
        _resources.Ore = _resources.Ore + resourceUnit.Ore;
        _resources.Wood = _resources.Wood + resourceUnit.Wood;
        _resources.Rope = _resources.Rope + resourceUnit.Rope;
        UpdateReources();
    }

    private void UpdateReources() {
        if (_manaText)
            _manaText.text = "Mana: " + _resources.Mana.ToString();
        if (_oreText)
            _oreText.text = "Ore: " + _resources.Ore.ToString();
        if (_woodText)
            _woodText.text = "Wood: " + _resources.Wood.ToString();
        if (_ropeText)
            _ropeText.text = "Rope: " + _resources.Rope.ToString();
    }
}
