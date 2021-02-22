using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;


public class Game : MonoBehaviour
{

    public GameObject Rock;

    public GameObject Brick;

    public GameObject QuestionBox;

    public GameObject Stone;

    public Text timerText;
     float timer = 375;

    public Text scoreText;
    int score = 0;

    public Text coinText;
    int coinAmount = 0;
    


    public float rayLength;
    public LayerMask layermask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        int updatedTime = (int)timer;
        timerText.text = updatedTime.ToString();

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
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
                if(target == "Question(Clone) (UnityEngine.BoxCollider)")
                {
                    score += 200;
                    scoreText.text = score.ToString();
                    coinAmount++;
                    coinText.text = coinAmount.ToString();
                }
                
                
            }

            
        }
    }

   
   
}
