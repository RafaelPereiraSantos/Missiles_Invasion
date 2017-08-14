using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour
{

    public int destroyBonus;

    void Destroy()
    {
        this.GetComponent<Destroy>().Self();
    }

    void Bonus()
    {
        if (destroyBonus > 0)
        {
            FloatingTextControll.CreateFloatingText(string.Format("+{0}", this.destroyBonus.ToString()), this.transform);
            GameObject.Find("MoneyMenu").GetComponent<MoneyCrud>()._SetMoney(destroyBonus);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.transform.tag == "hostil" || col.transform.tag == "player")
        {
            if (col.gameObject.transform.tag == "hostil" && this.transform.tag == "hostil")
            {
                Bonus();
            }
            Destroy();
        }
        if(col.transform.tag == "explosion")
        {
            Destroy();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "player")
        {
            Bonus();
            Destroy();
        }
    }
}
