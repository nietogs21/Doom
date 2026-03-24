using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public GameObject blood;


    public void Damage(float damage, Quaternion rot)
    {
        health -= damage;
        AudioManager.instance.PlayEnemyDamage(); //se llama el scritp, la instancia y el metodo
        GameObject gunEffect = Instantiate(blood, transform.position, rot);
        Destroy(gunEffect , 0.5f);
    }

    public void EnemyDead()
    {
        if (health <= 0){
            EnemyManager.instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDead();
    }
}
