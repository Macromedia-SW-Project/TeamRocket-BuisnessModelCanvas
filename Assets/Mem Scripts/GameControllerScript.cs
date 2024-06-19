using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public const int columns = 4;
    public const int rows = 2;
    public const float Xspace = 4f;
    public const float Yspace = -5f;

    [SerializeField] private MainImageScript startObject;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreText;
    [SerializeField] private TextMesh attemptsText;

    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }

    private void Start()
    {
        if (startObject == null)
        {
            Debug.LogError("startObject is not assigned in the Inspector!");
            return;
        }

        if (images == null || images.Length == 0)
        {
            Debug.LogError("images array is not assigned or empty in the Inspector!");
            return;
        }

        if (scoreText == null)
        {
            Debug.LogError("scoreText is not assigned in the Inspector!");
            return;
        }

        if (attemptsText == null)
        {
            Debug.LogError("attemptsText is not assigned in the Inspector!");
            return;
        }

        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3 };
        locations = Randomiser(locations);

        Vector3 startPosition = startObject.transform.position;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                MainImageScript gameImage;
                if (i == 0 && j == 0)
                {
                    gameImage = startObject;
                }
                else
                {
                    gameImage = Instantiate(startObject) as MainImageScript;
                }

                if (gameImage == null)
                {
                    Debug.LogError("Failed to instantiate gameImage!");
                    continue;
                }

                int index = j * columns + i;
                int id = locations[index];
                gameImage.ChangeSprite(id, images[id]);

                float positionX = (Xspace * i) + startPosition.x;
                float positionY = (Yspace * j) + startPosition.y;

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z);
            }
        }
    }

    private MainImageScript firstOpen;
    private MainImageScript secondOpen;
    private int score = 0;
    private int attempts = 0;

    public bool canOpen
    {
        get { return secondOpen == null; }
    }

    public void imageOpened(MainImageScript startObject)
    {
        if (firstOpen == null)
        {
            firstOpen = startObject;
        }
        else
        {
            secondOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        if (firstOpen.spriteId == secondOpen.spriteId)
        {
            score++;
            scoreText.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstOpen.Close();
            secondOpen.Close();
        }

        attempts++;
        attemptsText.text = "Attempts: " + attempts;
        firstOpen = null;
        secondOpen = null;
    }

    public void Restart()
    {
        Debug.Log("Restart button clicked"); // Debug log for checking if the function is called
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}