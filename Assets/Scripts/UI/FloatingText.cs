using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    Animator animator;
    public Text damageText;

    void Start()
    {
        this.animator = this.transform.GetChild(0).GetComponent<Animator>();

        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(this.gameObject, clipInfo[0].clip.length);
        //destroi o texto assim que a animaçao acabar
    }

    public void SetText(string text)
    {
        this.damageText.text = text.ToString();
    }
}
