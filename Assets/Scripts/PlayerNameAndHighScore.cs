using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameAndHighScore : MonoBehaviour
{
    public static PlayerNameAndHighScore Instance;
    public string currentPlayerName = null; 
    public string bestPlayerName = null;
    public int playerHighScore = -1;
    private void Awake() 
    {
        if (Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
