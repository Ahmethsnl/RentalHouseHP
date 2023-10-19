using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "", fileName = "")]

public class WorkerData : ScriptableObject
{
    public int workerSpeed;
    public int workernum;
    public GameObject workerPrefab;
}
