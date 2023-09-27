using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour, IHealthBar
{
    private static Slider _slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    private Enemy _enemy;

    private void Awake()
    {
        try
        {
            _enemy = GameManager.Instance.CurrentEnemy;
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e);
        }
        _slider = gameObject.GetComponent<Slider>();
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        _slider.maxValue = _enemy.MaxHealth;
        _slider.value = _enemy.Health;
        fill.color = gradient.Evaluate(1f);
    }
    
    public void SetHealth()
    {
        _slider.value = _enemy.Health;
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
