using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HouseManager : MonoBehaviour
{
    [SerializeField] private List<HouseData> myHouses;
    [SerializeField] private List<Transform> housePositions;
    [SerializeField] private Button mergeButton;
    [SerializeField] private WalletData walletdata;
    [SerializeField] private int houseamount;
    [SerializeField] private int mergeamount;
    [SerializeField] private HouseData housedata;

    private List<HouseContainer> currentHouses = new List<HouseContainer>();
    private GameManager gameManager;

    private void Start() 
    {
        gameManager = GameManager.myGameManager;
        CheckMergeHouse();
    }

    public void AddHouse(HouseData hd)
    {
        if(houseamount <=walletdata.money)
        {
        var randPos = housePositions [Random.Range(0,housePositions.Count)];
        var house = Instantiate(hd.levels[hd.houselevel].housePrefab, randPos);
        var houseContainer = new HouseContainer();
        houseContainer.houseObject = house;
        houseContainer.myHouse = hd;
        houseContainer.myPos = randPos;

        currentHouses.Add(houseContainer);
        housePositions.Remove(randPos);
        houseamount *=2;
        walletdata.money -= houseamount;

        CheckMergeHouse();
        }
    }

    private bool sameLevelHouseFound;

    private List<HouseContainer> mySameHouses = new List<HouseContainer>();

    private void CheckMergeHouse()
    {
        mySameHouses.Clear();
        sameLevelHouseFound = false;

        foreach(var myHouse in currentHouses) 
        {
            var sameLevelCount = 0;

            foreach(var currHouse in currentHouses)
            {
                if(myHouse.myHouse.houselevel.Equals(currHouse.myHouse.houselevel) && myHouse.myHouse.houselevel <2)
                {
                    sameLevelCount++;
                }

                if(sameLevelCount >= 2 && mergeamount <= walletdata.money)
                {
                    mySameHouses.Add(currHouse);
                    mySameHouses.Add(myHouse);
                    sameLevelHouseFound = true;
                    //mergeamount *=20;

                    break;
                }
            }

            if(sameLevelHouseFound) break;
        }

        if (sameLevelHouseFound)
        {
            if(mergeamount <= walletdata.money) 
            {
                mergeButton.interactable = true;
            }
            //mergeButton.interactable = true;
            //mergeButton.interactable = gameManager.IsThereAnyMoney(mySameHouses[0].myHouse.levels[mySameHouses[0].myHouse.houselevel].mergePrice);
        }

        else if (mergeButton != null)
        {
            mergeButton.interactable = false;
        }
    }

    public void HouseIncome(HouseContainer h)
    {
        CheckMergeHouse();
    }

    private void MergeHouses()
    {
        var currHouseLevel  = mySameHouses[0].myHouse.houselevel;
        currHouseLevel++;
        mergeamount *=20;
        if(walletdata.money <=0)
        {
            mergeButton.interactable = false;
        }
        walletdata.money -=mergeamount;

        var randPos = housePositions [Random.Range(0,housePositions.Count)];
        var house = Instantiate(housedata.levels[housedata.houselevel].housePrefab, randPos);
        housePositions.Remove(randPos);

        foreach(var houseData in myHouses)
        {
            if(houseData.houselevel.Equals(currHouseLevel))
            {
                EventManager.AddHouse(houseData);
                break;
            }
        }

        foreach(var msh in mySameHouses)
        {
            housePositions.Add(msh.myPos);
            currentHouses.Remove(msh);
            msh.houseObject.SetActive(false);
        }

        mySameHouses.Clear();
        CheckMergeHouse();
    }

    private void OnEnable()
    {
        EventManager.onAddHouse += AddHouse;
        EventManager.onHouseMerged += MergeHouses;
    }

    private void OnDisable()
    {
        EventManager.onAddHouse -= AddHouse;
        EventManager.onHouseMerged -= MergeHouses;
    }
}

[Serializable]
public class HouseContainer
{
    public GameObject houseObject;
    public Transform myPos;
    public HouseData myHouse;
}