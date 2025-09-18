using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerPosX, playerPosY, playerPosZ;
    public int playerHP;

    public string[] npcNames;
    public int[] npcHP;
}

public class SaveLoadManager : MonoBehaviour
{
    private string savePath;

    void Awake()
    {
        savePath = Application.persistentDataPath + "/save.json";
    }

    public void SaveGame(PlayerController player, NPCHealth[] npcs)
    {
        GameData data = new GameData();

        // 플레이어 정보
        data.playerPosX = player.transform.position.x;
        data.playerPosY = player.transform.position.y;
        data.playerPosZ = player.transform.position.z;
        data.playerHP = player.GetComponent<Health>().currentHealth;

        // NPC 정보
        data.npcNames = new string[npcs.Length];
        data.npcHP = new int[npcs.Length];
        for (int i = 0; i < npcs.Length; i++)
        {
            data.npcNames[i] = npcs[i].gameObject.name;
            data.npcHP[i] = npcs[i].GetComponent<Health>().currentHealth;
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("게임 저장 완료: " + savePath);
    }

    public void LoadGame(PlayerController player, NPCHealth[] npcs)
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("저장 파일 없음");
            return;
        }

        string json = File.ReadAllText(savePath);
        GameData data = JsonUtility.FromJson<GameData>(json);

        // 플레이어 정보 적용
        player.transform.position = new Vector3(data.playerPosX, data.playerPosY, data.playerPosZ);
        player.GetComponent<Health>().SetHealth(data.playerHP);

        // NPC 정보 적용
        for (int i = 0; i < npcs.Length; i++)
        {
            if (i < data.npcHP.Length)
                npcs[i].GetComponent<Health>().SetHealth(data.npcHP[i]);
        }

        Debug.Log("게임 로드 완료");
    }
}
