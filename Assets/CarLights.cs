using UnityEngine;

public class CarLights : MonoBehaviour
{
    public GameObject SpotLightL;
    public GameObject SpotLightR;
    public GameObject PointLightL;
    public GameObject PointLightR;

    // Метод для установки состояния фар
    public void SetLights(bool spotLightsOn, bool pointLightsOn)
    {
        if (SpotLightL != null)
        {
            SpotLightL.SetActive(spotLightsOn);
        }

        if (SpotLightR != null)
        {
            SpotLightR.SetActive(spotLightsOn);
        }

        if (PointLightL != null)
        {
            PointLightL.SetActive(pointLightsOn);
        }

        if (PointLightR != null)
        {
            PointLightR.SetActive(pointLightsOn);
        }
    }
}