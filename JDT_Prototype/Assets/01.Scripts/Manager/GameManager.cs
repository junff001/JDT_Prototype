using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    private static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                return null;
            }
            return _Instance;
        }

    }
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this);
        }
        PoolManager.CreatePool<EnemyBullet>(enemyBullet, transform, 100);
    }
    #endregion

    public enum Weapon
    {
        shotgun,
        Bazooca,
        DoubleHandgun
    }
    public enum Sub
    {
        none,
        bulletDivision,
        swiftAttack,
        guidedMissile,
        exploisionBullet
    }

    public Sub sub = Sub.none;
    public Weapon weapon = Weapon.shotgun;
    public Action OnReload;
    public AudioBoxSO audioBox;
    public AudioSource sfxSource;

    public GameObject enemyBullet;

    

    public Player player;
    public GameObject gun;
    public List<GameObject> weapons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            PoolManager.ResetPool();
            //PlayerMove.isDead = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameStart()
    {
        switch (weapon)
        {
            case Weapon.shotgun:
                gun.AddComponent<Shotgun>();
                break;

            case Weapon.Bazooca:
                gun.AddComponent<Bazooca>();
                break;

            case Weapon.DoubleHandgun:
                gun.AddComponent<DoubleHandgun>();
                break;
        }

        player.InitData();
    }

    public static void PlaySound(AudioSource source, AudioClip clip, float volume = 1f)
    {
        source.PlayOneShot(clip, volume);
    }

    public static void PlaySFX(AudioClip clip, float volume = 1f)
    {
        Instance.sfxSource.PlayOneShot(clip, volume);
    }


}
