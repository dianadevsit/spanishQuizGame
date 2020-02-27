using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public QuestionDatabase questionDatabase;
    private int level;
    private QuestionSet currentQuestionSet;
    private Question currentQuestion;
    private int currentQuestionIndex;
    [SerializeField]
    private Transform questionPanel;
    [SerializeField]
    private Transform answerPanel;
    [SerializeField]
    private AudioClip correctSound, incorrectSound;
    private AudioSource audioSource;

    [SerializeField]
    private Transform scoreScreen, questionScreen;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreStats, scorePercentage;


    private int correctAnswers;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        level = PlayerPrefs.GetInt("level", 0);
        LoadQuestionSet();
        UseQuestionTemplate(currentQuestion.questionType);
    }

    void LoadQuestionSet()
    {
        currentQuestionSet = questionDatabase.GetQuestionSet(level);
        currentQuestion = currentQuestionSet.questions[0];
    }

    void ClearAnswers()
    {
        foreach (Transform buttons in answerPanel)
        {
            Destroy(buttons.gameObject);
        }
        
    }

    void UseQuestionTemplate(Question.QuestionType questionType)
    {
        for (int i = 0; i < questionPanel.childCount; i++)
        {
            questionPanel.GetChild(i).gameObject.SetActive(i == (int)questionType);
            if (i == (int)questionType)
            {
                questionPanel.GetChild(i).GetComponent<QuestionUI>().UpdateQuestionInfo(currentQuestion);
            }
        }
    }

    public void NextQuestionSet()
    {
        if (level < questionDatabase.questionSets.Length-1)
        {
            correctAnswers = 0;
            currentQuestionIndex = 0;
             level++;
            PlayerPrefs.SetInt("level", level);
            scoreScreen.gameObject.SetActive(false);
            questionScreen.gameObject.SetActive(true);
            LoadQuestionSet();
            UseQuestionTemplate(currentQuestion.questionType);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
        }
    }

    void NextQuestion()
    {
        if (currentQuestionIndex < currentQuestionSet.questions.Count-1)
        {
            currentQuestionIndex++;
            currentQuestion = currentQuestionSet.questions[currentQuestionIndex];
            UseQuestionTemplate(currentQuestion.questionType);
        }
        else
        {
            scoreScreen.gameObject.SetActive(true);
            questionScreen.gameObject.SetActive(false);
            scorePercentage.text = string.Format("Score:\n{0}%", (float)correctAnswers/(float)currentQuestionSet.questions.Count * 100);
            scoreStats.text = string.Format("Questions: {0}\nCorrect: {1}", currentQuestionSet.questions.Count, correctAnswers);
        }
    }

    public void CheckAnswer(string answer)
    {
        bool correct = false;
        foreach (string answerKey in currentQuestion.correctAnswerKeys)
        {
            if (answer == answerKey)
            {
                correctAnswers++;
                correct = true;
                Debug.Log("That's correct!");
                break;
            }
        }
        if (correct)
        {
            audioSource.PlayOneShot(correctSound);
        }
        else
        {
            audioSource.PlayOneShot(incorrectSound);
        }
        ClearAnswers();
        NextQuestion();
        
    }
}
