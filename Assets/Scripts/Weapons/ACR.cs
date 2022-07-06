using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ACR : MonoBehaviour
{
    private Animation anim; //анимация
    private AudioSource _AudioSource;

    public float range = 100f; //Максимальное расстояние полета пули
    public float speed = 200f;
    public int bulletPerMag = 30; //патронов в магазине
    public int bulletLeft; //количество оставшихся патронов
    public int currentBullets; //текущие патроны в магазине
    public float damage = 20f;
    public float waitReload = 2.8f;
    private float wReload = 2f;
    public float spreadFactor = 0.1f;
    private int bonusesReload = 0; //количество бонусов перезарядки
    private int bonusesDamage = 0; //количество бонусов урона
    public bool bReload = false;
    public bool bDamage = false;

    public Text ammoText;
    public Transform shootPoint; //точка, из которой пуля покидает оружие
    public GameObject hitParticles;
    public GameObject hitParticles2;
    public GameObject bulletImpact;
    public GameObject bullet;

    Vector3 bulletForce; //сила выстрела
    public WeaponSway weaponSway;

    public float fireRate = 0.1f; //задержка между каждым выстрелом
    public bool reloading = false;
    public AudioClip shootSound; //звук выстрела
    public AudioClip reloadSound; //звук перезарядки

    float fireTimer; //счетчик времени задержки

    private Vector3 originalPosition;
    public Vector3 aimPosition;
    public float aimSpeed;

    // Start is called before the first frame update
    void Start()
    {
        weaponSway = GetComponent<WeaponSway>();
        anim = GetComponent<Animation>();
        _AudioSource = GetComponent<AudioSource>();
        currentBullets = bulletPerMag;
        originalPosition = transform.localPosition;
    }
    IEnumerator BReload(float bonusReload)
    {
        bonusesReload++; if(bonusesReload >= 2) { bReload = false; }
        if (bReload)
        { yield break; }
        bReload = true;
        anim["reload"].speed = bonusReload;
        waitReload = 0.8f; 
        yield return new WaitForSeconds(10f);
        if (bonusesReload >= 2) { bonusesReload = 0; yield break; } else bonusesReload = 0;
        anim["reload"].speed = 1f;
        waitReload = 2.8f;
        bReload = false;
    }
    public void BonusReload(float bonusReload)
    {
        StartCoroutine(BReload(bonusReload));

    }

    IEnumerator BDamage(float bonusDamage)
    { 
        bonusesDamage++; if (bonusesDamage >= 2) { bDamage = false; }
        if (bDamage)
        { yield break; }      
        bDamage = true;
        damage = 1000;
        yield return new WaitForSeconds(10f);
        if (bonusesDamage >= 2) { bonusesDamage = 0; yield break; } else bonusesDamage = 0;
        damage = 20;
        bDamage = false;
    }
    public void BonusDamage(float bonusDamage)
    {

        StartCoroutine(BDamage(bonusDamage));

    }
    IEnumerator DoReload()
    {
        if (reloading)
        { yield break; }
        PlayReloadSound();

        anim.Play("reload");
        reloading = true;
        yield return new WaitForSeconds(waitReload);
        Reload();
        reloading = false;
    }


    private void AimDownSighth()
    {
        if (Input.GetButton("Fire2") && reloading == false)
        {
            spreadFactor = 0f;
            weaponSway.amount = 0.01f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * aimSpeed);
        }
        else
        {
            spreadFactor = 0.1f;
            weaponSway.amount = 0.1f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * aimSpeed);
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateAmmoText();
        if (Input.GetKey("r"))
        {
            if (currentBullets < bulletPerMag && bulletLeft > 0)
            {
                StartCoroutine(DoReload());
            }
        }

        if (Input.GetButton("Fire1") && reloading == false)
        {
            if (currentBullets > 0)
            {
                Fire(); //выполнить функцию огня, если нажать или удерживать левую кнопку мыши
            }
            else if (currentBullets <= 0 && bulletLeft > 0) //если в обойме нет патронов, а в запасе есть
            {
                StartCoroutine(DoReload());
            }
            else if (bulletLeft <= 0)  //если патронов в запасе нет
                return;

        }


        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime; //добавить в таймер
        }

        AimDownSighth();
    }

    private void Fire()
    {
        //Если патронов в обойме нет
        if (fireTimer < fireRate || currentBullets <= 0)
        {
            return;
        }

        RaycastHit hit; //луч

        Vector3 shootDirection = shootPoint.transform.forward;

        shootDirection.y += Random.Range(-spreadFactor, spreadFactor);
        shootDirection.x += Random.Range(-spreadFactor, spreadFactor);


        if (Physics.Raycast(shootPoint.position, shootDirection, out hit, range))
        {
            float x = shootPoint.transform.position.x - transform.position.x;
            float y = shootPoint.transform.position.y - transform.position.y;
            float z = shootPoint.transform.position.z - transform.position.z;

            bulletForce = new Vector3(x, y, z);
            GameObject createdBullet = Instantiate(bullet, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            createdBullet.GetComponent<Rigidbody>().AddForce(bulletForce * speed, ForceMode.Impulse);
            //Debug.Log(hit.transform.name); //нахождение объектов

            //transform.Translate(shootDirection * Time.deltaTime * speed, Space.World);
            if (hit.transform.GetComponent<HealthController>())
            {
                hit.transform.GetComponent<HealthController>().ApplyDamage(damage);
            }
            if (hit.transform.GetComponent<ZombieEnemy>())
            {
                GameObject hitParticleEffect = Instantiate(hitParticles2, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                hitParticleEffect.transform.SetParent(hit.transform);
                Destroy(hitParticleEffect, 10f);
                hit.transform.GetComponent<ZombieEnemy>().ApplyDamage(damage);
            }
            else
            {
                GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)); //Выравнивание эффекта на плоскость
                GameObject bulletsHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                hitParticleEffect.transform.SetParent(hit.transform);
                Destroy(hitParticleEffect, 10f);
                Destroy(bulletsHole, 10f);
            }
        }

        anim.CrossFadeQueued("fire", 0.08f, QueueMode.PlayNow);
        PlayShootSound(); //звук выстрела
        currentBullets--; //-1 патрон после выстрела
        UpdateAmmoText();
        fireTimer = 0.0f; //Сброс таймера
    }

    private void Reload()
    {

        if (bulletLeft < 0) return;

        int bulletsToLoad = bulletPerMag - currentBullets;
        int bulletxToDeduct = (bulletLeft >= bulletsToLoad) ? bulletsToLoad : bulletLeft;

        bulletLeft -= bulletxToDeduct;
        currentBullets += bulletxToDeduct;

        UpdateAmmoText();
    }

    private void PlayShootSound()
    {
        _AudioSource.PlayOneShot(shootSound);
    }

    private void PlayReloadSound()
    {

        _AudioSource.clip = reloadSound;
        _AudioSource.Play();
    }

    private void UpdateAmmoText()
    {
        ammoText.text = currentBullets + " / " + bulletLeft;
    }
}

