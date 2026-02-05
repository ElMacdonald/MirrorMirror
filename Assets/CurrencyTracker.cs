using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrencyTracker : MonoBehaviour
{
    public int totalCurrency = 0;
    public TextMeshProUGUI currencyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // sets self to not be destroyed on scene load
        DontDestroyOnLoad(this.gameObject);
        if(currencyText == null)
            if(GameObject.Find("CurrencyText") != null)
                currencyText = GameObject.Find("CurrencyText").GetComponent<TextMeshProUGUI>();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    // Searches for text again when loading into scene
    public void FindText()
    {
        if(GameObject.Find("CurrencyText") != null)
            currencyText = GameObject.Find("CurrencyText").GetComponent<TextMeshProUGUI>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindText();
    }
    // Update is called once per frame
    void Update()
    {
        if(currencyText != null)
            currencyText.text = "$" + totalCurrency.ToString();
    }
}
