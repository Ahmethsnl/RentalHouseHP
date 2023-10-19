using System;

public static class EventManager 
{
    public static event Action<HouseData> onAddHouse;
    public static void AddHouse(HouseData hd) => onAddHouse?.Invoke(hd);

    public static event Action onHouseMerged;
    public static void MergeHouse() => onHouseMerged?.Invoke();

    public static event Action<HouseContainer> onHouseIncome;
    public static void IncomeHouse(HouseContainer h) => onHouseIncome?.Invoke(h);

    public static event Action onAddWorker;
    public static void AddWorkers() => onAddWorker?.Invoke();
}
