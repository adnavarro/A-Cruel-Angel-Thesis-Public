using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private PlayerCombat playerCombat;
	[SerializeField] private Backpack backpack;
	private PlayerMovement _playerMovement;
	
	#region Properties
	
	public PlayerMovement PlayerMovement
	{
		get => _playerMovement;
		set => _playerMovement = value;
	}

	public PlayerCombat PlayerCombat
	{
		get => playerCombat;
		set => playerCombat = value;
	}

	public Backpack Backpack
	{
		get => backpack;
		set => backpack = value;
	}

	#endregion

	private void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
	}

	public void LoadPlayerPosition()
	{
		if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
		{
			transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), 
				PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
		}
	}

	public void SavePlayerPosition()
	{
		var position = transform.position;
		PlayerPrefs.SetFloat("PlayerX", position.x);
		PlayerPrefs.SetFloat("PlayerY", position.y);
		PlayerPrefs.SetFloat("PlayerZ", position.z);
	}

	public void ResetPlayerData()
	{
		playerCombat.Health = 100;
		playerCombat.MaxHealth = 100;
		backpack.NumSlots = 10;
		backpack.Items.Clear();
		backpack.Quests.Clear();
	}
}