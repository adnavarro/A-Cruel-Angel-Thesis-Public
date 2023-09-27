using System;
using MongoDB.Bson;
using UnityEngine;

[CreateAssetMenu(fileName = "New SymptomsCounts", menuName = "SymptomsCounts")]
public class SymptomsCounts : ScriptableObject
{
    public ObjectId userId;
    public new string name;
    public string lastName;
    public string username;
    public string password;
    public long sessionId;
    public DateTime dateCreated;

    public int sentimientosInfelicidadMiserabilidadCount;
    public int apetitoCount;
    public int culpabilidadCount;
    public int indecisionCount;
    public int llantoIncontrolableCount;
    public int frecuenciaComunicacionVerbalDisminuidaCount;
    public int futuroFatalistaCount;
    public int noValeLaPenaVivirCount;
    public int actividadSocialDisminuidaCount;
    public int pensamientoMuerteCount;
    public int pensamientoSuicidaCount;
    public int personasEnGeneralMalasCount;
    public int autoImagenComprometidaCount;
    public int sentimientosSoledadCount;
    public int noSeSienteQueridoCount;
    public int generalmenteNoSoyBuenoCount;
    public int todoSaleMalCount;
    
    public int maxSentimientosInfelicidadMiserabilidadCount;
    public int maxApetitoCount;
    public int maxCulpabilidadCount;
    public int maxIndecisionCount;
    public int maxLlantoIncontrolableCount;
    public int maxFrecuenciaComunicacionVerbalDisminuidaCount;
    public int maxFuturoFatalistaCount;
    public int maxNoValeLaPenaVivirCount;
    public int maxActividadSocialDisminuidaCount;
    public int maxPensamientoMuerteCount;
    public int maxPensamientoSuicidaCount;
    public int maxPersonasEnGeneralMalasCount;
    public int maxAutoImagenComprometidaCount;
    public int maxSentimientosSoledadCount;
    public int maxNoSeSienteQueridoCount;
    public int maxGeneralmenteNoSoyBuenoCount;
    public int maxTodoSaleMalCount;

    public int npcTalkedBeforeCount;

    public DateTime lastAccess;
    


    public void ResetSymptoms()
    {
        sentimientosInfelicidadMiserabilidadCount = 0;
        apetitoCount = 0;
        culpabilidadCount = 0;
        indecisionCount = 0;
        llantoIncontrolableCount = 0;
        frecuenciaComunicacionVerbalDisminuidaCount = 0;
        futuroFatalistaCount = 0;
        noValeLaPenaVivirCount = 0;
        actividadSocialDisminuidaCount = 0;
        pensamientoMuerteCount = 0;
        pensamientoSuicidaCount = 0;
        personasEnGeneralMalasCount = 0;
        autoImagenComprometidaCount = 0;
        sentimientosSoledadCount = 0;
        noSeSienteQueridoCount = 0;
        generalmenteNoSoyBuenoCount = 0;
        todoSaleMalCount = 0;
        
        maxSentimientosInfelicidadMiserabilidadCount = 0;
        maxApetitoCount = 0;
        maxCulpabilidadCount = 0;
        maxIndecisionCount = 0;
        maxLlantoIncontrolableCount = 0;
        maxFrecuenciaComunicacionVerbalDisminuidaCount = 0;
        maxFuturoFatalistaCount = 0;
        maxNoValeLaPenaVivirCount = 0;
        maxActividadSocialDisminuidaCount = 0;
        maxPensamientoMuerteCount = 0;
        maxPensamientoSuicidaCount = 0;
        maxPersonasEnGeneralMalasCount = 0;
        maxAutoImagenComprometidaCount = 0;
        maxSentimientosSoledadCount = 0;
        maxNoSeSienteQueridoCount = 0;
        maxGeneralmenteNoSoyBuenoCount = 0;
        maxTodoSaleMalCount = 0;

        npcTalkedBeforeCount = 0;
        
        lastAccess = DateTime.Now;
    }
}
