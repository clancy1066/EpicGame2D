using UnityEngine;

public class FogOfWarController : MonoBehaviour
{
    [Header("Fog Settings")]
    public Material fogMaterial; // The material using the FogOfWar shader
    public Transform[] revealPoints; // Points that reveal the fog (e.g., players, torches)
    public float revealRadius = 5f; // Radius of revealed areas

    [Header("Fog Mask Settings")]
    public int textureSize = 512; // Resolution of the fog mask
    public Color fogColor = Color.black; // Default fog color

    private Texture2D fogMask;
    private Color[] fogPixels;
    private Vector2 textureScale;

    void Start()
    {
        // Create the fog mask texture
        fogMask = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        fogMask.wrapMode = TextureWrapMode.Clamp;
        fogMask.filterMode = FilterMode.Bilinear;

        // Initialize the fog texture with the fog color
        fogPixels = new Color[textureSize * textureSize];
        for (int i = 0; i < fogPixels.Length; i++)
        {
            fogPixels[i] = fogColor;
        }
        fogMask.SetPixels(fogPixels);
        fogMask.Apply();

        // Assign the fog mask to the shader material
        fogMaterial.SetTexture("_FogMask", fogMask);

        // Calculate the texture scale based on the object's scale
        Vector3 scale = transform.localScale;
        textureScale = new Vector2(scale.x, scale.y);
    }

    void Update()
    {
        // Update the fog mask based on reveal points
        foreach (Transform point in revealPoints)
        {
            if (point != null)
            {
                RevealFogAtPoint(point.position);
            }
        }

        // Apply the updated fog mask
        fogMask.SetPixels(fogPixels);
        fogMask.Apply();
    }

    private void RevealFogAtPoint(Vector2 worldPosition)
    {
        // Convert world position to texture coordinates
        Vector2 textureCoord = new Vector2(
            (worldPosition.x - transform.position.x) / textureScale.x + 0.5f,
            (worldPosition.y - transform.position.y) / textureScale.y + 0.5f
        );

        int centerX = Mathf.RoundToInt(textureCoord.x * textureSize);
        int centerY = Mathf.RoundToInt(textureCoord.y * textureSize);
        int radius = Mathf.RoundToInt(revealRadius * textureSize / textureScale.x);

        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                int px = centerX + x;
                int py = centerY + y;

                if (px >= 0 && px < textureSize && py >= 0 && py < textureSize)
                {
                    float distance = Mathf.Sqrt(x * x + y * y);
                    if (distance <= radius)
                    {
                        int index = py * textureSize + px;
                        fogPixels[index] = Color.clear; // Clear the fog
                    }
                }
            }
        }
    }
}