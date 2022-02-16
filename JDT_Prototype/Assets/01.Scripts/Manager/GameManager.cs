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
            if(_Instance == null)
            {
                return null;
            }
            return _Instance;
        }
        
    }
    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public Action OnReload;
    public AudioBoxSO audioBox;
    public AudioSource sfxSource;

    public GameObject enemyBullet;

    private void Start()
    {
        PoolManager.CreatePool<EnemyBullet>(enemyBullet, transform, 100);
    }

    public static void PlaySound(AudioSource source, AudioClip clip, float volume = 1f)
    {
        source.PlayOneShot(clip, volume);
    }

    public static void PlaySFX(AudioClip clip, float volume = 1f)
    {
        Instance.sfxSource.PlayOneShot(clip, volume);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            PoolManager.ResetPool();
            PlayerMove.isDead = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
