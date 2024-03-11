using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 private static GameManager _instance;
 public static GameManager Instance
    {
        get
        {
            if (_instance == null) 
            {
                Debug.Log("Game Mnager is null");
            }

            return _instance;
        }
    }

    public int points;
    public int WinPoints;



    private void Awake()
    {
        _instance = this;
    }

    public void pointsUp (int gain) 
    {
        points += gain;
    }





}
