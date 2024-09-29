using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public enum MathType
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
}
public class QuestionManager : MonoBehaviour
{
    Player player;
    Enemy enemy;
    public GameManager gameManager;

    public Text questionText;
    public InputField answerInput;
    public Text feedback;
    public Text newAbilityText;

    [SerializeField] private int correctAnswers = 0;
    [SerializeField] private int incorrectAnswers = 1;
    [SerializeField] private int currentQuestionDifficulty = 1;
    public int abilitiesGained;

    [SerializeField] private string currentQuestion;
    [SerializeField] public int currentAnswer;

    public MathType[] mathChoice;
    [SerializeField] MathType currentChoice;

    public Canvas canvas;
    public Dropdown choices;

    int number1 = 0;
    int number2 = 0;
    public int newAbilities = 0;
    public int abilityIndexUp;
    

   
    public int playerAnswer;

    public Canvas attackQuestionCanvas;
    [SerializeField] public bool canUseAbility = false;
    public bool hasAbility = false;

    
     [SerializeField]Image[] allAbilities = new Image[10];
   

    private void Awake()
    {
        
       
        canvas.GetComponent<Canvas>().enabled = true;
        mathChoice = (MathType[])System.Enum.GetValues(typeof(MathType));
        
    }
    void Start()
    {
         player = FindObjectOfType<Player>();
       enemy = FindObjectOfType<Enemy>();
        GenerateNewQuestion(choices.value + 1);
        
    }

    public void SubmitAnswer()
    {
        playerAnswer = int.Parse(answerInput.text);

        if (playerAnswer == currentAnswer)
        {
            feedback.text = "Correct!";
            correctAnswers++;
            incorrectAnswers = 1 ;
            AdjustDifficulty();
            GainAbility();
            player.DealDamage(enemy);
            canUseAbility = true;
            hasAbility = false;
            
           
        }
        else
        {
            feedback.text = "Incorrect!";
            incorrectAnswers++;
            correctAnswers = 0;
            AdjustDifficulty();
            GainAbility();
            Player player = FindObjectOfType<Player>();
            Enemy enemy = FindObjectOfType<Enemy>();
            enemy.DealDamage(player);
            hasAbility = true;

        }
        answerInput.text = "";
        GenerateNewQuestion(choices.value);
        
    }

    void GenerateNewQuestion(int choice)
    {
        switch (choice)
        {
            case 0:
                number1 = Random.Range(1, 10 * currentQuestionDifficulty);
                number2 = Random.Range(1, 10 * currentQuestionDifficulty);
                currentAnswer = number1 + number2;
                currentQuestion = "" + number1 + " + " + number2 + "?";
                questionText.text = currentQuestion;
                break;
            case 1:
                number1 = Random.Range(10 * currentQuestionDifficulty, 1);
                number2 = Random.Range(1, 10 * currentQuestionDifficulty);
                currentAnswer = number1 - number2;
                currentQuestion = "" + number1 + " - " + number2 + "?";
                questionText.text = currentQuestion;
                break;
            case 2:
                number1 = Random.Range(1, 10 * currentQuestionDifficulty);
                number2 = Random.Range(1, 10 * currentQuestionDifficulty);
                currentAnswer = number1 * number2;
                currentQuestion = "" + number1 + " x " + number2 + "?";
                questionText.text = currentQuestion;
                break;
            case 3:
                number1 = Random.Range(10 * currentQuestionDifficulty, 1);
                number2 = Random.Range(1, 10 * currentQuestionDifficulty);
                currentAnswer = number1 / number2;
                currentQuestion = "" + number1 + " + " + number2 + "?";
                questionText.text = currentQuestion;
                break;
            default:
                Debug.Log("Error");
                return;

        
        }
    }

    void AdjustDifficulty()
    {
        if (correctAnswers >= 3 )
        {
            currentQuestionDifficulty++;
            feedback.text += " Difficulty increased!";
            
        }
        else if (incorrectAnswers >= 3)
        {
            currentQuestionDifficulty = Mathf.Max(1, currentQuestionDifficulty - 1);
            feedback.text += " Difficulty decreased!";
        }
    }
    // Update is called once per frame
    public int Choice(MathType mathChoice)
    {
        switch(mathChoice)
        {
            case (MathType)0:
                return 0;
            case (MathType)1:
                return 1;

            case (MathType)2:
                return 2;
            case (MathType)3:
                return 3;
            default:
                return 4;

        }
    }

