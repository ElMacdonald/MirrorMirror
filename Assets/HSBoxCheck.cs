using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HSBoxCheck : MonoBehaviour
{
    public CurrencyTracker ct;
    public TextMeshProUGUI txt;
    public HighScoreManager hsm;
    public TMP_InputField inputField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ct = FindFirstObjectByType<CurrencyTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "Your Score: $" + ct.totalCurrency.ToString();
    }

    public void submitHS()
    {
        string playerName = inputField.text;
        hsm.SubmitScore(playerName);
    }
}
