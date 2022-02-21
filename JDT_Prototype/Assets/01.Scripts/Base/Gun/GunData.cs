[System.Serializable]
public class GunData
{
    public float shootDelay;
    public int bulletCount;
    public int maxBulletCount { get; private set; }
    public float reloadTime;
    public bool canShoot = true;
}
