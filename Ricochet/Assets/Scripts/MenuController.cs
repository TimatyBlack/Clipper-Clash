using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public GameObject restartButton;

    public GameObject finger;
    public GameObject fingerIdle;
    public GameObject fingerClicked;

    public CanvasGroup winMenu;
    public Animator transition;

    public Shooting shots;
    public RagdollOnOff[] enemies;

    public float delayToCheck = 2f;
    public float transitionTime = 0.5f;

    private void Start()
    {
        if(shots != null)
        shots.onShoot += StartCheckCoroutine;
    }


    private void Update()
    {
        finger.transform.position = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            fingerClicked.SetActive(true);
            fingerIdle.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            fingerClicked.SetActive(false);
            fingerIdle.SetActive(true);
        }
    }

    private void StartCheckCoroutine()
    {   
        if(shots.shotsLeft <= 0)
        StartCoroutine(CheckEnemies());
    }

    private IEnumerator CheckEnemies()
    {   
        yield return new WaitForSeconds(delayToCheck);

        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].isDead == false)
            {
                restartButton.SetActive(true);
                restartButton.transform.position = new Vector3(restartButton.transform.position.x,
                                                           -55,
                                                           restartButton.transform.position.z);

                restartButton.transform.DOMove(new Vector3(restartButton.transform.position.x,
                                                           55,
                                                           restartButton.transform.position.z),
                                                           1f);
                yield break;
            }
        }

        winMenu.gameObject.SetActive(true);
        winMenu.DOFade(1, 0.5f);
    }

    public void PlayNextSkip()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Restart()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void MainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void Quit()
    {
        Application.Quit(); 
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
