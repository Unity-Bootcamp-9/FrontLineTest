using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VibrationTest : MonoBehaviour
{
    public Button vibeButton;
    public GameObject bulletPrefab;
    public Transform GunTip;
    private void Awake()
    {
        vibeButton = GetComponent<Button>();
        vibeButton.onClick.AddListener(Shoot);
    }
    private void Shoot()
    {
        Instantiate(bulletPrefab,GunTip.position, Quaternion.identity);
    }

    private void Vibrate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Handheld.Vibrate();
#endif
    }
}
