using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    private int frame;

    private float animationSpeed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (sprites.Length == 6)
        {
            animationSpeed = 0.45f;
        }
        else
        {
            animationSpeed = 1f;
        }
    }

    private void OnEnable()
    {
        //To get called next frame
        Invoke(nameof(Animate), 0f);
    }

    private void Animate()
    {
        frame++;

        if(frame >= sprites.Length)
        {
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }

        Invoke(nameof(Animate), animationSpeed / GameManager.Instance.gameSpeed); 
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
