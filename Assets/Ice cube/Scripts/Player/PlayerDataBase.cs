using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataBase
{
    private const string _levelGroup = "LevelGroup";
    private const string _levelNumber = "LevelNumber";
    private const string _regDate = "RegistrationDate";
    private const string _money = "Money";

    public static bool Registered() => PlayerPrefs.HasKey(_regDate);

    public static int GetGroup() => PlayerPrefs.GetInt(_levelGroup);

    public static void SetGroup(int value) => PlayerPrefs.SetInt(_levelGroup, value);

    public static int GetLevel() => PlayerPrefs.GetInt(_levelNumber);

    public static void SetNumber(int value) => PlayerPrefs.SetInt(_levelNumber, value);

    public static string GetRegistrationDate() => PlayerPrefs.GetString(_regDate);

    public static void SetRegistrationDate(string value) => PlayerPrefs.SetString(_regDate, value);

    public static int GetMoney() => PlayerPrefs.GetInt(_money);

    public static void SetMoney(int value) => PlayerPrefs.SetInt(_money, value);
}
