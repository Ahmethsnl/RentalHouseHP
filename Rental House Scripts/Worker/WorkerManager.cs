using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class WorkerManager : MonoBehaviour
{
    [SerializeField] private WalletData myWallet;
    [SerializeField] private WorkerData workerData;
    [SerializeField] private CinemachineSmoothPath path;
    [SerializeField] private List<WorkerController> workers;
    [SerializeField] private Button addWorkButton;
    [SerializeField] private int workeramount;
    
    private int workernums;
    
    private void Start() 
    {
        workernums = workerData.workernum;
    }

    public void AddWorker()
    {
        if(workernums < 3 && workeramount <= myWallet.money)
        {
            var worker = Instantiate(workerData.workerPrefab);
            worker.transform.SetParent(path.transform);
            if (worker.TryGetComponent(out WorkerController w))
            {
                workers.Add(w);
                workeramount *= 20;
                myWallet.money -= workeramount;
            }
            PositionSetting();

            workernums +=1;
        }
        // else if(workernums >= 3 || workeramount > myWallet.money)
        // {
        //      addWorkButton.interactable = false;
        // }
    }

    private void PositionSetting()
    {
        for (int i = 0; i < workers.Count; i++)
        {
            workers[i].SetMyPosition(path, (path.PathLength / workers.Count) * i);
        }
    }

    private void OnEnable()
    {
        EventManager.onAddWorker += AddWorker;
    }

    private void OnDisable()
    {
        EventManager.onAddWorker -= AddWorker;
    }

}

