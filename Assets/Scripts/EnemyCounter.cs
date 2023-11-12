using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    public Text enemyCountText;
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCountText.text ="Inimigos restantes: " + enemies.Length.ToString();

        if(enemies.Length == 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        
    }
}
