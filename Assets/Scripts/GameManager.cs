using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] GameObject mainMenu;
    [SerializeField] Animator mainMenuAnimator;
    [SerializeField] TextMeshProUGUI YouDiedText;
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

    public IEnumerator EndGame(){
        YouDiedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        // Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }
}
