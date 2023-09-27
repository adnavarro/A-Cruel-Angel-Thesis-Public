using System;
using MongoDB.Bson;

[System.Serializable]
public class SymptomsCountsData
{
    private ObjectId _userId;
    private string _name;
    private string _lastName;
    private string _username;
    private string _password;
    private long _sessionId;
    private DateTime _dateCreated;
    
    private int _sentimientosInfelicidadMiserabilidadCount;
    private int _apetitoCount;
    private int _culpabilidadCount;
    private int _indecisionCount;
    private int _llantoIncontrolableCount;
    private int _frecuenciaComunicacionVerbalDisminuidaCount;
    private int _futuroFatalistaCount;
    private int _noValeLaPenaVivirCount;
    private int _actividadSocialDisminuidaCount;
    private int _pensamientoMuerteCount;
    private int _pensamientoSuicidaCount;
    private int _personasEnGeneralMalasCount;
    private int _autoImagenComprometidaCount;
    private int _sentimientosSoledadCount;
    private int _noSeSienteQueridoCount;
    private int _generalmenteNoSoyBuenoCount;
    private int _todoSaleMalCount;

    private int _maxSentimientosInfelicidadMiserabilidadCount;
    private int _maxApetitoCount;
    private int _maxCulpabilidadCount;
    private int _maxIndecisionCount;
    private int _maxLlantoIncontrolableCount;
    private int _maxFrecuenciaComunicacionVerbalDisminuidaCount;
    private int _maxFuturoFatalistaCount;
    private int _maxNoValeLaPenaVivirCount;
    private int _maxActividadSocialDisminuidaCount;
    private int _maxPensamientoMuerteCount;
    private int _maxPensamientoSuicidaCount;
    private int _maxPersonasEnGeneralMalasCount;
    private int _maxAutoImagenComprometidaCount;
    private int _maxSentimientosSoledadCount;
    private int _maxNoSeSienteQueridoCount;
    private int _maxGeneralmenteNoSoyBuenoCount;
    private int _maxTodoSaleMalCount;
    private DateTime _lastAccess;
    
    private int _npcTalkedBeforeCount;

