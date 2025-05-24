using System.Collections;
using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject baseballPrefab;
    public float attackInterval = 2f;
    public float attackRange = 15f;
    [SerializeField] private float fireSpeed = 10f;
    [SerializeField] private float spawnOffset = 1.5f; // Distance in front of the drone to spawn the baseball
    [SerializeField] private AnimationCurve animateCurve;
    private float animationTime;
    private float animationDuration;
    private float lastAttackTime;
    [SerializeField] private float animationScaleFactor = 1f;
    [SerializeField] private float animationDurationScale = 1f;

    void Start()
    {
        TryAssignPlayer();
        TryKeyframe();
    }
    

    void Update()
    {
        // Try to find player if not assigned yet (e.g. player spawned after this drone)
        if (player == null)
        {
            TryAssignPlayer();
            return;
        }

        // Always look at the player
        transform.LookAt(player.transform);

        // Attack if in range and cooldown elapsed
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackInterval)
        {
            StartCoroutine(Attacking());
            lastAttackTime = Time.time;
        }
        
    }

    void TryAssignPlayer()
    {
        GameObject head = GameObject.Find("Head");
        if (head != null)
        {
            player = head;
        }
    }

    void TryKeyframe()
    {
        if (animateCurve == null || animateCurve.length == 0)
        {
            Debug.LogWarning("AnimationCurve is empty! Please assign keyframes.");
            animationDuration = 0f;
            return;
        }
        animationDuration = animateCurve.keys[animateCurve.length - 1].time;
    }

    void Attack()
    {
        Vector3 spawnPosition = transform.position + transform.forward * spawnOffset;

        //AUDIO EFFECTS
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.pitch = Random.Range(0.95f, 1.1f);
            audio.Play();
        }

        GameObject baseball = Instantiate(baseballPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rb = baseball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * fireSpeed;
        }
    }

    //Get the duration of the animation curve from the last key
    
    IEnumerator Charging()
    {
        //reset animation time to 0
        animationTime = 0f;
        Debug.Log(animationDuration);
        while (animationTime < animationDuration)
        {
            float tempSize = 1 + animateCurve.Evaluate(animationTime) * animationScaleFactor;
            this.transform.localScale = new Vector3(tempSize, tempSize, tempSize);
            // Update time of the animation curve to however much time has passed
            animationTime += Time.deltaTime * animationDurationScale;
            yield return null; //wait for the next frame
        }
        //reset scale
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator Attacking() {
        //Go through whole animation curve
        yield return StartCoroutine(Charging());
        //Then Attack
        Attack();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
