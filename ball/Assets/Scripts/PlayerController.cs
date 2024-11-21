using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject SOUND;
    public GameObject Background;
    public GameObject Lose;
    public GameObject Win;
    public GameObject Ded;
    public GameObject Won;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        winTextObject.SetActive(false);
        count = 0;
        
        SetCountText();
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 4) 
        {
            winTextObject.SetActive(true);
            Background.GetComponent<AudioSource>().Stop();
            Win.GetComponent<AudioSource>().Play();
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            this.gameObject.transform.Find("Won").GetComponent<ParticleSystem>().Play();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.transform.Find("Burst").GetComponent<ParticleSystem>().Play();
            SOUND.GetComponent<AudioSource>().Play(); //Was destroying object too fast. Idk. Run program when see, and notify me. 
            count = count + 1;
            Destroy(other.gameObject, 0.15f);
            SetCountText();
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            this.gameObject.transform.Find("Ded").GetComponent<ParticleSystem>().Play();
            //Destroy(gameObject);
            Destroy(gameObject, 0.05f);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose... :[";
            Lose.GetComponent<AudioSource>().Play();
            Background.GetComponent<AudioSource>().Stop();
        }
        if (collision.gameObject.CompareTag("DynamicBox"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
        if (collision.gameObject.CompareTag("Bonk"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
    }
  
}
