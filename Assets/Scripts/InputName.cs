using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputName : MonoBehaviour
{
    public static InputName inputInstance;
    private string playername;
    

    public void StoreName()
    {
        playername = gameObject.GetComponent<TMP_InputField>().text;
        Debug.Log("Playername is: " + playername);
    }    
}
