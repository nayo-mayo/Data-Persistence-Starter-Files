using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
public class DataManager : MonoBehaviour
{
    public static DataManager DM;
    public User user;
    public string userName;
    public int userScore = 0;

    void Start()
    {
        DM = this;
        DM.user = loadScore();
        GameObject.Find("Score").GetComponent<TMP_Text>().text = $"Best Score: {DM.user.name}\t{DM.user.score.ToString()}";
        Debug.Log($"Start():\n{DM.user.name}\n{DM.user.score}");
        GameObject.Find("NameInput").GetComponent<TMP_InputField>().onEndEdit.AddListener(setName);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void QuitGame()
    {
        Debug.Log($"QuitGame()");
    }

    public User saveScore(User user)
    {
        if (user.score > DM.user.score)
        {
            File.WriteAllText(Application.persistentDataPath + "/saveScore.json", JsonUtility.ToJson(user));
            Debug.Log($"saveScore():\n{loadScore().name}\n{loadScore().score}");
        }
        return loadScore();
    }

    public User loadScore(){
        return File.Exists(Application.persistentDataPath + "/saveScore.json") ? (User)JsonUtility.FromJson<User>(File.ReadAllText(Application.persistentDataPath + "/saveScore.json")) : new User();
    }

    public class User{
        public User(){}
        public User(string name, int score){
            this.name = name;
            this.score = score;
        }
        public int score;
        public string name;
    }

    public void setName(string name){
        DM.userName = name;
    }

    public void setScore(int score){
        DM.userScore = score;
    }
}


