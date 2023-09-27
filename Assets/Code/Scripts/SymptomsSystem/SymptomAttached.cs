using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SymptomAttached", menuName = "SymptomAttached")]
public class SymptomAttached : ScriptableObject
{
    [SerializeField] private Symptom associatedSymptom;
    [SerializeField] private int maxDepressionPoints;

    public Symptom AssociatedSymptom
    {
        get => associatedSymptom;
        set => associatedSymptom = value;
    }

    public int MaxDepressionPoints
    {
        get => maxDepressionPoints;
        set => maxDepressionPoints = value;
    }
}
