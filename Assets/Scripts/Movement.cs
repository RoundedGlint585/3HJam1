using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    [SerializeField]
    float speed = 1.0f;

    [SerializeField]
    float shiftCooldown = 0.5f; 

    [SerializeField]
    float shiftVelocityScale = 1.0f;

    [SerializeField]
    float massOnStart = 1.0f;

    [SerializeField]
    float massOnShift = 10.0f;

    [SerializeField]
    float startColliderSize = 0.5f;
    
    [SerializeField]
    float shiftColliderSize = 0.45f;


    [SerializeField]
    Color colorBeforeShift = Color.red;


    [SerializeField]
    Color colorAfterShift = Color.blue;

    float shiftTimer = 0.5f;
    bool isShiftEnabled = false;

    private float horizontal;
    private float vertical;

    [SerializeField]
    private float endGameTime = 36;

    private float endGameTimer = 0;

    Rigidbody2D rb;

    CircleCollider2D collider;

    SpriteRenderer spriteRenderer;
    public bool isGameFreezed = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorBeforeShift;
        collider = GetComponent<CircleCollider2D>();
        collider.radius = startColliderSize;
        rb.mass = massOnStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameFreezed)
        {
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        shiftTimer += Time.deltaTime;
        shiftTimer = Mathf.Min(shiftTimer, shiftCooldown);

        endGameTimer += Time.deltaTime;
        if (endGameTimer >= endGameTime)
        {
            SceneManager.LoadScene(1);
        }

        spriteRenderer.color = Vector4.Lerp(colorBeforeShift, colorAfterShift, 1 - shiftTimer / shiftCooldown);
        rb.mass = Mathf.Lerp(massOnStart, massOnShift, 1 - shiftTimer / shiftCooldown);
        if (Input.GetKeyDown(KeyCode.Space) && shiftTimer >= shiftCooldown)
        {
            rb.mass = massOnShift;
            shiftTimer = 0.0f;
            rb.velocity += rb.velocity.normalized * shiftVelocityScale * (shiftCooldown - shiftTimer);
        }
    }
    
    private void OnCollisionStay2D(Collision2D collider)
    {
/*        ContactPoint2D[] contactPoints;
        int contactPointsCount = collider.GetContacts(contactPoints);*/
        
        if (collider.transform.CompareTag("Cage"))
        {
            //rb.mass = 0.0001f;
        }
    }


    private void FixedUpdate()
    {
        if (isShiftEnabled) { 
            //rb.AddForce(rb.velocity.normalized * shiftVelocityScale);
        }
        //adding velocity will most likely destroy physical cage
        
        //rb.AddForce(new Vector2(horizontal * speed, vertical * speed));
        rb.velocity += new Vector2(horizontal * speed, vertical * speed);   
    }
}