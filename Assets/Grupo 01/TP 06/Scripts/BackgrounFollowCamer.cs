using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundFollowCamera : MonoBehaviour
{
    private Camera cam;
    private SpriteRenderer sr;

    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        
        sr.sortingLayerName = "Background";
        sr.sortingOrder = -5;
    }

    void LateUpdate()
    {
        if (cam == null || sr == null) return;
        
        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * cam.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;
        transform.localScale = new Vector3(
            worldWidth / spriteSize.x,
            worldHeight / spriteSize.y,
            1f
        );
        
        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 10f);
    }
}
