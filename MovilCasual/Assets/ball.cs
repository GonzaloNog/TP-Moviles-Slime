using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody2D rb;

    public float releseTime = 0.15f;
    public float maxDistance;
    public AudioClip move;
    public AudioClip jump;

    private bool isPressed = false;
    private bool changePoint = false;
    private bool enter = true;
    private Vector2 savePosition;
    private bool StartPoint = true;
    private AudioSource aud;
    private void Start()
    {
        aud = GetComponent<AudioSource>();
        Time.timeScale = 1;
        GetComponent<Rigidbody2D>().sharedMaterial.bounciness = 0.2f;
    }
    void Update()
    {
        float dist = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), GetComponent<SpringJoint2D>().connectedBody.position);
        if (isPressed && GameManager.instance.GetPlay())
        {
            if (dist < maxDistance)
            {
                rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                savePosition = rb.position;
            }
            else
                rb.position = savePosition;
        }
        else if(!changePoint && enter)
        {
            rb.velocity = Vector3.zero;
        }
        if(this.transform.position.y < -6)
        {
            EndGame();
        }
        if (this.transform.position.y > 5)
        {
            rb.velocity = Vector3.zero;
        }
    }
    void OnMouseDown()
    {
        if (GetComponent<SpringJoint2D>().enabled && GameManager.instance.GetPlay())
        {
            isPressed = true;
            enter = false;
        }   
        rb.isKinematic = true;
    }
    private void OnMouseUp()
    {
        if (GameManager.instance.GetPlay())
        {
            
            aud.clip = move;
            aud.time = 0.3f;
            aud.Play();
            isPressed = false;
            rb.isKinematic = false;
            StartCoroutine(Release());
            if (StartPoint)
            {
                GameManager.instance.StartPointOff();
                StartPoint = false;
            }
        }
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        StartCoroutine(Wait());
        GameManager.instance.SetStopPoints(true);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(releseTime);
        changePoint = true;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Point" && changePoint)
        {
            GameManager.instance.SetScore(1);
            GameManager.instance.SetPointEntity(collision.gameObject);
            GameManager.instance.SetStopPoints(false);
            transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, transform.position.z);
            GetComponent<SpringJoint2D>().connectedBody = collision.GetComponent<Rigidbody2D>();
            GetComponent<SpringJoint2D>().enabled = true;
            changePoint = false;
            enter = true;
        }
        if(collision.tag == "Start")
        {
            rb.velocity = Vector3.zero;
        }
        if(collision.tag == "End")
        {
            EndGame();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.tag == "Point" && !isPressed)
            //transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, transform.position.z);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        
        if(coll.gameObject.tag == "Jump")
        {
            aud.clip = jump;
            aud.time = 0.0f;
            aud.Play();
            GameManager.instance.SetScore(5);
            var anim = coll.gameObject.GetComponent<Animator>();
            anim.SetBool("Jump", true);
        }
    }
    private void EndGame()
    {
        GameManager.instance.EndGame();
    }
}
