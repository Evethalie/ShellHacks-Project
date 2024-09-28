using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public QuestionManager questionManager;
    public Text turnText;
    public Text resultText;
    public Canvas gameOver;

    private bool isPlayerTurn = true;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTurnText();
        gameOver.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (player.currentHealth <= 0)
        {
            gameOver.gameObject.SetActive(true);
            resultText.text = "You Lost! Game Over!";
            
        }
        else if (enemy.currentHealth <= 0) 
        {
             gameOver.gameObject.SetActive(true);
            resultText.text = "You Won! Thank you Hero!";
        }
    }
    public void OnSubmitAnswer()
    {
        if (isPlayerTurn)
        {
            questionManager.SubmitAnswer();
            EndTurn();
        }
    }

    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        UpdateTurnText();

        if (!isPlayerTurn)
        {
            EnemyTurn();
        }
    }

    void EnemyTurn()
    {
        bool willAttack = Random.value > 0.3f;
        if (willAttack)
        {
            enemy.DealDamage(player);
            resultText.text = "Enemy attacks!";

        }
        else
        {
            resultText.text = "Enemy is preparing....";
        }
        EndTurn();
    }

    void UpdateTurnText()
    {
        if (isPlayerTurn)
        {
            turnText.text = "Your Turn!";

        }
        else
        {
            turnText.text = "Enemy's Turn";
        }
    }
}
