using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrencyTracker : MonoBehaviour
{
    public int totalCurrency = 0;
    public TextMeshProUGUI currencyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.text = "$" + totalCurrency.ToString();
    }
}
