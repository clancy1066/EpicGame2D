using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool simulates = false;
    public float maxSpeed = 1;
    public float jumpStrength = 1;
    public float speedMod = 0.1f;
    
    bool flipX = false;
    public bool falling = false;

    SpriteRenderer  theSpriteRenderer;
    Rigidbody2D     theRB;
    Animator        theAnimator;

    [SerializeField]
    float jumpInterval = 2.0f;
    float jumpTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        theSpriteRenderer   = GetComponent<SpriteRenderer>();
        theRB               = GetComponent<Rigidbody2D>();    
        theAnimator         = GetComponent<Animator>();

        theRB.gameObject.SetActive(simulates);
     //   theSpriteRenderer.flipX = true;
     //   theSpriteRenderer.flipY = true;

        Debug.Log($"FlipX: {theSpriteRenderer.flipX}, FlipY: {theSpriteRenderer.flipY}");
    }

    void SetAnimationParamBool(string triggerName,bool value)
    {
        theAnimator.SetBool("move", false);
        theAnimator.SetBool("jump", false);
        theAnimator.SetBool("falling", false);
        theAnimator.SetBool("jump", false);

        theAnimator.SetBool(triggerName, value);
      
    }

    // Update is called once per frame
   public void Execute()
    {
        if (!simulates)
            return;

        // If in the middle of a jump, just return
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;

            if (jumpTimer > 0)
                return;

            SetAnimationParamBool("jump", false);
        }
        falling = false; 

        // See if we are falling before checking for Jump
        if (theRB.velocity.y < -0.5f)
        {
            falling = true;
        }
        else
        {
            // Always check for a jump before trying to move
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector2 jumpDir = Vector2.up * jumpStrength;

                // theRB.velocity *= 0;
                theRB.AddForce(jumpDir);

                SetAnimationParamBool("jump", true);

                jumpTimer = jumpInterval;

                return;
            }
        }

        bool moveHorizontal = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));


        // After the jump check
        SetAnimationParamBool("falling", falling);

        float incX = (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0) + (Input.GetKey(KeyCode.RightArrow) ? 1 : 0);
     //   incX = 1;

        //float incY = (Input.GetKey(KeyCode.DownArrow) ? -1 : 0) + (Input.GetKey(KeyCode.UpArrow) ? 1 : 0); ; //(Input.GetMouseButtonDown(0) ? jumpHeight : 0);
        float incY = (Input.GetKeyDown(KeyCode.Space) ? jumpStrength : 0);

        float offsetX = speedMod * incX * Time.deltaTime;
        float offsetY = speedMod * incY * Time.deltaTime;

        Vector2 dir = Vector2.right * offsetX + Vector2.up*offsetY;

        if (dir.x < 0.0)
            flipX = true;
        else
        {
            if (dir.x > 0.1)
                flipX = false;
        }
        Vector2 rbVel = theRB.velocity;

        if (Vector2.Dot(dir, rbVel) < 0.0f)
        {
            theRB.velocity = rbVel * 0.5f;

        }
        else
        {
            if (rbVel.x > maxSpeed)
            {
                rbVel.x = rbVel.normalized.x * maxSpeed;
                theRB.velocity = rbVel;

        
            }
        }

        float linearDrag = (moveHorizontal ? 0.0f:5f);
        

        if (!moveHorizontal)
        { 
            rbVel.x = 0.0f;

            theRB.velocity = rbVel;
        }

        theRB.drag = linearDrag;

        bool  doMove = false;

        if (theRB.velocity.magnitude > 0.01f)
            doMove = true;
        else
        {
        //    dir = Vector2.zero;
            
        }
        theSpriteRenderer.flipX = flipX;

        Debug.Log($"FlipX: {theSpriteRenderer.flipX}, FlipY: {theSpriteRenderer.flipY}");
 

        theRB.AddForce(dir);

        
        if (!falling)
            theAnimator.SetBool("move", doMove);

         //   if (!doMove)
         //       theRB.velocity *= 0;
        //  }

        theRB.angularVelocity = 0;

    }

    static public void Collided()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }


}
