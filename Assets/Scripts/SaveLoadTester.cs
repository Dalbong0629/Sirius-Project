using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTester : MonoBehaviour
{
    public PlayerController player;
    public NPCHealth[] npcs;
    public SaveLoadManager saveLoad;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            saveLoad.SaveGame(player, npcs);

        if (Input.GetKeyDown(KeyCode.F9))
            saveLoad.LoadGame(player, npcs);
    }
}
