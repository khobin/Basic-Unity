using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;

    public Player player;
    public GameObject[] Stages;
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartBtn;

    public void NextStage()
    {
        //Change Stage
        if(stageIndex < Stages.Length - 1)
        {
            PlayerRepos();
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);

            UIStage.text = "STAGE " + (stageIndex + 1);
        }
        else
        {
            //Game Clear
            Time.timeScale = 0;
            //Result UI
            Debug.Log("Game Clear");
            //Restart Button UI
            Text btnText = RestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            RestartBtn.SetActive(true);
        }


        //Point Sum
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
            
        else
        {
            //HealthUI Off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            //player Die Effect
            player.OnDie();
            //Result UI
            Debug.Log("Dead");
            //Retry Button UI
            RestartBtn.SetActive(true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(health > 1)
            {
                PlayerRepos();
            }

            HealthDown();

        }
            
    }
    void PlayerRepos()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); 
    }
}
