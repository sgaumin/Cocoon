using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Ennemy : MonoBehaviour
{
    public Transform[] Point;
    public Transform Player;

    private NavMeshAgent agent;
    private Transform LastPosPlayer;
    [SerializeField]
    private Transform goal;
    private float BaseSpeed;
    private float TimerDetect;
    private int WhichPoint = 0;
    private IAStates IAState = IAStates.Looking;

    // To prevent looping Doors animations
    private bool canInteractWithDoors;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = Point[WhichPoint];
        agent.destination = goal.position;
        BaseSpeed = agent.speed;

        canInteractWithDoors = true;
    }

    void Update()
    {
        switch (IAState)
        {
            case IAStates.Looking:
                //Debug.Log("LOKKAT");
                goal = Point[WhichPoint];
                agent.destination = goal.position;

                if (Vector3.Distance(transform.position, agent.destination) <= 3f)
                {
                    agent.speed = Mathf.Lerp(agent.speed, 1, 0.3f);
                }

                if (Vector3.Distance(transform.position, agent.destination) > 3f)
                {
                    agent.speed = BaseSpeed;
                }

                if (Vector3.Distance(transform.position, agent.destination) <= 1.2f)
                {
                    WhichPoint = WhichPoint + 1;
                    if (WhichPoint > Point.Length - 1) { WhichPoint = 0; }
                    goal = Point[WhichPoint];
                    agent.destination = goal.position;
                    agent.speed = BaseSpeed;
                }
                break;

            case IAStates.Detection:
                //Debug.Log("detect");
                agent.destination = gameObject.transform.position;
                if (TimerDetect > 1.5f)
                {
                    IAState = IAStates.Chasing;
                }

                break;
            case IAStates.Chasing:
                //Debug.Log("chase");
                goal = LastPosPlayer;
                agent.destination = goal.position;
                agent.speed = BaseSpeed * 1.5f;



                break;

            case IAStates.ChasingLastPos:
                //Debug.Log("chaseLASTTT");
                goal = LastPosPlayer;
                //agent.destination voir dans !hitcollider l 145 environ
                agent.speed = BaseSpeed * 2f;
                if (Vector3.Distance(transform.position, agent.destination) <= 1.2f) { IAState = IAStates.ReturnToPlace; }
                break;
            case IAStates.ReturnToPlace:
                IAState = IAStates.Looking;
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        #region DetectPlayer
        Vector3 targetDir = Player.position - transform.position;
        float Playerangle = Vector3.Angle(targetDir, transform.forward);
        float PlayerDistance = Vector3.Distance(transform.position, Player.position);

        if (PlayerDistance < 30)
        {
            if (Playerangle < 60 && Playerangle > -60)
            {
                RaycastHit hit;
                Vector3 raycastDir = Player.transform.position - transform.position;

                if (Physics.Raycast(transform.position, raycastDir, out hit, 30))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        LastPosPlayer = hit.transform;
                        if (IAState != IAStates.Chasing && IAState != IAStates.ChasingLastPos)
                        {
                            IAState = IAStates.Detection;
                        }

                        if (IAState == IAStates.Detection) { TimerDetect += Time.deltaTime; }

                        if (IAState == IAStates.Chasing) { LastPosPlayer = hit.transform; }
                    }

                    if (!hit.collider.gameObject.CompareTag("Player"))
                    {
                        if (IAState == IAStates.Detection) { TimerDetect = 0;/* a smoother*/ IAState = IAStates.Looking; }
                        if (IAState == IAStates.Chasing) { IAState = IAStates.ChasingLastPos; agent.destination = Player.position; }
                    }
                }
            }
        }
        if (PlayerDistance >= 30 && IAState == IAStates.Detection) { TimerDetect = 0;/* a smoother*/ IAState = IAStates.Looking; }
        if (Playerangle > 25 || Playerangle < -25 && IAState == IAStates.Detection) { TimerDetect = 0;/* a smoother*/ IAState = IAStates.Looking; }

        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        DoorController door = other.GetComponent<DoorController>();
        if (door != null)
        {
            if (!door.lokedByKey)
            {
                if (canInteractWithDoors)
                {
                    door.SetStatut();

                    canInteractWithDoors = false;
                    Animator animator = other.GetComponent<Animator>();
                    StartCoroutine(InteractWithDoors(animator));
                }
            }
        }
    }

    IEnumerator InteractWithDoors(Animator animator)
    {
        animator.SetTrigger("Door");

        yield return new WaitForSeconds(4f);
        canInteractWithDoors = true;
        yield break;
    }
}
