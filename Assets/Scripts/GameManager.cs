using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] GameObject mainMenu;
    [SerializeField] Animator mainMenuAnimator;
    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        playButton.onClick.AddListener(StartGame);
    }

    void StartGame(){
        Time.timeScale = 1;
        mainMenuAnimator.SetTrigger("startGame");
        foreach (Transform child in mainMenu.transform) { child.gameObject.SetActive(false); }
    }
}
