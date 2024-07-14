using System.Collections;
using TMPro;
using UnityEngine;

public class SingleShotGun : Weapon
{
    public PlayerController player;
    public GameObject bulletPrefab;
    public Transform gunTip;
    public Transform target;
    private int currentBulletCount;
    private bool isCoolingDown = false; 
    private bool isReloading = false; 
    public TextMeshProUGUI bulletUI;
    
    [Header ("DataField")]
    private const int bulletCount = 6;
    public float fireDelay = 0.5f;
    public float reloadTime = 2f; 
    public float attackDamage;

    private void OnEnable()
    {
        player = GetComponent<PlayerController>();
        gunTip = Camera.main.gameObject.transform.Find("Gun").transform.Find("GunTip").transform;
        target = Camera.main.gameObject.transform.Find("Target").transform;
        player.fireGun += OnFire;
        currentBulletCount = bulletCount;
    }

    private void OnDisable()
    {
        player.fireGun -= OnFire;
    }

    private void Update()
    {
        bulletUI.text = $"{currentBulletCount} / {bulletCount}";
    }

    protected override void OnFire()
    {
        if (isCoolingDown || isReloading)
        {
            return;
        }

        if (currentBulletCount > 0)
        {
            GameObject projectile = Instantiate(bulletPrefab, gunTip.position, Quaternion.identity);
            StartCoroutine(ShootBullet(projectile, target.position, 0.05f));
            currentBulletCount--;
            Handheld.Vibrate();


            Vector3 direction = (target.position - Camera.main.transform.position).normalized;

            RaycastHit hit;

            if(Physics.Raycast(Camera.main.gameObject.transform.position, direction, out hit, 20f))
            {
                Debug.Log("Hit1");
                if (hit.transform.TryGetComponent<Monster>(out Monster monster))
                {
                    monster.Hit();
                    Debug.Log("Hit2");
                }
            }

            StartCoroutine(CoolDown());
        }

        if (currentBulletCount == 0)
        {
            StartCoroutine(Reload());
        }
    }

    private bool isShooting = false;
    private IEnumerator ShootRepeat(GameObject bullet, Vector3 targetPosition, float duration)
    {
        while(isShooting)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = bullet.transform.position;

            while (elapsedTime < duration)
            {
                bullet.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                Debug.Log(elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            bullet.transform.position = targetPosition;
            Destroy(bullet);
        }
    }

    private IEnumerator ShootBullet(GameObject bullet, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = bullet.transform.position;

        while (elapsedTime < duration)
        {
            bullet.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            Debug.Log(elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bullet.transform.position = targetPosition;
        Destroy(bullet);
    }

    private IEnumerator CoolDown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(fireDelay);
        isCoolingDown = false;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentBulletCount = bulletCount;
        isReloading = false;
        Debug.Log("Reloaded!");
    }

}
