using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class Room : MonoBehaviour
{
    public Light2D mainLight;
    public Light2D[] subLights;

    public void TurnOn()
    {
        DOTween.To(() => mainLight.pointLightOuterRadius, x => mainLight.pointLightOuterRadius = x, 10, 0.5f).OnComplete(() =>
        {
            foreach (Light2D light in subLights) light.gameObject.SetActive(true);
        });
    }

    public void TurnOff()
    {
        DOTween.To(() => mainLight.pointLightOuterRadius, x => mainLight.pointLightOuterRadius = x, 0, 0.5f).OnComplete(() =>
        {
            foreach (Light2D light in subLights) light.gameObject.SetActive(false);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TurnOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TurnOff();
        }
    }
}
