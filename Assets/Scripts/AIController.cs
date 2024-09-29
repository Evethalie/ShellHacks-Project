using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

public class AIController : MonoBehaviour
{
    public float learningRate = 0.1f;
    public float discountFactor = 0.9f;
    public float epsilon = 0.1f;
    public int gridSizeX;
    public int gridSizeY;
    public Vector2 goalPosition;
    public float movementSpeed = 2f;

    private Dictionary<(int, int, int), float> QTable = new Dictionary<(int, int, int), float>();
    private Vector2Int currentPos;
    private int currentDirection;

    [SerializeField] bool isObstacle;
    [SerializeField] bool isGoal;

    public Tilemap tilemap;


    // Start is called before the first frame update
    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        gridSizeX = bounds.size.x;
        gridSizeY = bounds.size.y;
        currentPos = new Vector2Int(0, 0);
        currentDirection = 0;
        StartCoroutine(AIMoveRoutine());
        InitializeQtable();
    }

    private void InitializeQtable()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                for (int direction = 0; direction < 4; direction++)
                {
                    QTable[(x,y,direction)] = 0f;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Random.value < epsilon)
        {
            TakeRandomAction();
        }
        else
        {
            TakeBestAction();

        }
        float reward = GetReward();
        MoveAI();
    }

   private void TakeRandomAction()
    {
        currentDirection = UnityEngine.Random.Range(0, 4);
    }

    void TakeBestAction()
    {
        float maxQValue = float.MinValue;
        int bestAction = currentDirection;

        for (int action = 0; action < 4; action++)
        {
            float qValue = QTable[(currentPos.x,currentPos.y,action)];
            if (qValue > maxQValue)
            {
                maxQValue = qValue;
                bestAction = action;
            }
        }
        currentDirection = bestAction;
    }

    float GetReward()
    {
        if (isGoal == true)
        {
            return 1f;
        }
        else if (isObstacle == true)
        {
            return -1f;
        }
        return -0.01f;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            isObstacle = true;
            isGoal = false;
        }
        if (col.gameObject.tag == "Goal")
        {
            isGoal = true;
            isObstacle = false;
        }

        
    }

    void UpdateQTable(float reward)
    {
        float maxFutureQValue = float.MinValue;

        for (int action = 0;action < 4; action++)
        {
            if (QTable.ContainsKey((currentPos.x,currentPos.y,action)))
            {
                maxFutureQValue = Mathf.Max(maxFutureQValue, QTable[(currentPos.x,currentPos.y,action)]);
            }
        }
        QTable[(currentPos.x, currentPos.y,currentDirection)] = (1 - learningRate) * QTable[(currentPos.x, currentPos.y,currentDirection)] + learningRate * (reward + discountFactor * maxFutureQValue);
    }

    void MoveAI()
    {
        switch(currentDirection)
        {
            case 0:
                currentPos.y += 1;
                break;
            case 1:
                currentPos.x += 1;
                break;
            case 2:
                currentPos.y -= 1;
                break;
            case 3: 
                currentPos.x -= 1;
                break;
        }
        
        transform.position = new Vector2(currentPos.x, currentPos.y);

    }

    IEnumerator AIMoveRoutine()
    {
        while(true)
        {
            TakeBestAction();
            MoveAI();

            yield return new WaitForSeconds(5);
        }
    }

    
}
