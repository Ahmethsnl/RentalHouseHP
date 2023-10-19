using UnityEngine;

public class UiManager : MonoBehaviour
{
    public void AddHouseButton(HouseData myData)
    {
        EventManager.AddHouse(myData);
    }

    public void MergeButton()
    {
        EventManager.MergeHouse();
    }

    public void AddWorkerButton() 
    {
        EventManager.AddWorkers();
    }
}