    #region Properties

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }

    public string Username
    {
        get => _username;
        set => _username = value;
    }

    public long SessionId
    {
        get => _sessionId;
        set => _sessionId = value;
    }

    public DateTime DateCreated
    {
        get => _dateCreated;
        set => _dateCreated = value;
    }

    public ObjectId UserId
    {
        get => _userId;
        set => _userId = value;
    }

    public string Password
    {
        get => _password;
        set => _password = value;
    }

    public int SentimientosInfelicidadMiserabilidadCount
    {
        get => _sentimientosInfelicidadMiserabilidadCount;
        set => _sentimientosInfelicidadMiserabilidadCount = value;
    }

    public int ApetitoCount
    {
        get => _apetitoCount;
        set => _apetitoCount = value;
    }

    public int CulpabilidadCount
    {
        get => _culpabilidadCount;
        set => _culpabilidadCount = value;
    }

    public int IndecisionCount
    {
        get => _indecisionCount;
        set => _indecisionCount = value;
    }

    public int LlantoIncontrolableCount
    {
        get => _llantoIncontrolableCount;
        set => _llantoIncontrolableCount = value;
    }

    public int FrecuenciaComunicacionVerbalDisminuidaCount
    {
        get => _frecuenciaComunicacionVerbalDisminuidaCount;
        set => _frecuenciaComunicacionVerbalDisminuidaCount = value;
    }

    public int FuturoFatalistaCount
    {
        get => _futuroFatalistaCount;
        set => _futuroFatalistaCount = value;
    }

    public int NoValeLaPenaVivirCount
    {
        get => _noValeLaPenaVivirCount;
        set => _noValeLaPenaVivirCount = value;
    }

    public int ActividadSocialDisminuidaCount
    {
        get => _actividadSocialDisminuidaCount;
        set => _actividadSocialDisminuidaCount = value;
    }

    public int PensamientoMuerteCount
    {
        get => _pensamientoMuerteCount;
        set => _pensamientoMuerteCount = value;
    }

    public int PensamientoSuicidaCount
    {
        get => _pensamientoSuicidaCount;
        set => _pensamientoSuicidaCount = value;
    }

    public int PersonasEnGeneralMalasCount
    {
        get => _personasEnGeneralMalasCount;
        set => _personasEnGeneralMalasCount = value;
    }

    public int AutoImagenComprometidaCount
    {
        get => _autoImagenComprometidaCount;
        set => _autoImagenComprometidaCount = value;
    }

    public int SentimientosSoledadCount
    {
        get => _sentimientosSoledadCount;
        set => _sentimientosSoledadCount = value;
    }

    public int NoSeSienteQueridoCount
    {
        get => _noSeSienteQueridoCount;
        set => _noSeSienteQueridoCount = value;
    }

    public int GeneralmenteNoSoyBuenoCount
    {
        get => _generalmenteNoSoyBuenoCount;
        set => _generalmenteNoSoyBuenoCount = value;
    }

    public int TodoSaleMalCount
    {
        get => _todoSaleMalCount;
        set => _todoSaleMalCount = value;
    }

    public int MaxSentimientosInfelicidadMiserabilidadCount
    {
        get => _maxSentimientosInfelicidadMiserabilidadCount;
        set => _maxSentimientosInfelicidadMiserabilidadCount = value;
    }

    public int MaxApetitoCount
    {
        get => _maxApetitoCount;
        set => _maxApetitoCount = value;
    }

    public int MaxCulpabilidadCount
    {
        get => _maxCulpabilidadCount;
        set => _maxCulpabilidadCount = value;
    }

    public int MaxIndecisionCount
    {
        get => _maxIndecisionCount;
        set => _maxIndecisionCount = value;
    }

    public int MaxLlantoIncontrolableCount
    {
        get => _maxLlantoIncontrolableCount;
        set => _maxLlantoIncontrolableCount = value;
    }

    public int MaxFrecuenciaComunicacionVerbalDisminuidaCount
    {
        get => _maxFrecuenciaComunicacionVerbalDisminuidaCount;
        set => _maxFrecuenciaComunicacionVerbalDisminuidaCount = value;
    }

    public int MaxFuturoFatalistaCount
    {
        get => _maxFuturoFatalistaCount;
        set => _maxFuturoFatalistaCount = value;
    }

    public int MaxNoValeLaPenaVivirCount
    {
        get => _maxNoValeLaPenaVivirCount;
        set => _maxNoValeLaPenaVivirCount = value;
    }

    public int MaxActividadSocialDisminuidaCount
    {
        get => _maxActividadSocialDisminuidaCount;
        set => _maxActividadSocialDisminuidaCount = value;
    }

    public int MaxPensamientoMuerteCount
    {
        get => _maxPensamientoMuerteCount;
        set => _maxPensamientoMuerteCount = value;
    }

    public int MaxPensamientoSuicidaCount
    {
        get => _maxPensamientoSuicidaCount;
        set => _maxPensamientoSuicidaCount = value;
    }

    public int MaxPersonasEnGeneralMalasCount
    {
        get => _maxPersonasEnGeneralMalasCount;
        set => _maxPersonasEnGeneralMalasCount = value;
    }

    public int MaxAutoImagenComprometidaCount
    {
        get => _maxAutoImagenComprometidaCount;
        set => _maxAutoImagenComprometidaCount = value;
    }

    public int MaxSentimientosSoledadCount
    {
        get => _maxSentimientosSoledadCount;
        set => _maxSentimientosSoledadCount = value;
    }

    public int MaxNoSeSienteQueridoCount
    {
        get => _maxNoSeSienteQueridoCount;
        set => _maxNoSeSienteQueridoCount = value;
    }

    public int MaxGeneralmenteNoSoyBuenoCount
    {
        get => _maxGeneralmenteNoSoyBuenoCount;
        set => _maxGeneralmenteNoSoyBuenoCount = value;
    }

    public int MaxTodoSaleMalCount
    {
        get => _maxTodoSaleMalCount;
        set => _maxTodoSaleMalCount = value;
    }

    public int NpcTalkedBeforeCount
    {
        get => _npcTalkedBeforeCount;
        set => _npcTalkedBeforeCount = value;
    }
    
    public DateTime LastAccess
    {
        get => _lastAccess;
        set => _lastAccess = value;
    }

    #endregion

    public SymptomsCountsData(SymptomsCounts symptomsCounts)
    {
        Name = symptomsCounts.name;
        LastName = symptomsCounts.lastName;
        Username = symptomsCounts.username;
        SessionId = symptomsCounts.sessionId;
        UserId = symptomsCounts.userId;
        Password = symptomsCounts.password;
        DateCreated = DateTime.Now;
        
        SentimientosInfelicidadMiserabilidadCount = symptomsCounts.sentimientosInfelicidadMiserabilidadCount;
        ApetitoCount = symptomsCounts.apetitoCount;
        CulpabilidadCount = symptomsCounts.culpabilidadCount;
        IndecisionCount = symptomsCounts.indecisionCount;
        LlantoIncontrolableCount = symptomsCounts.llantoIncontrolableCount;
        FrecuenciaComunicacionVerbalDisminuidaCount = symptomsCounts.frecuenciaComunicacionVerbalDisminuidaCount;
        FuturoFatalistaCount = symptomsCounts.futuroFatalistaCount;
        NoValeLaPenaVivirCount = symptomsCounts.noValeLaPenaVivirCount;
        ActividadSocialDisminuidaCount = symptomsCounts.actividadSocialDisminuidaCount;
        PensamientoMuerteCount = symptomsCounts.pensamientoMuerteCount;
        PensamientoSuicidaCount = symptomsCounts.pensamientoSuicidaCount;
        PersonasEnGeneralMalasCount = symptomsCounts.personasEnGeneralMalasCount;
        AutoImagenComprometidaCount = symptomsCounts.autoImagenComprometidaCount;
        SentimientosSoledadCount = symptomsCounts.sentimientosSoledadCount;
        NoSeSienteQueridoCount = symptomsCounts.noSeSienteQueridoCount;
        GeneralmenteNoSoyBuenoCount = symptomsCounts.generalmenteNoSoyBuenoCount;
        TodoSaleMalCount = symptomsCounts.todoSaleMalCount;
        
        MaxSentimientosInfelicidadMiserabilidadCount = symptomsCounts.maxSentimientosInfelicidadMiserabilidadCount;
        MaxApetitoCount = symptomsCounts.maxApetitoCount;
        MaxCulpabilidadCount = symptomsCounts.maxCulpabilidadCount;
        MaxIndecisionCount = symptomsCounts.maxIndecisionCount;
        MaxLlantoIncontrolableCount = symptomsCounts.maxLlantoIncontrolableCount;
        MaxFrecuenciaComunicacionVerbalDisminuidaCount = symptomsCounts.maxFrecuenciaComunicacionVerbalDisminuidaCount;
        MaxFuturoFatalistaCount = symptomsCounts.maxFuturoFatalistaCount;
        MaxNoValeLaPenaVivirCount = symptomsCounts.maxNoValeLaPenaVivirCount;
        MaxActividadSocialDisminuidaCount = symptomsCounts.maxActividadSocialDisminuidaCount;
        MaxPensamientoMuerteCount = symptomsCounts.maxPensamientoMuerteCount;
        MaxPensamientoSuicidaCount = symptomsCounts.maxPensamientoSuicidaCount;
        MaxPersonasEnGeneralMalasCount = symptomsCounts.maxPersonasEnGeneralMalasCount;
        MaxAutoImagenComprometidaCount = symptomsCounts.maxAutoImagenComprometidaCount;
        MaxSentimientosSoledadCount = symptomsCounts.maxSentimientosSoledadCount;
        MaxNoSeSienteQueridoCount = symptomsCounts.maxNoSeSienteQueridoCount;
        MaxGeneralmenteNoSoyBuenoCount = symptomsCounts.maxGeneralmenteNoSoyBuenoCount;
        MaxTodoSaleMalCount = symptomsCounts.maxTodoSaleMalCount;

        NpcTalkedBeforeCount = symptomsCounts.npcTalkedBeforeCount;
        
        LastAccess = DateTime.UtcNow.AddHours(-4);;
    }
}
