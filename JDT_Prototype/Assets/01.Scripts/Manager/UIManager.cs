using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    #region ½Ì±ÛÅæ
    private static UIManager _Instance;
    public static UIManager Instance
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
        
    }
    #endregion

    [Header("¸ÞÀÎ")]
    public Button startBtn;
    public Button exitBtn;
    public Button selectWeapon;
    public Button selectSub;
    [Space(10)]
    [Header("¹«±â")]
    public GameObject weaponPanel;
    public Button shotgun;
    public Button cannon;
    public Button pistol;
    [Space(10)]
    [Header("¼­ºê")]
    public GameObject subPanel;
    public Button bulletDivision; // Åº¾ËºÐÈ¯
    public Button swiftAttack;
    public Button guidedMissile;
    public Button exploisionBullet;

    private void Start()
    {
        startBtn.onClick.AddListener(() => SceneManager.LoadScene("InGame"));
        exitBtn.onClick.AddListener(() => Application.Quit());

        selectWeapon.onClick.AddListener(() => weaponPanel.SetActive(true));
        selectSub.onClick.AddListener(() => subPanel.SetActive(true));

        shotgun.onClick.AddListener(() => GameManager.Instance.weapon = GameManager.Weapon.shotgun);
        cannon.onClick.AddListener(() => GameManager.Instance.weapon = GameManager.Weapon.Bazooca);
        pistol.onClick.AddListener(() => GameManager.Instance.weapon = GameManager.Weapon.DoubleHandgun);

        bulletDivision.onClick.AddListener(() => GameManager.Instance.sub = GameManager.Sub.bulletDivision);
        swiftAttack.onClick.AddListener(() => GameManager.Instance.sub = GameManager.Sub.swiftAttack);
        guidedMissile.onClick.AddListener(() => GameManager.Instance.sub = GameManager.Sub.guidedMissile);
        exploisionBullet.onClick.AddListener(() => GameManager.Instance.sub = GameManager.Sub.exploisionBullet);
    }


    public void Esc()
    {
        if (weaponPanel.activeSelf)
        {
            weaponPanel.SetActive(false);
        }
        else
        {
            subPanel.SetActive(false);
        }
    }
}
