
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnim : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;

    [SerializeField]
    protected Sprite[] frames;

    [SerializeField]
    bool loopAtStart = false;

    [SerializeField]
    protected float frameHoldTime = 2.0f;

    // Made these puvblic for the animation
    public bool changeNow = false;
    public int          curFrame = 0;
   
    protected float    stateTimer = -1.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeFrame(curFrame);

        if (loopAtStart)
        {
            stateTimer = frameHoldTime;
        }

    }

    protected void ChangeFrame(int newFrame)
    {
        if (spriteRenderer == null)
            return;

        if (frames.Length == 0)
            return;
        newFrame %= frames.Length;

        spriteRenderer.sprite = frames[newFrame];
    }

    private void Update()
    {
        if (changeNow)
        {
            changeNow = false;
            ChangeFrame(curFrame);
            stateTimer = frameHoldTime;
        }
    }

    virtual public void Execute()
    { 
        if (!loopAtStart)
            return;

        stateTimer -= Time.deltaTime;

        if (stateTimer < 0.0f)
        {
            stateTimer = frameHoldTime;
            curFrame ++;
            curFrame %= frames.Length;

            ChangeFrame(curFrame);
        }
        


    }

}
