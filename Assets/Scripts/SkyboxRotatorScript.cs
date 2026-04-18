using UnityEngine;

public class SkyboxRotatorScript : MonoBehaviour
{
    [SerializeField] Material skyboxMaterial;
    [SerializeField] float rotationSpeed = 1.0f;

    private void Awake()
    {
        if (skyboxMaterial == null)
        {
            Debug.LogError("Skybox material is not assigned.");
        }

    }

    public void RotateSkybox(float deltaTime)
    {
        if (skyboxMaterial != null)
        {
            float rotation = skyboxMaterial.GetFloat("_Rotation");
            rotation += rotationSpeed * deltaTime;
            skyboxMaterial.SetFloat("_Rotation", rotation);
        }
    }



    // Update is called once per frame
    void Update()
    {
        RotateSkybox(Time.deltaTime);
    }
}
