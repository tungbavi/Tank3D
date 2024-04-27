using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Text numberOfEnemiesText;
    [SerializeField] private Slider numberOfEnemies;
    
    private void Update()
    {
        numberOfEnemiesText.text = Spawn.instance.remainingEnemy.ToString();
        numberOfEnemies.value = Spawn.instance.remainingEnemy;
    }
}
