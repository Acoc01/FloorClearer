using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private static PlayerUI _instance;

    public static PlayerUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerUI>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("PlayerUI");
                    _instance = obj.AddComponent<PlayerUI>();
                }
            }
            return _instance;
        }
    }

    public SpellUI spellUI;
    public HealthBar healthBar;
    public PointCounter pointCounter;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

}
