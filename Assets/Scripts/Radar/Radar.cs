using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour

/*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
 * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
 * esses creditos permaneçam no topo do script.
 * 
 */

{

    public GameObject[] parents;
    public string[] parentsName;

    [Header("diferentes setas para indicar diferentes alvos")]
    public GameObject[] arrows;

    [Header("tag referente a cada seta")]
    public string[] tags;

    private List<GameObject> targets = new List<GameObject>();

    

	// Use this for initialization
	void Start () {

        int i = 0;

        foreach(string name in parentsName)
        {
            parents[i] = GameObject.Find(name); 
                i++;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        int targetsCount = 0;
        foreach(GameObject parent in parents)
        {
            targetsCount += parent.transform.childCount;
        }

        if (this.transform.childCount < targetsCount)
        {

            foreach (GameObject parent in this.parents)
            {
                foreach (Transform child in parent.transform)
                {
                    bool userAsTarget = false;

                    foreach (string tag in this.tags)
                    {
                        if (child.transform.tag == tag)
                        {
                            userAsTarget = true;
                        }
                    }

                    if (!targets.Contains(child.gameObject) && userAsTarget)
                    {
                        targets.Add(child.gameObject);
                        CreateNewArrow(child.gameObject);
                    }
                }
            }

        }
	}

    void CreateNewArrow(GameObject target)
    {
        int i = 0;
        foreach(string tag in tags)
        {
            if(tag == target.transform.tag)
            {
                break;
            }
            i++;
        }

        GameObject temp = Instantiate(arrows[i]);

        temp.GetComponent<Arrow>().target = target;

        temp.transform.name = string.Format("radar_point {0}", tag[i]);

        temp.transform.SetParent(this.transform.parent);

    }
}
