using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RandomNPCMovement : MonoBehaviour, IMovement
{
    public float moveSpeed;
    private Rigidbody2D _rigidbody2D;
    public bool isWalking;
    [FormerlySerializedAs("walkCOunter")] public float walkCounter;
    public float waitTime;
    private float _waitCounter;
    private bool _active;

    private int _walkDirection;
    
    void Start()
    {
        _active = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _waitCounter = waitTime;
        _waitCounter = waitTime;
        
        ChooseDirection();
    }

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     walkCOunter = 0;
    //     isWalking = false;
    // }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
            _active = !_active;
    }
    
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
            _active = !_active;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    public void CalculateMovement()
    {
        if (_active)
        {
            if (isWalking)
            {
                walkCounter -= Time.deltaTime;


                switch (_walkDirection)
                {
                    case 0:
                        _rigidbody2D.velocity = new Vector2(0, moveSpeed);
                        break;
                    case 1:
                        _rigidbody2D.velocity = new Vector2(moveSpeed, 0);
                        break;
                    case 2:
                        _rigidbody2D.velocity = new Vector2(0, -moveSpeed);
                        break;
                    case 3:
                        _rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
                        break;
                }
            
                if (walkCounter < 0)
                {
                    isWalking = false;
                    _waitCounter = waitTime;
                }
            }
            else
            {
                _waitCounter -= Time.deltaTime;
            
                _rigidbody2D.velocity = Vector2.zero;
            
                if (_waitCounter < 0)
                {
                    ChooseDirection();
                }
            }
        }
    }
    public void ChooseDirection()
    {
        _walkDirection = Random.Range(0, 4);
        isWalking = true;

        walkCounter = waitTime;
    }
}
