using UnityEngine;


public class GUI : MonoBehaviour
{
    public static float tempsDepart = 0f;
    public static float tempsLimite = 20f;
    public static float tempsRestant ;


    public void Start()
    {
        tempsRestant = tempsLimite;
    }
    public void Update()
    {
        Debug.Log(tempsRestant);
        tempsRestant -= Time.deltaTime;
        tempsDepart += Time.deltaTime;
        if (tempsRestant <= 0)
        {
            tempsRestant = 0;
        }
    }
}