    public void GainAbility()
    {
        if (correctAnswers % 4 == 0 && hasAbility == false && incorrectAnswers % 2 != 0)
        {
            if (abilitiesGained == 3)
            {
                newAbilityText.text = "You can now block enemy attacks!";
            }
            else
            {
                newAbilityText.text = "You have gained an Ability! Answer and press " + (abilitiesGained + 1) + " to use";
            }
            abilitiesGained++;
            allAbilities[abilitiesGained - 1].gameObject.SetActive(true);
            
           
            
        }
       
    }
    public void closeCanvas()
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }

    public void PopulateList()
    {
       
    }

    public void UseAbility()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && abilitiesGained >= 1 && canUseAbility)
        {
            
                Debug.Log("Used Ability 1");
                player.DealDamage(enemy);
                player.animator.SetTrigger("Attack");
                canUseAbility = false;
            if (player.attackPower <= 10)
            {
                player.attackPower += 5;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && abilitiesGained >= 2 && canUseAbility)
        {

            Debug.Log("Used Ability 2");
            player.DealDamage(enemy);
            player.animator.ResetTrigger("Attack");
            player.animator.SetTrigger("Bow");
            canUseAbility = false;
            if (player.attackPower <= 22)
            {
                player.attackPower += 7;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && abilitiesGained >= 3 && canUseAbility)
        {
            Debug.Log("Used Ability 3");
            player.DealDamage(enemy);
            player.animator.ResetTrigger("Bow");
            player.animator.SetTrigger("Spell");
            canUseAbility = false;
            if (player.attackPower <= 29)
            {
                player.attackPower += 7;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && abilitiesGained >= 4 && canUseAbility)
        {
            Debug.Log("Used Ability 4");
            if (enemy.dealingDamage == true)
            {
                player.currentHealth += enemy.attackPower;
                
            }
            player.animator.ResetTrigger("Spell");
            player.animator.SetTrigger("Block");
            canUseAbility = false;
            if (player.attackPower <= 36)
            {
                player.attackPower += 7;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && abilitiesGained >= 5 && canUseAbility)
        {
            
            Debug.Log("Used Ability 5");
            player.DealDamage(enemy);
            player.animator.ResetTrigger("Block");
            player.animator.SetTrigger("Stab");
            canUseAbility = false;
            if (player.attackPower <= 43)
            {
                player.attackPower += 7;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && abilitiesGained >= 6 && canUseAbility)
        {
            
            Debug.Log("Used Ability 6");
            player.currentHealth += 20;
            player.animator.ResetTrigger("Stab");
            player.animator.SetTrigger("Heal");
            canUseAbility = false;
            if (player.attackPower <= 50)
            {
                player.attackPower += 7;
            }
            enemy.attackMax = 75;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7) && abilitiesGained >= 7 && canUseAbility)
        {
            
            Debug.Log("Used Ability 7");
            player.DealDamage(enemy);
            player.animator.ResetTrigger("Heal");
            player.animator.SetTrigger("Bolt");
            canUseAbility = false;
            if (player.attackPower <= 64)
            {
                player.attackPower += 7;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8) && abilitiesGained >= 8 && canUseAbility)
        {
            
            Debug.Log("Used Ability 8");
            player.DealDamage(enemy);
            player.animator.ResetTrigger("Bolt");
            player.animator.SetTrigger("Fire");
            canUseAbility = false;
            if (player.attackPower <= 71)
            {
                player.attackPower += 7;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9) && abilitiesGained >= 9 && canUseAbility)
        {
            
            Debug.Log("Used Ability 9");
            player.DealDamage(enemy);
            player.animator.ResetTrigger("Fire");
            player.animator.SetTrigger("Fireball");
            canUseAbility = false;
            if (player.attackPower <= 78)
            {
                player.attackPower += 7;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0) && abilitiesGained >= 10 && canUseAbility)
        {
           
            Debug.Log("Used Ability 10");
            player.DealDamage(enemy);
            player.animator.ResetTrigger("Fireball");
            player.animator.SetTrigger("Cleave");
            canUseAbility = false;
            if (player.attackPower <= 85)
            {
                player.attackPower += 7;
            }
        }
        

    }

    public void GenerateNewQuestionAbility()
    {

    }
        



    public void Update()
    {
        UseAbility();
        
    }

}
