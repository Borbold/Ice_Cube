using UnityEngine;
using System.Collections.Generic;

public class AmplitudeSettings : MonoBehaviour
{
    [SerializeField] private readonly string _amplitudeKey;

    private const string _dateFormat = "dd.MM.yy"; 

    private static AmplitudeSettings _instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init(_amplitudeKey);
    }

    private void OnEnable()
    {
        if (_instance == this)
        {
            Amplitude.Instance.startSession();
            if (PlayerDataBase.Registered())
            {
                UpdateRegistrationDuration();
            }
            else
            {
                SetRegistrationDate();
            }
            UpdateSessionID();
        }
    }

    private void OnDisable()
    {
        if (_instance == this)
        {
            Amplitude.Instance.endSession();
            Amplitude.Instance.uploadEvents();
        }
    }

    private void Start()
    {
        GameStart();
    }

    public void LevelWin()
    {
        Amplitude.Instance.logEvent(EventNames.LevelWin);
    }

    public void LevelFail()
    {
        Amplitude.Instance.logEvent(EventNames.LevelFail);
    }

    public void LevelStart(int level)
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("level", level);
        Amplitude.Instance.logEvent(EventNames.LevelStart, eventProps);
        int maxLevel = Mathf.Max(PlayerDataBase.GetLevel(), level);
        Amplitude.Instance.setUserProperty(UserParamsNames.LastLevel, maxLevel);
    }

    public void MainMenu() => Amplitude.Instance.logEvent(EventNames.MainMenu);

    public void SetCurrentSoft(int soft)
    {
        Amplitude.Instance.setUserProperty(UserParamsNames.CurrentSoft, soft);
    }

    private void GameStart() => Amplitude.Instance.logEvent(EventNames.GameStart);

    private void SetRegistrationDuration(int days)
    {
        Amplitude.Instance.setUserProperty(UserParamsNames.DaysAfter, days);
    }

    private void SetRegistrationDate()
    {
        var date = System.DateTime.Now.ToString(_dateFormat);
        PlayerDataBase.SetRegistrationDate(date);
        Amplitude.Instance.setOnceUserProperty(UserParamsNames.RegistrationDay, date);
    }

    private void UpdateSessionID()
    {
        int sessionID = (int)Amplitude.Instance.getSessionId();
        Amplitude.Instance.setUserProperty(UserParamsNames.SessionId, sessionID);
    }

    private System.DateTime GetRegistrationDate()
    {
        string regDate = PlayerDataBase.GetRegistrationDate();
        var date = System.DateTime.ParseExact(regDate, _dateFormat, System.Globalization.CultureInfo.InvariantCulture);
        return date;
    }

    private void UpdateRegistrationDuration()
    {
        
        System.DateTime regDate = GetRegistrationDate();
        int regDuration = (int)(System.DateTime.Now - regDate).TotalDays;
        SetRegistrationDuration(regDuration);
    }
}

public class EventNames
{
    public static readonly string LevelWin = "level_win";
    public static readonly string MainMenu = "main_menu";
    public static readonly string LevelFail = "level_fail";
    public static readonly string GameStart = "game_start";
    public static readonly string LevelStart = "level_start";
}

public class UserParamsNames
{
    public static readonly string RegistrationDay = "reg_day";
    public static readonly string SessionId= "session_id";
    public static readonly string DaysAfter = "days_after";
    public static readonly string CurrentSoft = "current_soft";
    public static readonly string LastLevel = "level_last";
}
