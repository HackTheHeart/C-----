using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button gameStart;
    [SerializeField] private Button gameQuit;
    private void Awake()
    {
        gameStart.onClick.AddListener(() =>
        {   
            Loader.Load(Loader.Scene.GameScene);
        });
        gameQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1;
    }
}
