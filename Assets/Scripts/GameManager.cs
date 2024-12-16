using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject GameTitleGO;
    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    //function to update the game manager state
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
        case GameManagerState.Opening:

              //Hide game over
              GameOverGO.SetActive(false);

             //Display the game title
             GameTitleGO.SetActive(true);

             //Set play button visible (active)
             playButton.SetActive(true);

            break;
        case GameManagerState.Gameplay:

            //Reset the score
            scoreUITextGO.GetComponent<GameScore>().Score = 0;

            //hide play button on game play state
            playButton.SetActive(false);

            //hide the game title
            GameTitleGO.SetActive(false);

            //set the player visible (active) and init the player lives
            playerShip.GetComponent<PlayerControl>().Init();

            //Start enemy spawner
            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

            //start the time counter
            TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

           break;

        case GameManagerState.GameOver:

                //Stop the time counter
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                //Stop enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

                //Display game over
                GameOverGO.SetActive(true);

                //Change game manager state to Opening state after 8 seconds
                Invoke("ChangeToOpeningState", 8f);

            break;
        }
    }

    //Function to set the game manager state
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    //Our Play button will call this function
    //when the user clicks the button.
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    //Function to change game manager state to opening state
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}