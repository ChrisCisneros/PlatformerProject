using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;


public class Game : MonoBehaviour
{
    public EthanCharacter reference;
    public GameObject Rock;

    public GameObject Brick;

    public GameObject QuestionBox;

    public GameObject Stone;

    public Text timerText;
     float timer = 100;

    public Text scoreText;
    public int score = 0;

    public Text coinText;
    public int coinAmount = 0;

    public Text results;

    public bool gameOver;

    public float rayLength;
    public LayerMask layermask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            timer -= Time.deltaTime;
            int updatedTime = (int)timer;
            timerText.text = updatedTime.ToString();
        }

        if(timer < 0 && !gameOver)
        {
            gameLoss();
            gameOver = true;
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !gameOver)
        {
            RaycastHit hit;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,  out hit, 1000))
            {
               
                string target = hit.collider.ToString();
                
                if (target  == "Brick(Clone) (UnityEngine.BoxCollider)")
                {
                    Destroy(hit.collider.gameObject);
                    score += 50;
                    scoreText.text = score.ToString();
                    
                }
               
                
                
            }

            
        }
    }
    public void hitBrick()
    {
        score += 100;
        scoreText.text = score.ToString();
    }

    public void hitQuestion()
    {
        score += 100;
        scoreText.text = score.ToString();
        coinAmount++;
        coinText.text = coinAmount.ToString();
    }

    public void gameWin()
    {
        results.text = "WORLD 1-0 COMPLETE";
        gameOver = true;
        reference.speed = 0;
    }

    public void gameLoss()
    {
        results.text = "GAME OVER";
        gameOver = true;
        reference.speed = 0;
    }


}
