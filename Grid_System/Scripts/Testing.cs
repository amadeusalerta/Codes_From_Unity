using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private GridSystem gridSystem;

    void Start()
    {
        gridSystem = new GridSystem(4,2,10f,new Vector3(20,0));
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            gridSystem.SetValue(UtilsClass.GetMouseWorldPosition(),56);
        }

        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log(gridSystem.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
