using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDisappear : MonoBehaviour
{
    [SerializeField] private Condition condition;

    private void Update()
    {
        if (condition.EvaluateCondition())
        {
            Destroy(gameObject);
        }
    }
}
