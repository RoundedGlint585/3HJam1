using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 direction = new Vector3(1.0f, -1.0f, 0.0f);

    public float speed = 1.0f;

    public float delay = 1.0f;

    float delayTimer = 0.0f;

    void Start()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Cage").GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(collision.transform.gameObject);
            //rb.mass = 0.0001f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        delayTimer += Time.deltaTime;
        if(delay < delayTimer)
        {
            this.transform.position += direction.normalized * speed;
        }
    }
}
