using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public Transform spawnPos;
    public int characterId;
    public float characterSpeed, characterTurnSpeed;

    [Space(2)]
    [Header("Characters")]
    public GameObject[] characters;

    public void _Spawn()
    {
        GameObject temp = Instantiate(this.characters[this.characterId]);

        Player player = temp.GetComponent<Player>();
        player.speedMax = this.characterSpeed;
        player.turnSpeed = this.characterTurnSpeed;


        temp.transform.position = this.spawnPos.position;
        temp.transform.name = "player";

        if (GameObject.Find("_GC").GetComponent<_GC>().isPaused)
        {
            GameObject.Find("_GC").GetComponent<_GC>()._Pause();
        }

        string[] toReset = { "missiles", "collectables" };
        foreach(string reset in toReset)
        {
            GameObject.Find(reset).GetComponent<SpawnSomething>()._DestroyAll();
        }        

    }

    public void _Destroy()
    {
        Destroy(GameObject.Find("player"));
    }
}
