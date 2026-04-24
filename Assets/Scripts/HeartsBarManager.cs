using System.Collections.Generic;
using UnityEngine;

public class HeartsBarManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHealth playerHealth;
    List<HealthHeart> hearts = new List<HealthHeart>();


    private void OnEnable()
    {
        PlayerHealth.OnPlayerHealthChanged += DrawHearts;
    }
    private void OnDisable()
    {
        PlayerHealth.OnPlayerHealthChanged -= DrawHearts;
    }
    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        
    }
    private void Update()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        }
   
    }
    private void Start()
    {
        DrawHearts();
    }
    public void DrawHearts()
    {
        ClearHearts();

        float maxHealthRemainder = (float)playerHealth.maxHealth % 2;
        int heartsToMake = (int)((playerHealth.maxHealth / 2) + maxHealthRemainder);
        for(int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }
    
    public void CreateEmptyHeart()
    {
        //instantiate heartPefabe, then set its parent to this game object
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        newHeart.transform.localScale = new Vector3(1, 1, 1);
        //grabs heart's component and sets its state to empty
        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        //adds heart to list
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }




}
