
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
        
    }

    // Update is called once per frame
    public void SetTrigger()
    {
        gunTrigger.size = new Vector3(weapon.horizontalRange, weapon.verticalRange, weapon.range);
        gunTrigger.center = new Vector3(0, (0.5f * weapon.verticalRange - 1f), weapon.range * 0.5f);
    }
}
