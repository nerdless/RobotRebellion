using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour 
{
    public static int score;

    public Text tex;

    void Awake()
    {
        score = 0;
    }

    void Update()
    {
        tex.text = "Score = " +score;
    }
	
}
