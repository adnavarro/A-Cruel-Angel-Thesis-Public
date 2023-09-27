using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IHealthBar
{
    private static Slider _slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    private PlayerCombat _player;

    private void Awake()
    {
        _player = GameManager.Instance.PlayerCombat;
        _slider = gameObject.GetComponent<Slider>();
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        _slider.maxValue = _player.MaxHealth;
        _slider.value = _player.Health;
        fill.color = gradient.Evaluate(1f);
    }
    
    public void SetHealth()
    {
        _slider.value = _player.Health;
        fill.color = gradient.Evaluate(_slider.normalizedValue);
    }
    
    private void OnEnable()
    {
        EventManager.OnHealthChanged += SetHealth;
    }

    private void OnDisable()
    {
        EventManager.OnHealthChanged -= SetHealth;
    }
}
