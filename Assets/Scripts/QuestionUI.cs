using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject answerButton;
    [SerializeField]
    private Transform answerPanel;
    public virtual void UpdateQuestionInfo(Question question)
    {
        question.answers = question.answers.OrderBy(answer => Random.value).ToArray();
        foreach(Answer answer in question.answers)
        {
            Transform answerButtonInstance = Instantiate(answerButton, answerPanel).transform;
            answerButtonInstance.GetComponent<AnswerButton>().SetAnswerButton(answer);
        }
    }
}
