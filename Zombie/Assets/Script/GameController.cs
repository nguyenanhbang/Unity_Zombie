using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject menu;
    public Text txtPoint;


    private int currentPoint = 0;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        menu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetPoint(int point)
    {
        currentPoint++;
        txtPoint.text = "Zombie killed: " + currentPoint.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }
}
