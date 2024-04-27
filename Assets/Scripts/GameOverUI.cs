using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = "Kills: " + TankController.instance.score.ToString();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
