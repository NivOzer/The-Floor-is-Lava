using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource gameManagerAudioSource;
    [SerializeField] AudioClip levelClearedAudio;
    [SerializeField] Button playButton;
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] GameObject mainMenu;
    [SerializeField] Animator mainMenuAnimator;
    [SerializeField] TextMeshProUGUI YouDiedText;
    public int currentLevel = 1;
    public int remainingEnemies;
    void Start()
    {
        Time.timeScale = 0;
        playButton.onClick.AddListener(StartGame);
    }

    void Update()
    {

    }

    void StartGame(){
        Time.timeScale = 1;
        mainMenuAnimator.SetTrigger("startGame");
        foreach (Transform child in mainMenu.transform) { child.gameObject.SetActive(false); }
        levelGenerator.LoadLevel(currentLevel);
        Debug.Log(remainingEnemies);
    }

    public void EnemyDied(){
        remainingEnemies--;
        if (remainingEnemies <= 0){
            Debug.Log($"Level{currentLevel} cleared");
            LoadNextLevel();
        }
    }

    public void LoadNextLevel(){
        gameManagerAudioSource.PlayOneShot(levelClearedAudio);
        currentLevel++;
        levelGenerator.LoadLevel(currentLevel);
    }

    public IEnumerator EndGame(){
        YouDiedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        // Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }
}
