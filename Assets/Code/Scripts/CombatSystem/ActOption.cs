using System.Collections.Generic;
using UnityEngine;

public enum Symptom
{
    SentimientosInfelicidadMiserabilidad,
    Apetito,
    Culpabilidad,
    Indecision,
    LlantoIncontrolable,
    FrecuenciaComunicacionVerbalDisminuida,
    FuturoFatalista,
    NoValeLaPenaVivir,
    ActividadSocialDisminuida,
    PensamientoMuerte,
    PensamientoSuicida,
    PersonasEnGeneralMalas,
    AutoImagenComprometida,
    SentimientosSoledad,
    NoSeSienteQuerido,
    GeneralmenteNoSoyBueno,
    TodoSaleMal,
    None
}

[CreateAssetMenu(fileName = "New Act", menuName = "Act Option")]
public class ActOption : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private List<ActOption> nextActOptions;
    [SerializeField] private Dialogue response;
    
    // Estos atributos son para introducirlos en la base de datos
    [SerializeField] private int depressionPoints;
    [SerializeField] private Symptom associatedSymptom;

    #region Properties
    
    public string Name
    {
        get => name;
        set => name = value;
    }
    
    public List<ActOption> NextActOptions
    {
        get => nextActOptions;
        set => nextActOptions = value;
    }

    public Dialogue Response
    {
        get => response;
        set => response = value;
    }

    public int DepressionPoints
    {
        get => depressionPoints;
        set => depressionPoints = value;
    }

    public Symptom AssociatedSymptom
    {
        get => associatedSymptom;
        set => associatedSymptom = value;
    }

    #endregion
    
}
