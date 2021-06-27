using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDatabase : MonoBehaviour
{


    /// <summary>
    /// manages the highscores
    /// </summary>
  public static List<string> scores = new List<string>();

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            scores.Add("score: 0");
        }
        scores.Reverse();
    }
    
}
