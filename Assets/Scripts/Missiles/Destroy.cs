using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public GameObject smokePreFab;
    public string explosionName;

    // Use this for initialization
    public void Self()
    {
        if (this.explosionName != "" && GetComponent<Renderer>().isVisible)
            //o audio só toca se ele existir e se o objeto estiver visivel para a camera para não haver sons toda hora 
        {
            GameObject.Find("_SFX").GetComponent<AudioManager>().Play(this.explosionName);
        }        

        if (this.smokePreFab != null)
        {
            SpawnSmoke();
        }

        Destroy(this.gameObject);
    }

    void SpawnSmoke()
    {
        GameObject temp;
        temp = Instantiate(this.smokePreFab);
        temp.transform.name = "smoke";
        temp.transform.position = this.transform.position;
        temp.transform.rotation = this.transform.rotation;
        temp.transform.parent = this.transform.parent;
    }
}
