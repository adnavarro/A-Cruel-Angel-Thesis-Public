using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public enum BattleState
{
	Start,
	PlayerMain,
	PlayerAbilities,
	PlayerAct,
	EnemyTurn,
	Won,
	Lost,
	Leave
}
public class CombatManager : MonoBehaviour
{
	private List<AttackAbility> _attackAbilities;
	private List<ActOption> _actOptions;
	private Enemy _enemy;
	private Dialogue _currentActDialogue;
	private List<Dialogue> _currentDamageDialogues;
	private int _currentDepressionPhase;
	private PlayerCombat _player;
	private BattleState _battleState;
	private CombatDialogueManager _combatDialogueManager;
	private int _currentPhase;
	private CombatCanvas _combatCanvas;
	private bool _isFirstCycle;

	private void Start()
	{
		GameManager.Instance.IsInteractActive = true;
		_isFirstCycle = true;
		InitializeCombatCanvas();
		InitializeDialogueManager();
		InitializeEnemy();
		InitializePlayer();
		StartCoroutine(BattleCycle());
	}

	private void OnDestroy()
	{
		StopCoroutine(BattleCycle());
	}

	#region InitializeMethods
	private void InitializeDialogueManager()
	{
		try
		{
			_combatDialogueManager = FindObjectOfType<CombatDialogueManager>();
		}
		catch (Exception e)
		{
			Debug.Log("Exception: " + e);
		}
	}
	
	private void InitializeCombatCanvas()
	{
		try
		{
			_combatCanvas = FindObjectOfType<CombatCanvas>();
		}
		catch (Exception e)
		{
			Debug.Log("Exception: " + e);
		}
	}

	private void InitializeEnemy()
	{
		try
		{
			_enemy = GameManager.Instance.CurrentEnemy;
		}
		catch (Exception e)
		{
			Debug.Log("Exception: " + e);
		}

		_currentDepressionPhase = 0;
		_enemy.Phase = 0;
		_enemy.DepressionPhase = 0;
		_enemy.Health = _enemy.MaxHealth;
		_combatCanvas.EnemySpriteHolder.sprite = _enemy.sprite;
		_actOptions = _enemy.actOptions;
		_attackAbilities = _enemy.attackAbilities;
	}
	

	private void InitializePlayer()
	{
		try
		{
			_player = GameManager.Instance.PlayerCombat;
		}
		catch (Exception e)
		{
			Debug.Log("Exception: " + e);
		}
		_combatCanvas.PlayerSpriteHolder.sprite = _player.combatSprite;
	}
	#endregion
	
