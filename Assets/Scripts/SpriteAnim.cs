
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnim : MonoBehaviour
{
    SpriteRenderer  spriteRenderer;

    [SerializeField]
    Sprite[] frames;

    [SerializeField]
    bool loopAtStart = false;

    [SerializeField]
    protected float frameHoldTime = 2.0f;

    protected int      curFrame = 0;
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

        newFrame %= frames.Length;

        spriteRenderer.sprite = frames[newFrame];
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
