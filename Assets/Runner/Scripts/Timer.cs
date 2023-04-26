using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    private float startTime;

    void Start()
    {
        // Get the start time when the game starts
        startTime = Time.time;
    }

    void Update()
    {
        // Calculate the time elapsed since the start time
        float elapsedTime = Time.time - startTime;

        // Format the time as minutes and seconds
        float minutes = Mathf.FloorToInt(elapsedTime / 60);
        float seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Update the timer text with the formatted time
    }

    public float GetElapsedTime()
    {
        // Return the total elapsed time since the game started
        return Time.time - startTime;
    }
}
