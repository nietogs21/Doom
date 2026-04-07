
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAggro enemyAggro; //se declara la variable privada EnemyAggro de clase enemyAggro
    [HideInInspector] public bool isAttacking; //evitar que aparezca en el inspector cuando el enemigo ataca

    private void Start() //metodo privado que se ejecuta al comenzar el juego
    {
        enemyAggro = GetComponentInParent<EnemyAggro>(); //aqui se llama al componente que está dentro del script en el objeto padre del enemigo
        isAttacking = false; //el enemigo comienza el juego sin atacar al jugador
    }

    private void OnTriggerEnter(Collider other) //método privado para detectar lo que pasa al entrar al rango del enemigo
    {
        if (other.CompareTag("Target")) //si en el collider del ataque del enemigo detecta a un elemento con el tag de Target este lo marcará como objetivo y lo atacará
        {
            //enemyAggro.animator.SetBool("Attack", true);
            isAttacking = true; //el enemigo ataca si se cumple lo enunciado en el if
        }
    }

    private void OnTriggerExit(Collider other) //método privado para detectar lo que pasa al salir del rango del enemigo
    {
        if (other.CompareTag("Target")) //si en el collider del ataque del enemigo deja de detectar el elemento con el tag Target, dejará de atacar                                  
        {
            //enemyAggro.animator.SetBool("Attack", false);
            isAttacking = false; //el enemigo deja de atacar si se cumple lo enunciado en el if
        }
    }

}
