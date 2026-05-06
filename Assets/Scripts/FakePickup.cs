using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakePickup : MonoBehaviour
{
    public GameObject pickup;
    public float rotSpeed = 1f;
    public float bobFreq = 0.5f;
    public float bobAmp = 1;
    [SerializeField] AudioClip clip;
    private AudioSource soruce;
    private Vector3 initPositon;

    void Start()
    {
       soruce = GetComponent<AudioSource>();
       initPositon = pickup.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        { 
            pickup.SetActive(false);
            StartCoroutine(MainMenuAfterSound());
        }

    }
    IEnumerator MainMenuAfterSound()
    {
        soruce.Stop();
        soruce.PlayOneShot(clip);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        pickup.transform.Rotate(Vector3.left * rotSpeed * Time.deltaTime);
        pickup.transform.localPosition = new Vector3 (initPositon.x, Mathf.Sin(Time.time* bobFreq) * bobAmp + initPositon.y, initPositon.z);

    }
}
