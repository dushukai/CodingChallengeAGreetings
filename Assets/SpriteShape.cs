using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteShape : MonoBehaviour
{
    // Start is called before the first frame update
    private Image myImage;

    protected void Awake()
    {
        this.myImage = this.GetComponent<Image>();
    }

    public void SwitchRandomColor()
    {

        this.myImage.color = GameManagerSprite.Instance.GetRandomColor();
       
    }

    public void SwitchRandomShape()
    {
        this.myImage.sprite = GameManagerSprite.Instance.GetRandomShape();
    }


}
