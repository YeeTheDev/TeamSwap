using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TSwap.Attacks;
using TSwap.Data;

namespace TSwap.UI
{
    public class Ammo_UI : MonoBehaviour
    {
        [SerializeField] float flashDuration = 0.25f;
        [SerializeField] Image weaponAmmo;
        [SerializeField] Image transferAmmo;
        [SerializeField] Shooter shooter;

        private void OnEnable() => shooter.OnShoot += UpdateAmmoUI;
        private void OnDisable() => shooter.OnShoot -= UpdateAmmoUI;

        private void UpdateAmmoUI(ShootingData data)
        {
            Image image = data.isTransfer ? transferAmmo : weaponAmmo;
            image.fillAmount = (float)data.ammo / data.stats.Ammo;

            if (data.ammo <= 0) { StartCoroutine(PlayReload(image, data)); }
        }

        private IEnumerator PlayReload(Image image, ShootingData data)
        {
            float amountToFill = 0;
            while (image.fillAmount < 1)
            {
                yield return new WaitForEndOfFrame();

                amountToFill += Time.deltaTime;
                image.fillAmount = amountToFill / data.stats.ReloadTime;
            }

            StartCoroutine(Flash(image));
        }

        private IEnumerator Flash(Image image)
        {
            Color defaultColor = image.color;
            float colorTimer = 0;

            while (colorTimer < flashDuration)
            {
                yield return new WaitForEndOfFrame();

                image.color = Color.Lerp(Color.white, defaultColor, colorTimer / flashDuration);
                colorTimer += Time.deltaTime;
            }

            image.color = defaultColor;
        }
    }
}