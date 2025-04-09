using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Generates a texture indicating the presence of tiles in a Tilemap.
/// Saves the result as a PNG file.
/// </summary>
public class TilemapToTextureGenerator : MonoBehaviour
{
    [Header("Tilemap Settings")]
    public Tilemap tilemap;

    [Header("Texture Settings")]
    public int textureWidth = 104;
    public int textureHeight = 28;

    // Adjust based on Tilemap bottom left tile position
    public int offsetX = -49;
    public int offsetY = -21;

    private void Start()
    {
        // GenerateTilePresenceTexture();
    }

    /// <summary>
    /// Generates and saves a texture mask where each pixel represents tile presence.
    /// </summary>
    public void GenerateTilePresenceTexture()
    {
        Debug.Log("Generating platform Outline texture...");
        Texture2D texture = CreateEmptyTexture(textureWidth, textureHeight);
        FillTextureFromTilemap(texture);
        SaveTextureAsPNG(texture, "TilePresenceMask.png");
    }

    /// <summary>
    /// Creates a new transparent texture.
    /// </summary>
    private Texture2D CreateEmptyTexture(int width, int height)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                texture.SetPixel(x, y, Color.clear);
            }
        }

        return texture;
    }

    /// <summary>
    /// Sets texture pixels to black for each tile in the platform tilemap.
    /// </summary>
    private void FillTextureFromTilemap(Texture2D texture)
    {
        for (int x = 0; x < textureWidth; x++)
        {
            for (int y = 0; y < textureHeight; y++)
            {
                Vector3Int tilePos = new Vector3Int(offsetX + x, offsetY + y, 0);
                TileBase tile = tilemap.GetTile(tilePos);

                if (tile != null)
                {
                    texture.SetPixel(x, y, Color.black);
                }
            }
        }

        texture.Apply();
    }

    /// <summary>
    /// Saves the given texture to the project folder.
    /// </summary>
    private void SaveTextureAsPNG(Texture2D texture, string fileName)
    {
        string path = Path.Combine(Application.dataPath, fileName);
        File.WriteAllBytes(path, texture.EncodeToPNG());
        Debug.Log("Saved texture to: " + path);
        #if UNITY_EDITOR
                AssetDatabase.Refresh();
        #endif
    }
    
}
