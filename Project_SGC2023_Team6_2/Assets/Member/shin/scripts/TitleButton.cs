using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{

    public void OnClick() //クリック時にログ出力
    {
        Debug.Log("Push Button");
    }

    public void OnClickVer2() //クリック時にログ出力
    {
        Debug.Log("Push Button2");
    }

    public void RandomColor()
    {
        Debug.Log("RandomButton");
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        

    }
}
