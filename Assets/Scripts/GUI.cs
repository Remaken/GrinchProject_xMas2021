using UnityEngine;


public class GUI : MonoBehaviour
{
    public float tempsDepart = 0f;
    public float tempsLimite = 20f;
    public float tempsRestant ;


    public void Start()
    {
        tempsRestant = tempsLimite;
    }
    public void Update()
    {
        tempsRestant -= Time.deltaTime;
        tempsDepart += Time.deltaTime;
        if (tempsRestant <= 0)
        {
            tempsRestant = 0;
        }
    }
}
