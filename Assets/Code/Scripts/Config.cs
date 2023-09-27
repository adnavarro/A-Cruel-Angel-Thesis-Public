using System;
using System.IO;
using Newtonsoft.Json;

public class Config
{
    public string _dbConnectionString { get; set; }

    public static string DBConnectionString
    {
        get
        {
            return LoadConfig()._dbConnectionString;
        }
        set
        {
            var target = LoadConfig();
            target._dbConnectionString = value;
            SaveConfig(target);
        }
    }

    private static void SaveConfig(Config target)
    {
        try
        {
            var serializedObject = JsonConvert.SerializeObject(target);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\config.json", serializedObject);
        }
        catch (Exception e)
        {
            WriteLog("Error al salvar configuracion: " + e);
        }
    }

    private static Config LoadConfig()
    {
        try
        {
            var file = File.ReadAllText(Directory.GetCurrentDirectory() + "\\config.json");
            if (file == null ||file == "{}")
                throw new FileNotFoundException();
            return JsonConvert.DeserializeObject<Config>(file);
        }
        catch (FileNotFoundException e)
        {
            WriteLog("Error: " + e);
            var target = new Config();
            target._dbConnectionString = "mongodb+srv://dbadmin:123456tesis@cluster0.r1e5hau.mongodb.net/?retryWrites=true&w=majority";
            SaveConfig(target);
            return target;
        } 
        catch (Exception e)
        {
            WriteLog("Error al cargar configuracin: " + e);
            var target = new Config();
            target._dbConnectionString = "mongodb+srv://dbadmin:123456tesis@cluster0.r1e5hau.mongodb.net/?retryWrites=true&w=majority";
            SaveConfig(target);
            return target;
        }
    }

    public static void WriteLog(string text)
    {
        try
        {
            var fs = new FileStream(Directory.GetCurrentDirectory() + "\\log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using (var mStreamWriter = new StreamWriter(fs))
            {
                mStreamWriter.BaseStream.Seek(0, SeekOrigin.End);
                mStreamWriter.WriteLine("[" + DateTime.Now + "] " +  text);
            }
            fs.Close();
        }
        catch (Exception) { }
    }
}
