using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsData", menuName = "Data/StatsData", order = 51)]
public class StatsData : ScriptableObject
{
    [SerializeField] private int _numberOfWins = 0;
    [SerializeField] private int _numberOfkills = 0;
    [SerializeField] private int _bestTimeInSeconds = 0;
    [SerializeField] private string _bestTime = "";

    public int NumberOfWins => _numberOfWins;
    public int NumberOfkills => _numberOfkills;
    public string BestTime => _bestTime;

    private void CountingKilledEnemies()
    {
        _numberOfkills++;
    }

    public void CountingWins()
    {
        _numberOfWins++;
    }

    public void СomparePlayingTime(int minute, int second)
    {
        int timeInSeconds = minute * 60 + second;

        if (timeInSeconds < _bestTimeInSeconds || _bestTimeInSeconds <= 0)
        {
            _bestTimeInSeconds = timeInSeconds;
            _bestTime = minute + " : " + second;
        }
    }

    private void OnEnable()
    {
        var json = File.ReadAllText(GetFilePath());

        JsonUtility.FromJsonOverwrite(json, this);
    }

    private void OnDisable()
    {
        var json = JsonUtility.ToJson(this);

        File.WriteAllText(GetFilePath(), json);
    }

    private string GetFilePath()
    {
        return Application.persistentDataPath + $"/{this.name}.so";
    }
}