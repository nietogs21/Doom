using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController instance;
    private BoxCollider gunTrigger;

    public Weapons weapon;

    [Tooltip("Las capas en las que actua el rayo")]
    public LayerMask rayscastLayer;

    private bool canShoot; //para el colmdown

    public AudioSource audioSource;

    [Header("Enemy Test Materials")] //el header pone titulos
    public Material initialMaterial;
    public Material detectecMaterial;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        canShoot = true;
        SetTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    // Update is called once per frame
    public void SetTrigger()
    {
        gunTrigger.size = new Vector3(weapon.horizontalRange, weapon.verticalRange, weapon.range);
        gunTrigger.center = new Vector3(0, (0.5f * weapon.verticalRange - 1f), weapon.range * 0.5f);
    }

    private void OnTriggerEnter(Collider other) //en other se almacena el objeto que entra
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.GetComponent<Renderer>().material = detectecMaterial;
            EnemyManager.instance.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.GetComponent<Renderer>().material = initialMaterial;
            EnemyManager.instance.RemoveEnemy(enemy);
        }
    }

    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& canShoot == true)
        {
            audioSource.PlayOneShot(weapon.sound);

            foreach(Enemy enemy in EnemyManager.instance.enemiesInRange)
            {
                var dir = (enemy.transform.position - transform.position).normalized; //esto es para la direccion de los elementos

                RaycastHit hit;

                if(Physics.Raycast(transform.position, dir, out hit, weapon.range * 1.5f, rayscastLayer)) //se pone el 1,5 para que la bala agarre mas fuerza y si le de al objetivo
                {
                    if(hit.transform == enemy.transform)
                    {
                        Quaternion rot = Quaternion.LookRotation(-hit.normal);
                        enemy.Damage(weapon.damage, rot);
                    }

                    Debug.DrawRay(transform.position, dir * weapon.range, Color.green, 1f );
                }                                                                                           

            }
            StartCoroutine(CanFire(weapon.fireRate));

        }
    }
    

    IEnumerator CanFire(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
   
