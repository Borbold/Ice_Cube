using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour
{
    [SerializeField] private CubeGroup _group;
    [Header("Settings")]
    [SerializeField] private int _levelgroup;
    [SerializeField] private int _levelnumber;
    [SerializeField] private int _startCubesAmount;

    private void Start()
    {
            for (var i = 0; i < _startCubesAmount; i++)
            {
                _group.AddNew();
            }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Time.timeScale = 0.3f;
        if (Input.GetKeyDown(KeyCode.S))
            Time.timeScale = 1f;
        if (Input.GetKeyDown(KeyCode.D))
            Time.timeScale = 3f;
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetLevel();
        }
    }

    public void SetLevel()
    {
        PlayerDataBase.SetGroup(_levelgroup);
        PlayerDataBase.SetNumber(_levelnumber);
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
