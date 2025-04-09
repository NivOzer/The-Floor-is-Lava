using Unity.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public int offsetX = -49;
    public int offsetY = -21;
    void Start()
    {
        GenerateLevel();       
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
                Instantiate(colorMapping.prefab,position,colorMapping.prefab.transform.rotation,transform);
            }
        }

    }
}
