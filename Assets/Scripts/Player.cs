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
    
    
    private void Start()
    {
        Respawn();
        anim = GetComponent<Animator>();
        UpdateScoreText();
    }

    public void Update()
    {
        UpdateTimeText();
        
        if (_positionActuelle>=0 && _positionActuelle<placement.Length-1)
        {
            if (Input.GetKeyDown(KeyCode.D)||(Input.GetKeyDown(KeyCode.RightArrow)))
            { 
                _positionActuelle++;
                gameObject.GetComponent<SpriteRenderer>().flipX=false;           
                this.transform.position = placement[_positionActuelle].transform.position;
            }
        }

        if (_positionActuelle<placement.Length&&_positionActuelle>0)
        {
            if (Input.GetKeyDown(KeyCode.Q)||(Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                _positionActuelle--;
                gameObject.GetComponent<SpriteRenderer>().flipX=true;
                this.transform.position = placement[_positionActuelle].transform.position;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.G)||Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (objectObtained==true)
            {
                Debug.Log("je jette");
                ObjectDeposit();
            } 
            // TODO:Jetter object animation?
            Debug.Log("Throw");
        }
        
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
        timeText.text = "Time left : " + Mathf.Floor(GUI.tempsRestant).ToString() ;
    }
    
}
