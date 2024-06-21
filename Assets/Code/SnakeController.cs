using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    // Settings
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 10;

    // References
    public GameObject BodyPrefab;
    public GameObject ApplePrefab; // Add this reference for the apple prefab
    public Vector2 boundary = new Vector2(4, 4); // Define the boundaries for apple spawning

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start() {
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        
        // Instantiate the first apple
        SpawnApple();
    }

    // Update is called once per frame
    void Update() {

        // Move forward
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        // Steer
        float steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        // Store position history
        PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in BodyParts) {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snake's path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;

            // Rotate body towards the point along the snake's path
            body.transform.LookAt(point);

            index++;
        }
    }

    private void GrowSnake() {
        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
        Debug.Log("Snake grew. Total body parts: " + BodyParts.Count); // Log body part addition
    }

    private void SpawnApple() {
        Vector3 newPosition = new Vector3(Random.Range(-boundary.x, boundary.x), 0.5f, Random.Range(-boundary.y, boundary.y));
        Instantiate(ApplePrefab, newPosition, Quaternion.identity);
        Debug.Log("New apple spawned at: " + newPosition); // Log apple spawning
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collision detected with: " + other.name); // Log collision

        if (other.CompareTag("Apple")) {
            Debug.Log("Apple eaten by snake!"); // Log apple eating event

            // Destroy the apple
            Destroy(other.gameObject);

            // Grow the snake
            GrowSnake();

            // Spawn a new apple
            SpawnApple();
        }
    }
}
