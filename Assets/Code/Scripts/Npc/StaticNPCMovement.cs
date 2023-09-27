using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StaticNPCMovement : MonoBehaviour, IMovement
{
    [FormerlySerializedAs("PositionList")] public List<Vector2> positionList;
    public float moveSpeed;
    private bool _active;
    private int _currentPosition;

    void Start()
    {
        _active = true;
        _currentPosition = 0;
    }


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
            if((Vector2)this.transform.position != positionList[_currentPosition])
                transform.position = Vector2.MoveTowards(transform.position, positionList[_currentPosition], moveSpeed * Time.deltaTime);
            else
            {
                if(_currentPosition + 1 <= positionList.Count - 1)
                    _currentPosition ++;
                else
                {
                    _active = false;
                }
            }
        }
        
    }
}
