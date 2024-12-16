using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO;// reference to our game manager

    public GameObject PlayerBulletGO;//Player bullet prefab
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject ExplosionGO;//this is our explosion prefab

    //referencia do texto de vida 
    public TMP_Text LivesUIText;


    const int MaxLives = 3;// Quantidade de vidas
    int lives;//vidas atuais

    public float speed;

    public void Init()
    {
        lives = MaxLives;

        //update the lives UI text
        LivesUIText.text = lives.ToString();

        //Reset the player position to the center of the screen
        transform.position = new Vector2(0, 0);

        //set this player game object to active
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //fire bullets when the spacebar is pressed
        if (Input.GetKeyDown("space"))
        {
            //play the laser sound effect
            GetComponent<AudioSource>().Play();

            //Instantiate the first bullet
            GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;//set the bullet initial position

            //Instantiate the second bullet
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;//set the bullet initial position

        }

        float x = Input.GetAxisRaw("Horizontal");//the value will be -1, 0, or 1 (for left, no imput, and right)
        float y = Input.GetAxisRaw("Vertical");//the value will be -1, 0, or 1 (for down, no imput, and up)

        //now based on input we comput a direction vector, and we normalize it to get a unit vector
        Vector2 direction = new Vector2(x, y).normalized;

        //now we call the function that computes and the player's position
        Move (direction);
    }

    void Move(Vector2 direction)
    {
        //Find the screen limits to the player's movement (left, right, top, and bottom edges of screen)
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // this is the botton-left point (corner) of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //this is the top-right point (corner) of the screen

        max.x = max.x - 0.225f; //subtract the player sprite half width
        min.x = min.x + 0.225f; //add the player sprite half width

        max.y = max.y - 0.285f; //subtract the player sprite half height
        min.y = min.y + 0.285f; //add the player sprite half height

        //Get the player current position
        Vector2 pos = transform.position;

        //Calculate the new position
        pos += direction * speed * Time.deltaTime;

        //Make sure the new position is not outside the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //Update the player's position
        transform.position = pos;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision of the player ship with an enemy ship, or with an enemy bullet
        if((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--;//subtrai a vida
            LivesUIText.text = lives.ToString();//update quantidade de vidas

            if (lives == 0)//Sucumba
            {
                //change game manager state to game over state
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                //hide the player's ship
                gameObject.SetActive(false);
            }
        }
    }

    //function to instantiate an explosion
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }
}