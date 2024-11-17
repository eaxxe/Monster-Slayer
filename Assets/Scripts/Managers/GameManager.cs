using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // §±§à§Õ§á§Ú§ã§í§Ó§Ñ§Ö§Þ§ã§ñ §ß§Ñ §ã§à§Ò§í§ä§Ú§Ö §Ù§Ñ§Ô§â§å§Ù§Ü§Ú §ã§è§Ö§ß§í
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnemyFactory enemyFactory;

    void Start()
    {
        //Initialize();
    }

    private void Initialize()
    {
        //enemyFactory = FindObjectOfType<EnemyFactory>();
        //if (enemyFactory == null)
        //{
        //    Debug.LogError("Missing EnemyFactory in the scene");
        //    return;
        //}
        //CreateEnemies();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Initialize();
    }

    void CreateEnemies()
    {
        //List<Transform[]> patrolPointsList = new List<Transform[]>
        //{
        //    new Transform[] { GameObject.Find("PatrolPoint1").transform, GameObject.Find("PatrolPoint2").transform },
        //};

        //// §³§à§Ù§Õ§Ñ§Ö§Þ §Ó§â§Ñ§Ô§à§Ó §Ó §è§Ú§Ü§Ý§Ö, §ß§Ñ§Ù§ß§Ñ§é§Ñ§ñ §Ú§Þ §ä§à§é§Ü§Ú §á§Ñ§ä§â§å§Ý§Ú§â§à§Ó§Ñ§ß§Ú§ñ
        //for (int i = 0; i < patrolPointsList.Count; i++)
        //{
        //    Vector3 originalPosition = patrolPointsList[i][0].position;
        //    Vector3 modifiedPosition = new Vector3(originalPosition.x + 5, originalPosition.y, originalPosition.z);
        //    enemyFactory.CreateEnemy(modifiedPosition, patrolPointsList[i]);
        //}
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // §°§ä§á§Ú§ã§í§Ó§Ñ§Ö§Þ§ã§ñ §à§ä §ã§à§Ò§í§ä§Ú§ñ §Ù§Ñ§Ô§â§å§Ù§Ü§Ú §ã§è§Ö§ß§í
    }
}
