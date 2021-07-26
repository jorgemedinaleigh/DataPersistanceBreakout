using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;
    private string playername;

    private void Awake()
    {
        dataManager = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StoreName()
    {
        playername = GameObject.Find("InputName").GetComponent<TMP_InputField>().text;
        Debug.Log("Playername is: " + playername);
    }

    public string GetName()
    {
        return playername;
    }
}
