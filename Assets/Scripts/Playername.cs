using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Playername : MonoBehaviour
{
    DataManager dataManager = DataManager.dataManager;
    private string playername;
    // Start is called before the first frame update
    private void Awake()
    {
        playername = dataManager.GetName();
        Debug.Log("Player: " + playername + " At game screen");
        TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>();
        textMesh.SetText(playername);



    }
}
