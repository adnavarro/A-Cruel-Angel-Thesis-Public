using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
	public Sprite sprite;
	public TMP_FontAsset font;
	[SerializeField] private int id;
	[SerializeField] private List<SymptomAttached> enemySymptoms;

	#region CombatAtributtes
	
	private int _phase;
	public List<AttackAbility> attackAbilities;
	[SerializeField] private List<float> phaseDistribution;
	[SerializeField] private int maxPhases;
	[SerializeField] private int health; 
	[SerializeField] private int maxHealth;
	[SerializeField] private int baseAttack;
	public List<Dialogue> dialoguePhase;
	public List<Dialogue> dialoguePresentation;
	[FormerlySerializedAs("dialogueAttack")] public List<Dialogue> dialogueCommentary;
	
	#endregion

	#region ActAtributtes
	
	private int _depressionPhase;
	[SerializeField] private int maxDepressionPhase;
	public List<ActOption> actOptions;
	
	#endregion

	#region Properties

	public int DepressionPhase
	{
		get => _depressionPhase;
		set => _depressionPhase = value;
	}

	public int MaxDepressionPhase => maxDepressionPhase;

	public int Health
	{
		get => health;
		set
		{
			health = value;
			if (maxHealth > health)
			{
				health = maxHealth;
			}
		}
	}

	public int BaseAttack => baseAttack;

	public int Phase
	{
		get => _phase;
		set => _phase = value;
	}

	public int MaxHealth
	{
		get => maxHealth;
		private set
		{
			maxHealth = value;
			if (maxHealth < health)
			{
				maxHealth = health * 2;
			}
		}
	}

	public int Id => id;

	public List<SymptomAttached> EnemySymptoms => enemySymptoms;

	#endregion

	public void IncreaseEncounterCount()
	{
		if (PlayerPrefs.HasKey("EnemyId" + Id))
		{
			int encounterCount = PlayerPrefs.GetInt("EnemyId" + Id);
			encounterCount++;
			PlayerPrefs.SetInt("EnemyId" + Id, encounterCount);
		}
		else
		{
			PlayerPrefs.SetInt("EnemyId" + Id, 1);
		}
	}

	public int EncounterCount()
	{
		if (PlayerPrefs.HasKey("EnemyId" + Id))
		{
			int encounterCount = PlayerPrefs.GetInt("EnemyId" + Id);
			return encounterCount;
		}
		return 0;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		UpdatePhase();
	}

	public bool IsDead()
	{
		return health <= 0;
	}

	public bool IsActFinished()
	{
		if (maxDepressionPhase == _depressionPhase)
		{
			return true;
		}
		return false;
	}

	private void UpdatePhase()
	{
		if (health > 0)
		{
			// OBTENER DIALOGO CORRESPONDIENTE SEGUN LA DISTRIBUCION
			for (var i = _phase; i < maxPhases -1; i++)
			{
				if (health <= maxHealth * phaseDistribution[i + 1] && health > maxHealth * phaseDistribution[i + 2])
				{
					_phase += 1;
					break;
				}
			}
		}
		else
		{
			_phase = maxPhases;
		}
	}
}
