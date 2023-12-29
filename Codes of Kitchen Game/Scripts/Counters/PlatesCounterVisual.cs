using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform PlateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList=new List<GameObject>();
    }

    private void Start()
    {
        plateCounter.OnPlateSpawned+=PlatesCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved+=PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender,System.EventArgs e)
    {
        GameObject plateGameObject=plateVisualGameObjectList[plateVisualGameObjectList.Count-1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender,System.EventArgs e)
    {
        Transform plateVisualTransform=Instantiate(PlateVisualPrefab,counterTopPoint);

        float plateOffsetY=.1f;
        plateVisualTransform.localPosition=new Vector3(0,plateOffsetY*plateVisualGameObjectList.Count,0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
