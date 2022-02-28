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

    public Action OnReload;
    public AudioBoxSO audioBox;
    public AudioSource sfxSource;

    public GameObject enemyBullet;
    
    public GameObject player;
    public Transform gunParentTrm;
    public Gun selectedGun { get; set; } = null;

    public Gun shotgun;
    public Gun bazooka;
    public Gun doubleHandgun;

    public enum GameState
    {
        CurrnetState,
        Playing,
        Pause,
        GameOver
    }

    private void Start()
    {
        GameStart();
    }

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
        Debug.Log(DataManager.weapon);

        switch (DataManager.weapon)
        {
            case DataManager.Weapon.shotgun:
                selectedGun = Instantiate(shotgun, gunParentTrm);
                break;

            case DataManager.Weapon.Bazooka:
                selectedGun = Instantiate(bazooka, gunParentTrm);
                break;

            case DataManager.Weapon.DoubleHandgun:
                selectedGun = Instantiate(doubleHandgun, gunParentTrm);
                break;
        }
        selectedGun.GetComponent<Gun>().InitData();
        player.GetComponent<PlayerAttack>().InitData();
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
