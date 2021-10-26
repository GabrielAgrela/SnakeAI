using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    public GameObject Snake;
    public GameObject SnakeSquare;

    public GameObject Apple;

    public int LoopNumber=0;

    public GameObject AppleInGame;

    private int LastPosX,LastPosY;
    public int FrameCount = 0; 
    public char CurrDirection = 'L';

    public int RandomX,RandomY;
    
    void Start()
    {
        SpawnApple();
    }

    public void SpawnApple()
    {
        bool Collision = false;
        //Spawn apple in random position
        RandomX=Random.Range(-10, 10);
        RandomY=Random.Range(-4, 12);

        for (int j =0; j < Snake.GetComponent<Snake>().BodyPositions.Count; j++)
        {
            if (RandomX == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.x && RandomY == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.y)
            {
                Collision = true;
            }
        }
        if (Collision == true)
        {
            SpawnApple();
        }else
        {
            AppleInGame = Instantiate(Apple, new Vector3(RandomX,RandomY , 0), Quaternion.identity); 
            //Spawn extra snake body and add it to the snake BodyPositions list
            Snake.GetComponent<Snake>().BodyPositions.Insert(0,Instantiate(SnakeSquare, new Vector3(LastPosX, LastPosY, 0), Quaternion.identity));
        }

        
    }

    void Update()
    {
        //Depending on the keyboard awsd set snake direction flag (Right, Left, Up, Down)
        /*if (Input.GetKeyDown("d"))
        {
            CurrDirection = 'R';
        }
        if (Input.GetKeyDown("a"))
        {
            CurrDirection = 'L';
        }
        if (Input.GetKeyDown("w"))
        {
            CurrDirection = 'U';
        }
        if (Input.GetKeyDown("s"))
        {
            CurrDirection = 'D';
        }*/

        //Move snake every 60 frames
        FrameCount++;
        if (FrameCount > 30)
        {
            //Find the best CurrDirection
            SetDirection();
            //Make sure CurrDirection doesnt collide with snake
            CorrectDirection();
            //First the head will lead the way, then the body squares
            MoveSnakeHead(CurrDirection);
            FrameCount=0;
        }
    }

    //TODO: Refactor this into different methods
    // Every if (random ...) works the same way, only thing changing is the direction up down left or right stuff
    void SetDirection()
    {
        
        LoopNumber=0;
        // if apple is on the right relative to the snake head
        if (RandomX > Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x)
        {
            bool Collision= false;
            // if theres a collision with another SnakeBody by moving right, collision = true
            for (int i =0; i < Snake.GetComponent<Snake>().BodyPositions.Count; i++)
            {   
                if (Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x +1 == Snake.GetComponent<Snake>().BodyPositions[i].transform.position.x && Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y == Snake.GetComponent<Snake>().BodyPositions[i].transform.position.y)
                {
                    Collision = true;
                }
            }
            // if theres no collision then move to the right
            if(Collision == false)
            {
                print("R");
                CurrDirection= 'R';
            }else
                // if there is a collision move up if the apple is up
                if (RandomY > Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y)
                {
                    CurrDirection= 'U';
                    print("1  U");
                }
                // or move down if the apple is down
                else    
                {
                    CurrDirection= 'D';
                    print("1 D");
                }
                    
        }else
        // if apple is on the top relative to the snake head
        if (RandomY > Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y)
        {
            bool Collision= false;
            for (int j =0; j < Snake.GetComponent<Snake>().BodyPositions.Count; j++)
            {
                if (Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y +1 == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.y && Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.x)
                {
                    Collision = true;
                }
            }
            if(Collision == false)
            {
                print("U");
                CurrDirection= 'U';
            }else
                if (RandomX > Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x)
                {
                    CurrDirection= 'R';
                    print("2 R");
                }
                else    
                {
                    CurrDirection= 'L';
                    print("2 L");
                }
        }else
        // if apple is on the left relative to the snake head
        if (RandomX < Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x)
        {
            bool Collision= false;
            for (int j =0; j < Snake.GetComponent<Snake>().BodyPositions.Count; j++)
            {
                if (Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x -1 == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.x && Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x +1 == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.x)
                {
                    Collision = true;
                }
            }
            if(Collision == false)
            {
                print("L");
                CurrDirection= 'L';
            }else
                if (RandomY > Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y)
                {
                    CurrDirection= 'U';
                    print("3 U");
                }
                else    
                {
                    CurrDirection= 'D';
                    print("3 D");
                }
        }else
        // if apple is on the bottom relative to the snake head
        if (RandomY < Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y)
        {
            bool Collision= false;
            for (int j =0; j < Snake.GetComponent<Snake>().BodyPositions.Count; j++)
            {
                if (Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y -1 == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.y && Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.x)
                {
                    Collision = true;
                }
            }
            if(Collision == false)
            {
                print("D");
                    CurrDirection= 'D';
            }else
                if (RandomX > Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x)
                {
                    CurrDirection= 'R';
                    print("4 R");
                }
                else    
                {
                    CurrDirection= 'L';
                    print("4 L");
                }
        }
    }

    void CorrectDirection()
    {
        int XDir=0;
        int YDir=0;
        bool Collision = false;
        // if looped >20 then its an infinite loop
        LoopNumber++;

        if(LoopNumber>20)
        {
            print("Stuck, game over");
            return;
        }
        // CurrDirection values to int
        if (CurrDirection== 'U')
            YDir=1;
        if (CurrDirection== 'D')
            YDir=-1;
        if (CurrDirection== 'L')
            XDir=-1;
        if (CurrDirection== 'R')
            XDir=1;
        // If theres a colision in the next frame, given the current CurrDirection then collision = true
        for (int j =0; j < Snake.GetComponent<Snake>().BodyPositions.Count; j++)
        {
            if (Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y+YDir == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.y && Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x+XDir == Snake.GetComponent<Snake>().BodyPositions[j].transform.position.x)
            {
                print("Colision detected - case 1"+CurrDirection);
                Collision = true;
            }
        }

        // If theres a colision change its CurrDirection so it doesn't collide (doesnt care where the apple is, that's SetDirection() job, this one just want's it to not to collide)
        if (Collision == true)
        {
            if (CurrDirection== 'U')
                CurrDirection= 'L';
            else if (CurrDirection== 'L')
                CurrDirection= 'D';
            else if (CurrDirection== 'D')
                CurrDirection= 'R';
            else if (CurrDirection== 'R')
                CurrDirection= 'U';
            print("Colision detected - case 2"+CurrDirection);
            CorrectDirection();
        }
    }

    void MoveSnakeHead(char Direction)
    {
        // Current positions from the head (which is the last SnakeSquare in the list), will be used by the next SnakeSquare
        LastPosX = (int)Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x;
        LastPosY = (int)Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y;
        Vector3 newPos;

        //Depending on the current direction, the new position of the next SnakeSquare will be 1 unit up/down/left/right
        switch(Direction) 
        {
            case 'U':
                newPos = new Vector3(Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x,Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y+1,0);
                break;
            case 'D':
                newPos = new Vector3(Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x,Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y-1,0);
                break;
            case 'L':
                newPos = new Vector3(Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x-1,Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y,0);
                break;
            case 'R':
                newPos = new Vector3(Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x+1,Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y,0);
                break;
            default:
                newPos = new Vector3(Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.x+1,Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position.y,0);
                break;
        }
        Snake.GetComponent<Snake>().BodyPositions[Snake.GetComponent<Snake>().BodyPositions.Count-1].transform.position = newPos;

        // Next SnakeSquares will take the previous SnakeSquares' positions
        MoveSnakeSquaresBody();
    }

    void MoveSnakeSquaresBody()
    {
        //Each SnakeSquare will move to the previous SnakeSquare's position
        for (int i = Snake.GetComponent<Snake>().BodyPositions.Count-2 ; i >= 0 ;i--)
                {
                    Vector3 newPos = new Vector3(LastPosX,LastPosY,0);
                    LastPosX = (int)Snake.GetComponent<Snake>().BodyPositions[i].transform.position.x;
                    LastPosY = (int)Snake.GetComponent<Snake>().BodyPositions[i].transform.position.y;
                    Snake.GetComponent<Snake>().BodyPositions[i].transform.position = newPos;
                }
    }
    public void DestroySnake()
    {
        GameObject[] SnakeSquares = GameObject.FindGameObjectsWithTag("Snake");
         for(int i=0; i< SnakeSquares.Length; i++)
         {
             Destroy(SnakeSquares[i]);
         }        
    }

}