	private IEnumerator BattleCycle()
	{
		GameManager.Instance.IsMenuActive = true;
		SetBattleState(BattleState.Start);
		EventManager.OnHealthChanged?.Invoke();
		
		#region TurnCycle
		
		while (!((_battleState == BattleState.Won) || (_battleState == BattleState.Leave) ||
		       (_battleState == BattleState.Lost)))
		{
			if (_battleState == BattleState.Start)
			{
				StartCoroutine(_combatCanvas.ShowDialogBox());
				yield return null;
				#region EnemyPresentation
				
				_combatDialogueManager.StartDialogue(_enemy.dialoguePresentation[Random.Range(0,_enemy.dialoguePresentation.Count)], _enemy.font);
				while (_combatDialogueManager.DialogueIsActive)
				{
					if (GameManager.Instance.OnInteract("ui")) _combatDialogueManager.DisplayNextSentence(_enemy.font);
					yield return null;
				}
				
				#endregion
				SetBattleState(BattleState.PlayerMain);
			}
			
			if (_battleState == BattleState.PlayerMain || _battleState == BattleState.PlayerAbilities || _battleState == BattleState.PlayerAct)
			{
				_combatCanvas.StopMainCoroutines();
				StartCoroutine(_combatCanvas.ShowMainMenu());
				yield return null;
				#region PlayerTurn
				
				_combatDialogueManager.DisplaySentence("¿Qué debería hacer?");
				EventSystem.current.SetSelectedGameObject(_combatCanvas.MainButtons[0]);
				
				// PLAYER ESCOGE SU ACCION POR DEFECTO
				while (_battleState == BattleState.PlayerMain)
				{
					SelectAction();
					yield return null;
				}
		        
				// PLAYER ESCOGE UNA HABILIDAD
				EventSystem.current.SetSelectedGameObject(_combatCanvas.AbilityButtons[0]);
				while (_battleState == BattleState.PlayerAbilities)
				{
					if (GameManager.Instance.OnInteract("ui"))
						SelectAbility();
					yield return null;
				}
				
				//PLAYER ESCOGE UN ACT
				EventSystem.current.SetSelectedGameObject(_combatCanvas.ActButtons[0]);
				while (_battleState == BattleState.PlayerAct)
				{
					if (GameManager.Instance.OnInteract("ui"))
						SelectAct();
					yield return null;
				}
				
				#endregion
			}
			
			if (_battleState == BattleState.EnemyTurn)
			{
				#region EnemyTurn
				
				_combatCanvas.StopMainCoroutines();
				StartCoroutine(_combatCanvas.ShowDialogBox());
				yield return null;
				
				//REACCIONA AL ACT ANTERIOR
				if (_currentDepressionPhase < _enemy.DepressionPhase)
				{
					_combatDialogueManager.StartDialogue(_currentActDialogue, _enemy.font);
					while (_combatDialogueManager.DialogueIsActive)
					{
						if (GameManager.Instance.OnInteract("ui")) _combatDialogueManager.DisplayNextSentence(_enemy.font);
						yield return null;
					}
					_currentDepressionPhase = _enemy.DepressionPhase;
				}

				// REACCIONA AL ATAQUE ANTERIOR
				else if (_enemy.Phase == _currentPhase)
				{
					_combatDialogueManager.StartDialogue(_currentDamageDialogues[Random.Range(0,_currentDamageDialogues.Count)], _enemy.font);
					while (_combatDialogueManager.DialogueIsActive)
					{
						if (GameManager.Instance.OnInteract("ui")) _combatDialogueManager.DisplayNextSentence(_enemy.font);
						yield return null;
					}
				}
				else if (_enemy.Phase >= _currentPhase)
				{
					var currentPhaseDialogues = _enemy.dialoguePhase.FindAll(x => x.Phase == _enemy.Phase);
					_combatDialogueManager.StartDialogue(currentPhaseDialogues[Random.Range(0,currentPhaseDialogues.Count)], _enemy.font);
					while (_combatDialogueManager.DialogueIsActive)
					{
						if (GameManager.Instance.OnInteract("ui")) _combatDialogueManager.DisplayNextSentence(_enemy.font);
						yield return null;
					}

					_currentPhase += _enemy.Phase;
				}
				
				// ACCION DEL ENEMIGO
				var dialogueCommentary = _enemy.dialogueCommentary;
				_combatDialogueManager.StartDialogue(dialogueCommentary[Random.Range(0,dialogueCommentary.Count)], null);
				while (_combatDialogueManager.DialogueIsActive)
				{
					if (GameManager.Instance.OnInteract("ui")) _combatDialogueManager.DisplayNextSentence(null);
					yield return null;
				}
				
				// PLAYER RECIBE DAÑO
				PlayerTakeDamage();
				
				#endregion
			}
			yield return null;
		}
		
		#endregion

		#region EndOfBattle
		
		if (_battleState == BattleState.Won)
		{
			if (_enemy.IsActFinished())
			{ 
				_combatCanvas.StopMainCoroutines();
				StartCoroutine(_combatCanvas.ShowDialogBox());
				_combatDialogueManager.StartDialogue(_currentActDialogue, _enemy.font);
				while (_combatDialogueManager.DialogueIsActive)
				{
					if (GameManager.Instance.OnInteract("ui")) _combatDialogueManager.DisplayNextSentence(_enemy.font);
					yield return null;
				}
			}
			else if (_enemy.IsDead())
			{
				var currentPhaseDialogues = _enemy.dialoguePhase.FindAll(x => x.Phase == _enemy.Phase);
				_combatCanvas.StopMainCoroutines();
				StartCoroutine(_combatCanvas.ShowDialogBox());
				_combatDialogueManager.StartDialogue(currentPhaseDialogues[Random.Range(0,currentPhaseDialogues.Count)], _enemy.font);
				while (_combatDialogueManager.DialogueIsActive)
				{
					if (GameManager.Instance.OnInteract("ui")) _combatDialogueManager.DisplayNextSentence(_enemy.font);
					yield return null;
				}
			}
			GameManager.Instance.LeaveBattleScene();
			yield return new WaitForSeconds(0f);
		}
		else if (_battleState == BattleState.Lost)
		{
			EventManager.OnSceneChange("GameOverScene");
			yield return new WaitForSeconds(0f);
		}
		
		#endregion
	}

