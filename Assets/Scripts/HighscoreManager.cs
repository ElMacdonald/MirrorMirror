using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System.Linq;

public class HighScoreManager : MonoBehaviour
{
    [Header("References")]
    public TMP_Text displayText;
    public CurrencyTracker currencyTracker;

    [Header("Settings")]
    public string fileName = "highscores.txt";
    public int maxEntries = 10;

    [Header("Default Dummy Scores")]
    public List<string> defaultNames = new List<string>()
    {
        "AAA", "BBB", "CCC", "DDD", "EEE"
    };

    public List<int> defaultScores = new List<int>()
    {
        1000, 800, 600, 400, 200
    };

    private Dictionary<string, int> highScores = new Dictionary<string, int>();
    private string filePath;

    void Awake()
    {
        currencyTracker = FindFirstObjectByType<CurrencyTracker>();
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(filePath))
            CreateDefaultScores();

        LoadScores();
        SortScores();
        SaveScores();
        UpdateDisplay();
    }

    public void SubmitScore(string username)
    {
        int score = currencyTracker.totalCurrency;

        if (highScores.ContainsKey(username))
        {
            if (score > highScores[username])
                highScores[username] = score;
        }
        else
        {
            highScores.Add(username, score);
        }

        SortScores();
        SaveScores();
        UpdateDisplay();
    }

    void CreateDefaultScores()
    {
        highScores.Clear();

        int count = Mathf.Min(defaultNames.Count, defaultScores.Count);

        for (int i = 0; i < count; i++)
            highScores[defaultNames[i]] = defaultScores[i];

        SaveScores();
    }

    void LoadScores()
    {
        highScores.Clear();

        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(':');
            if (parts.Length != 2) continue;

            if (int.TryParse(parts[1], out int score))
                highScores[parts[0]] = score;
        }
    }

    void SortScores()
    {
        highScores = highScores
            .OrderByDescending(entry => entry.Value)
            .Take(maxEntries)
            .ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    void SaveScores()
    {
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            foreach (var entry in highScores)
                writer.WriteLine($"{entry.Key}:{entry.Value}");
        }
    }

    void UpdateDisplay()
    {
        if (displayText == null) return;

        displayText.text = "HIGH SCORES\n\n";

        int rank = 1;
        foreach (var entry in highScores)
        {
            displayText.text += $"{rank}. {entry.Key} - {entry.Value}\n";
            rank++;
        }
    }
}
