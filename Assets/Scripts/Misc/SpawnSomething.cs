using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSomething : MonoBehaviour
{

    [Header("limites do cenario")]

    public Transform limitLeft;
    public Transform limitRight;
    public Transform limitTop;
    public Transform limitButton;

    [Header("tempo para spawnar")]

    [Tooltip("aumenta a quantidade permitida com o tempo")]
    public bool increaseMaxQuant = false;

    [Tooltip("tempo para cada incremento")]
    public float timeToIncrease;
    float timetToIncreaseCount;

    [Tooltip("tempo para cada spawn")]
    public float timeToSpawn;
    float timeToSpawnCount;

    float currentTime;

    [Header("maximo de objetos simultaneos")]
    public int maxSpawnSameTime = 1;
    public int maxSpawn;
    int currentMaxSpawn;

    [Header("Pre Fabs")]
    public GameObject[] objectPreFab;

    public int difficult = 1;

    void Start()
    {
        this.timetToIncreaseCount = this.timeToIncrease;
        this.timeToSpawnCount = this.timeToSpawn;
        this.currentMaxSpawn = this.maxSpawn;
        this.currentTime = 0;
    }

    void Update()
    {
        if (GameObject.Find("_GC").GetComponent<_GC>().isPaused || !GameObject.Find("_GC").GetComponent<_GC>().isOnPlaying)
        {
            return;
        }

        this.currentTime += Time.deltaTime;

        if (this.currentTime > this.timeToSpawnCount)
        {
            if (this.transform.childCount <= this.currentMaxSpawn && GameObject.Find("player"))
            {
                Spawn();
            }

            this.timeToSpawnCount = this.currentTime + this.timeToSpawn;

        }

        if (this.increaseMaxQuant)
            //se for marcado para aumentar a quantidade maxima
        {
            if(this.currentTime > this.timetToIncreaseCount)
                //quando o tempo para aumetnar é alcançado
            {
                this.timetToIncreaseCount = this.currentTime + this.timeToIncrease;
                this.currentMaxSpawn += 1;
            }
        }

        //auto incremento de difuldade
    }

    public void _DestroyAll()
    {
        if (transform.GetChildCount() > 0)
        {
            foreach (Transform m in transform)
            {

                Destroy(m.gameObject);

                Start();

            }
        }

    }

    void Spawn()
    {
        for (int c = 0; c < this.maxSpawnSameTime; c++)
        {
            if(this.transform.childCount >= this.maxSpawn)
            {
                return;
            }

            int a = Random.Range(0, 2);
            int b = Random.Range(0, 2);

            int i = Random.Range(0, this.objectPreFab.Length);

            float rot = 0;

            GameObject temp = Instantiate(objectPreFab[i]);

            Vector3 pos = Vector3.zero;

            switch (a)
            {
                case 0:
                    //vertical
                    {
                        pos.x = Random.Range(this.limitLeft.transform.position.x, this.limitRight.transform.position.x);

                        switch (b)
                        {
                            case 0:
                                {
                                    pos.y = limitTop.transform.position.y;
                                    rot = -180;
                                    break;
                                }
                            case 1:
                                {
                                    pos.y = limitButton.transform.position.y;
                                    rot = 0;
                                    break;
                                }
                        }
                        break;
                    }
                case 1:
                    //horizontal
                    {
                        pos.y = Random.Range(this.limitButton.transform.position.y, this.limitTop.transform.position.x);

                        switch (b)
                        {
                            case 0:
                                {
                                    pos.x = limitRight.transform.position.x;
                                    rot = 90;
                                    break;
                                }
                            case 1:
                                {
                                    pos.x = limitLeft.transform.position.x;
                                    rot = -90;
                                    break;
                                }
                        }

                        break;
                    }
            }

            //cada novo missel spawnado aumenta a dificuldade

            temp.transform.position = pos;
            temp.transform.eulerAngles = new Vector3(0, 0, rot);
            temp.transform.SetParent(this.gameObject.transform);
            temp.transform.SetParent(transform);
            temp.transform.name = "missile";
        }
    }
}
