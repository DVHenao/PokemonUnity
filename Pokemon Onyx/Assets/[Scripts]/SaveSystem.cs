using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private const string DATA_FILE =  "/playerdata";
    

    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + DATA_FILE, FileMode.Create);

        PlayerData data = new PlayerData(player);
        // Erializing for saving Player data
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + DATA_FILE))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + DATA_FILE, FileMode.Open);
            PlayerData playerdata =  formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerdata;
        }
        else
        {
            Debug.LogWarning("There's no save data");
            return null;
        }
    }

    public static bool DoesSaveExist()
    {
        if (File.Exists(Application.persistentDataPath + DATA_FILE))
        {
            Debug.Log(Application.persistentDataPath);
            return true;
        }
        return false;
    }
}
