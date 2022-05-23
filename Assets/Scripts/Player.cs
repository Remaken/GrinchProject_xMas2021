using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool objectObtained = false;
    public int life=3;
    public int score = 0;
    public GameObject[] placement;
    public GameObject[] indicators;
    private int _positionActuelle = 0;
    private bool gift = false;
    private Animator anim;
    public Text scoreText;
    public Text timeText;
    
    public GUI _gui;



    private void OnEnable()
    {
        //InputManager.MovementEvent += PlayerMovement;
        InputManager.MoveRight += GoRight;
        InputManager.MoveLeft += GoLeft;
        InputManager.DropItem += DropItem;
    }
  


    private void OnDisable()
    {
        //InputManager.MovementEvent -= PlayerMovement;
        InputManager.MoveRight -= GoRight;
        InputManager.MoveLeft -= GoLeft;
        InputManager.DropItem -= DropItem;
    }
    private void Start()
    {
        _gui = FindObjectOfType<GUI>();
        Respawn();
        anim = GetComponent<Animator>();
        UpdateScoreText();
    }

    public void Update()
    {
        UpdateTimeText();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         
        if (other.gameObject.CompareTag("Gift"))
        {
            if (objectObtained==false)
            {
                anim.SetTrigger("Catch");
                gift = true;
                objectObtained = true;
                indicators[0].SetActive(true);
                Destroy(other.gameObject);
            }
            other.GetComponentInParent<ObjectSpawner>().canSpawn = true;
        }

        if (other.gameObject.CompareTag("Box"))
        {
            if (objectObtained==false)
            {
                
                anim.SetTrigger("Catch");
                objectObtained = true;
                indicators[1].SetActive(true);
                Destroy(other.gameObject);
            }
            other.GetComponentInParent<ObjectSpawner>().canSpawn = true;
        }
        if (other.gameObject.CompareTag("Ice"))
        {
            life--;
            Destroy(other.gameObject);
            other.GetComponentInParent<ObjectSpawner>().canSpawn = true;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D Object)
    {

        if (Object.gameObject.CompareTag("OffMap"))
        {
            life--;
            Respawn();
        }

        if (Object.gameObject.CompareTag("Storage"))
        {
            if ((objectObtained == true)&&(gift==true))
            {
                score++;
                ObjectDeposit();
                gift = false;
               // TODO: anim.SetTrigger("Deposit");
                UpdateScoreText();
            }
            else if (objectObtained == true)
            {
                // TODO:Animation coffre
                score--;
                ObjectDeposit();
               // TODO: anim.SetTrigger("Deposit");
                UpdateScoreText();
            }
        }
        
    }

    private void Respawn()
    {
        _positionActuelle = 0;
        this.transform.position = placement[_positionActuelle].transform.position;
        
    }
    private void ObjectDeposit()
    {
        objectObtained = false;
        indicators[0].SetActive(false);
        indicators[1].SetActive(false);
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score : " + score;
    }

    private void UpdateTimeText()
    {
        timeText.text = "Time left : " + Mathf.Floor(_gui.tempsRestant).ToString() ;
    }

    /*private void PlayerMovement(float inputValue)
    {
        print(inputValue);
        if (_positionActuelle>=0 && _positionActuelle<placement.Length-1)
        {
            if (inputValue > 0)
            { 
                _positionActuelle++;
                gameObject.GetComponent<SpriteRenderer>().flipX=false;           
                this.transform.position = placement[_positionActuelle].transform.position;
            }
        }

        if (_positionActuelle<placement.Length&&_positionActuelle>0)
        {
            if (inputValue < 0)
            {
                _positionActuelle--;
                gameObject.GetComponent<SpriteRenderer>().flipX=true;
                this.transform.position = placement[_positionActuelle].transform.position;
            }
        }
    }*/

    private void GoLeft()
    {
        if (_positionActuelle < placement.Length && _positionActuelle > 0)
        {
            _positionActuelle--;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            this.transform.position = placement[_positionActuelle].transform.position;
        }
    }
    
    private void GoRight()
    {if (_positionActuelle>=0 && _positionActuelle<placement.Length-1)
        {
                _positionActuelle++;
                gameObject.GetComponent<SpriteRenderer>().flipX=false;
                this.transform.position = placement[_positionActuelle].transform.position;
        }
    }
    private void DropItem()
    {
        if (objectObtained==true)
        {
            ObjectDeposit();
        } 
        // TODO:Jetter object animation?
    }
}
