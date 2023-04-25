using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuUIHandler : MonoBehaviour
{
    
    public TMPro.TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // quit the app and save into a json file the saved data
    public void QuitApplication()
    {
        Application.Quit();
    }

    public void GetPlayerName()
    {
        PlayerNameAndHighScore.Instance.currentPlayerName = inputField.text;
    }
}
