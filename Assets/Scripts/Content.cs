using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Content : MonoBehaviour
{
    public Image image;
    public Text cardName;
    public Text atk;
    public Text def;
    public Text hp;
    public HeroData data;

    public void setValues(Sprite img, string name, int atk, int def, int hp, HeroData data)
    {
        image.sprite = img;
        this.cardName.text = name;
        this.atk.text = "Atk: " + atk.ToString();
        this.def.text = "Def: " + def.ToString();
        this.hp.text = "Health: " + hp.ToString();
        this.data = data;
    }

}
