using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] Transform currentSpawnPoint;
    [SerializeField] private GameObject playerPrefab;

    private GameObject currentPlayer;
    public bool hasDoubleJumpPowerup = false;
    public bool hasSwingPowerup = false;


    private void OnEnable()
    {
        SpawnPoint.SetSpawnPoint += SetSpawnPoint;
        BoundaryBox.RespawnPlayer += SpawnPlayer;
    }

    private void OnDisable()
    {
        SpawnPoint.SetSpawnPoint -= SetSpawnPoint;
        BoundaryBox.RespawnPlayer -= SpawnPlayer;
    }

    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        currentSpawnPoint = newSpawnPoint;
    }

    private void Awake()
    {
        if(playerPrefab == null)
        {
            Debug.LogError("Player prefab not assigned in PlayerSpawnManager");
        }
        
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        
        if(currentSpawnPoint == null)
        {
            SpawnPoint[] spawnPoints = GameObject.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                if(spawnPoints[i].isInitialSpawnPoint)
                {
                    currentSpawnPoint = spawnPoints[i].transform;
                    break;
                }
            }
        }

        SpawnPlayer();



    }

    public void SpawnPlayer()
    {
        if (currentSpawnPoint == null)
        {
            Debug.LogError("Current spawn point is not set. Cannot spawn player.");
            return;
        }
        else
        {
            if (currentPlayer == null)
            {
                currentPlayer = Instantiate(playerPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation);
                currentPlayer = GameObject.FindGameObjectWithTag("Player");
                currentPlayer.GetComponentInChildren<PlayerMotor>().unlockedDoubleJump = hasDoubleJumpPowerup;
                currentPlayer.GetComponentInChildren<PlayerMotor>().unlockedSwing = hasSwingPowerup;
            }
            else
            {
                currentPlayer.transform.position = currentSpawnPoint.position;
            }
        }
    }



}
