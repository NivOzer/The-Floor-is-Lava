using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public int offsetX = -49;
    public int offsetY = -21;
    [SerializeField] TextMeshProUGUI levelText;
    public void LoadLevel(int levelIndex)
    {
        DisplayLevelText(levelIndex);
        map = Resources.Load<Texture2D>($"Levels Textures/Level{levelIndex}");
        if (map == null)
        {
            Debug.LogWarning($"Couldn't load Level Texture: Level{levelIndex}");
            return;
        }
        GenerateLevel();       
    }

    void DisplayLevelText(int level){
        levelText.text = "Level "+level;
        levelText.gameObject.SetActive(true);
        StartCoroutine(OnLevelTextFadeComplete());
    }
    IEnumerator OnLevelTextFadeComplete()
    {
        yield return new WaitForSeconds(1.5f);
        levelText.gameObject.SetActive(false);
    }

    void GenerateLevel(){
        for(int x=0; x < map.width; x++){
            for(int y=0; y < map.height; y++){
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y){
        Color pixelColor = map.GetPixel(x, y);
        // Ignore Transparent pixels
        if(pixelColor.a ==0) return;
        
        foreach (ColorToPrefab colorMapping in colorMappings){
            if(colorMapping.color.Equals(pixelColor)){
                Vector2 position = new Vector2(x +offsetX, y + offsetY);
                GameObject obj = Instantiate(colorMapping.prefab,position,colorMapping.prefab.transform.rotation,transform);
                if (obj.CompareTag("Enemy")){
                    FindFirstObjectByType<GameManager>().remainingEnemies++;
                }
            }
        }

    }
}
