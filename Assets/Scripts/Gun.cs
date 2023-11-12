using UnityEngine;


public class Gun : MonoBehaviour {

    Animator    animator;
    AudioSource audioSource;

    Transform tpsCam;

    ShotEffects shootEffects;

    Transform shotSpawn;
    Transform shellSpawn;

    [SerializeField] private AudioSource GunAudioSource;
    [SerializeField] private AudioClip[] GunAudioClip;

    private void Awake() {

        tpsCam     = transform.Find("shotSpawn");
        shotSpawn  = transform.Find("shotSpawn");
        shellSpawn = transform.Find("shellSpawn");

        shootEffects = GetComponent<ShotEffects>();

    }

    void Start() {
        
    }

    void Update() {
        
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            GunAudioSource.PlayOneShot(GunAudioClip[0]);
            shootEffects.MuzzleFlash(shotSpawn.position, shotSpawn.rotation);
            shootEffects.Shell(shellSpawn.position, shellSpawn.rotation);
            ShootRaycast();

        }

    }

    void ShootRaycast() {
        RaycastHit hitInfo;
        if(Physics.Raycast(tpsCam.transform.position, tpsCam.transform.forward, out hitInfo, Mathf.Infinity, LayerMask.GetMask("hittable"))) {
            EnemyAI enemy = hitInfo.transform.GetComponent<EnemyAI>();
            Debug.Log(hitInfo.collider.gameObject.name);
            if(enemy != null) {
                Debug.Log("Acertou um tanque");
                enemy.TakeDamage(10);
            }
        }
    }


   

}