using UnityEngine;

public class Itemizer : MonoBehaviour
{
    [SerializeField] private Item item;
    
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.Icon;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var target = GameManager.Instance.Player.Backpack.Items.Find(x => x.Id == item.Id);

            if (target != null)
            {
                if (item.Quantity < 99)
                {
                    item.Quantity += 1;
                    EventManager.OnItemTaken?.Invoke(item);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventario lleno");
                }
            }
            else
            {
                if (GameManager.Instance.Player.Backpack.Items.Count + 1 <=
                    GameManager.Instance.Player.Backpack.NumSlots)
                {
                    item.Quantity += 1;
                    GameManager.Instance.Player.Backpack.AddItem(item);
                    EventManager.OnItemTaken?.Invoke(item);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventario lleno");
                }
            }
        }
    }
}
