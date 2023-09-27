using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatCanvas : MonoBehaviour
{
    [SerializeField] private Image enemySpriteHolder;
    [SerializeField] private Image playerSpriteHolder;
    [SerializeField] private List<GameObject> mainButtons;
    [SerializeField] private List<GameObject> abilityButtons;
    [SerializeField] private List<GameObject> actButtons;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject abilitiesMenu;
    [SerializeField] private GameObject actMenu;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private List<TextMeshProUGUI> abilitiesButtonsTexts;
    [SerializeField] private List<TextMeshProUGUI> actButtonsTexts;
    [HideInInspector] public bool enemyStartBlink;
    [HideInInspector] public bool playerStartBlink;
    private readonly float _spriteBlinkingTotalDuration = 3.0f;
    private float _blinkingTimer = 0.0f;

    private bool _isMainMenuCoroutineActive;
    private bool _isActMenuCoroutineActive;
    private bool _isAbilityMenuCoroutineActive;
    private bool _isDialogueBoxCoroutineActive;
    
    #region Properties
    
    public Image EnemySpriteHolder
    {
        get => enemySpriteHolder;
        set => enemySpriteHolder = value;
    }

    public Image PlayerSpriteHolder
    {
        get => playerSpriteHolder;
        set => playerSpriteHolder = value;
    }

    public List<GameObject> MainButtons
    {
        get => mainButtons;
        set => mainButtons = value;
    }

    public List<GameObject> AbilityButtons
    {
        get => abilityButtons;
        set => abilityButtons = value;
    }

    public List<GameObject> ActButtons
    {
        get => actButtons;
        set => actButtons = value;
    }
    
    #endregion

    private void Update()
    {
        if (enemyStartBlink)
        {
            SpriteBlink(EnemySpriteHolder);
        }
        else if (playerStartBlink)
        {
            SpriteBlink(PlayerSpriteHolder);
        }
    }

    public IEnumerator ShowMainMenu()
    {
        _isMainMenuCoroutineActive = true;
        yield return new WaitForSeconds(0f);
        var encounterCount = GameManager.Instance.CurrentEnemy.EncounterCount();
        if (encounterCount == 0)
        {
            mainButtons[2].SetActive(false);
        }

        abilitiesMenu.SetActive(false);
        actMenu.SetActive(false);
        mainMenu.SetActive(true);
        dialogBox.SetActive(false);
        _isMainMenuCoroutineActive = false;
    }

    public IEnumerator ShowAbilitiesMenu(List<AttackAbility> abilities)
    {
        _isAbilityMenuCoroutineActive = true;
        yield return new WaitForSeconds(0f);
        for (var i = 0; i < abilities.Count; i++)
        {
            abilitiesButtonsTexts[i].text = abilities[i].Name;
        }
        mainMenu.SetActive(false);
        abilitiesMenu.SetActive(true);
        actMenu.SetActive(false);
        dialogBox.SetActive(false);
        _isAbilityMenuCoroutineActive = false;
    }
	
    public IEnumerator ShowActMenu(List<ActOption> actOptions)
    {
        _isActMenuCoroutineActive = true;
        yield return new WaitForSeconds(0f);
        for (var i = 0; i < actOptions.Count; i++)
        {
            actButtonsTexts[i].text = actOptions[i].Name;
        }
        mainMenu.SetActive(false);
        actMenu.SetActive(true);
        abilitiesMenu.SetActive(false);
        dialogBox.SetActive(false);
        _isActMenuCoroutineActive = false;
    }
	
    public IEnumerator ShowDialogBox()
    {
        _isDialogueBoxCoroutineActive = true;
        yield return new WaitForSeconds(0f);
        mainMenu.SetActive(false);
        abilitiesMenu.SetActive(false);
        actMenu.SetActive(false);
        dialogBox.SetActive(true);
        _isDialogueBoxCoroutineActive = false;
    }

    public void StopCoroutines(List<ActOption> actOptions, List<AttackAbility> abilities)
    {
        if (_isAbilityMenuCoroutineActive)
        {
            StopCoroutine(ShowAbilitiesMenu(abilities));
        }
        else if (_isActMenuCoroutineActive)
        {
            StopCoroutine(ShowActMenu(actOptions));
        }
        else if (_isDialogueBoxCoroutineActive)
        {
            StopCoroutine(ShowDialogBox());
        }
        else if (_isMainMenuCoroutineActive)
        {
            StopCoroutine(ShowMainMenu());
        }
    }

    public void StopMainCoroutines()
    {
        if (_isDialogueBoxCoroutineActive)
        {
            StopCoroutine(ShowDialogBox());
        }
        else if (_isMainMenuCoroutineActive)
        {
            StopCoroutine(ShowMainMenu());
        }
    }

    private void SpriteBlink(Image spriteHolder)
    {
        _blinkingTimer += Time.deltaTime;
        if (_blinkingTimer >= _spriteBlinkingTotalDuration)
        {
            if (enemyStartBlink)
            {
                enemyStartBlink = false;
            }
            else if (playerStartBlink)
            {
                playerStartBlink = false;
            }
            
            spriteHolder.enabled = true;
            _blinkingTimer = 0.0f;
            return;
        }

        if (Time.deltaTime <= 0.5f)
        {
            if (spriteHolder.enabled == false)
            {
                spriteHolder.enabled = true;
            }
            else
            {
                spriteHolder.enabled = false;
            }
        }
    }
}
