using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingBar : MonoBehaviour
{
    //private RectTransform rect;
    //protected void Awake()
    //{
    //    this.rect = this.GetComponent<RectTransform>();
    //}
    void Update()
    {
        var s = this.transform.localScale;
        if(GameManagerSprite.Instance!=null)
        {
            s.x = GameManagerSprite.Instance.RemainTimeRate;
            this.transform.localScale = s;
        }

    }
}
