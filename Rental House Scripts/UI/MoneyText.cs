using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    [SerializeField]WalletData myMoney;

    Text moneyText;
    
    void Start()
    {
        moneyText = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        moneyText.text = "Money: " + myMoney.money.ToString();
    }
}