	private void SelectAction()
	{
		if (GameManager.Instance.OnInteract("ui"))
			foreach (var target in _combatCanvas.MainButtons)
			{
				if (target == EventSystem.current.currentSelectedGameObject)
				{
					if (target.name == "AttackButton")
					{
						if (_enemy.EncounterCount() == 0 && _isFirstCycle)
						{
							_isFirstCycle = false;
						}
						GameManager.Instance.AudioManager.Play("Ui effect");
						SetBattleState(BattleState.PlayerAbilities);
						OnAttackButton();
						break;
					}

					if (target.name == "ActButton")
					{
						if (_isFirstCycle)
						{
							_isFirstCycle = false;
						}
						GameManager.Instance.AudioManager.Play("Ui effect");
						SetBattleState(BattleState.PlayerAct);
						OnActButton();
						break;
					}

					if (target.name == "FleeButton")
					{
						_isFirstCycle = false;
						GameManager.Instance.AudioManager.Play("Ui effect");
						GameManager.Instance.LeaveBattleScene();
					}
				}
			}
	}
	
	private void SelectAbility()
	{
		if (!GameManager.Instance.OnInteract("ui")) return;
		foreach (var target in _combatCanvas.AbilityButtons)
		{
			if (target == EventSystem.current.currentSelectedGameObject)
			{
				GameManager.Instance.AudioManager.Play("Ui effect");
				if (target.name == "GoBackButton")
				{
					OnReturnButton();
					break;
				}

				try
				{
					var ability = _attackAbilities.Find(x =>
						x.Name == target.GetComponentInChildren<TextMeshProUGUI>().text);
					PlayerAttack(ability);
				}
				catch (Exception e)
				{
					Debug.Log(e + " Component in children not found");
				}
			}
		}
	}

	private void SelectAct()
	{
		if (!GameManager.Instance.OnInteract("ui")) return;
		foreach (var target in _combatCanvas.ActButtons)
		{
			if (target != EventSystem.current.currentSelectedGameObject) continue;
			GameManager.Instance.AudioManager.Play("Ui effect");
			if (target.name == "GoBackButton")
			{
				OnReturnButton();
				break;
			}

			try
			{
				var act = _actOptions.Find(x => x.Name == target.GetComponentInChildren<TextMeshProUGUI>().text);
				PlayerAct(act);
			}
			catch (Exception e)
			{
				Debug.Log(e + " Component in children not found");
			}
		}
	}

	#region Actions
	private void PlayerAttack(AttackAbility ability)
	{
		_enemy.TakeDamage(ability.BaseAttack);
		_combatCanvas.enemyStartBlink = true;
		_currentDamageDialogues = ability.Responses;
		EventManager.OnHealthChanged?.Invoke();
		SetBattleState(_enemy.IsDead() ? BattleState.Won : BattleState.EnemyTurn);
	}

	private void PlayerAct(ActOption act)
	{
		_enemy.DepressionPhase += 1;
		_currentActDialogue = act.Response;
		GetCurrentActOptions(act);
		SetBattleState(_enemy.IsActFinished() ? BattleState.Won : BattleState.EnemyTurn);
	}

	private void PlayerTakeDamage()
	{
		_player.Health -= Random.Range(0, _enemy.BaseAttack);
		_combatCanvas.playerStartBlink = true;
		EventManager.OnHealthChanged?.Invoke();
		SetBattleState(_player.Health <= 0 ? BattleState.Lost : BattleState.PlayerMain);
	}

	#endregion

	#region Buttons
	private void OnAttackButton()
	{
		_combatCanvas.StopCoroutines(_actOptions, _attackAbilities);
		StartCoroutine(_combatCanvas.ShowAbilitiesMenu(_attackAbilities));
	}

	private void OnActButton()
	{
		_combatCanvas.StopCoroutines(_actOptions, _attackAbilities);
		StartCoroutine(_combatCanvas.ShowActMenu(_actOptions));
	}

	private void OnReturnButton()
	{
		_combatCanvas.StopCoroutines(_actOptions, _attackAbilities);
		_battleState = BattleState.PlayerMain;
		StartCoroutine(_combatCanvas.ShowMainMenu());
	}
	#endregion
	
	#region BasicMethods
	private void SetBattleState(BattleState newBattleState)
	{
		_battleState = newBattleState;
	}

	private void GetCurrentActOptions(ActOption act)
	{
		if (_enemy.DepressionPhase <= _enemy.MaxDepressionPhase)
		{
			_actOptions = act.NextActOptions;
		}
	}

	#endregion
}