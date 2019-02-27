﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public static class LeaderboardManager
{
    public static int currentScore;
    public static List<LeaderboardEntry> leaderboardEntries;



    public static readonly string savePath = Path.Combine(Application.persistentDataPath, "highscores.dat");



    private static readonly BinaryFormatter formatter = new BinaryFormatter();



    public static void LoadLeaderboardEntries()
    {
        if (!File.Exists(savePath))
            leaderboardEntries = new List<LeaderboardEntry>();

        try
        {
            using (FileStream fs = new FileStream(savePath, FileMode.Open))
                leaderboardEntries = (List<LeaderboardEntry>)formatter.Deserialize(fs);
        }
        catch (Exception)
        {
            Debug.Log("Error opening leaderboard file at " + savePath);

            leaderboardEntries = new List<LeaderboardEntry>();
        }
    }

    public static void SaveLeaderboardEntries()
    {
        try
        {
            if (File.Exists(savePath))
                File.Delete(savePath);

            using (FileStream fs = new FileStream(savePath, FileMode.CreateNew))
                formatter.Serialize(fs, leaderboardEntries);
        }
        catch (Exception)
        {
            Debug.Log("Error saving leaderboard file at " + savePath);
        }
    }



    public static void AddCurrentRun(string name)
    {
        // Find where to insert current run based on index (Lower index = higher score)
        int i = 0;
        while (leaderboardEntries[i].Score > currentScore)
            i++;

        leaderboardEntries.Insert(i, new LeaderboardEntry(name, currentScore));
    }
}



[Serializable]
public readonly struct LeaderboardEntry
{
    public readonly string Name;
    public readonly long Score;
    public readonly long Date;



    public static readonly int nameLength = 6;
    public static readonly int scoreLength = 10;



    public LeaderboardEntry(string name, long score)
    {
        Name = name.PadRight(nameLength);
        Score = score;
        Date = DateTime.UtcNow.Ticks;
    }



    public string DebugToString()
    {
        return $"[LeaderboardEntry] {Name} achieved {Score} score on {new DateTime(Date).ToString("f")}";
    }

    public override string ToString()
    {
        return $"{Name} - {Score.ToString().PadRight(scoreLength)} - {new DateTime(Date).ToString("f").PadLeft(25)}";
    }
}