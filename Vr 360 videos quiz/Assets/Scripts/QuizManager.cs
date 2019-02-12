using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class QuizManager : MonoBehaviour
{

    //canvas where the question appears
    public GameObject questionCanvas;

    //question title
    public Text questionTitle;

    //score text
    public Text scoreText;

    //video player
    public VideoPlayer player;

    //url of the quiz
    public string quizUrl;

    //flag to indicate whether we are showing questions or not
    bool isShowingQuestions;

    //quiz
    Quiz quiz;

    //time (used to show questions)
    float elapsedTime = 0;

    //next question
    Question nextQuestion;

    //next question index
    int nextQuestionIndex;

    //total correct answers
    int totalCorrect = 0;

    //total questions
    int numQuestionsResponded = 0;

    // Use this for initialization
    IEnumerator Start()
    {

        //set initial score
        scoreText.text = "Score: 0 / 0";

        //pause quiz
        PauseQuiz();

        // hide question canvas
        questionCanvas.SetActive(false);

        // prepare quiz (load it from the web, parse it)

        // header for our request
        Dictionary<string, string> headers = new Dictionary<string, string>();
        

        // create WWW object
        WWW www = new WWW(quizUrl, null, headers);
        yield return www;

        if (www.error != null) Debug.LogError(www.error);
        print(www.text);
        // save json in our quiz
        quiz = JsonUtility.FromJson<Quiz>(www.text);

        //prepare next question
        PrepareNext();
    }



    // Update is called once per frame
    void Update()
    {

        //check that we should be showing questions
        if (!isShowingQuestions) return;

        //increase elapsed time
        elapsedTime += Time.deltaTime;

        // check time, if a question is due, show it!
        if (elapsedTime > nextQuestion.time)
        {
            //show question

            //1) show question canvas
            questionCanvas.SetActive(true);

            //2) set question title
            questionTitle.text = nextQuestion.title;

            //3) pause the quiz
            PauseQuiz();

        }
    }

    void PauseQuiz()
    {
        // video paused
        player.Pause();

        // no showing questions
        isShowingQuestions = false;
    }

    void ResumeQuiz()
    {
        // continue playing video
        player.Play();

        // continue measuring elapsed time
        isShowingQuestions = true;
    }

    void PrepareNext()
    {
        //setting the first value
        if (nextQuestion == null)
        {
            //set index to the start of the array
            nextQuestionIndex = 0;

            //save next question
            nextQuestion = quiz.questions[nextQuestionIndex];
        }
        else
        {
            //increase the question index
            nextQuestionIndex++;

            //check that there are more question left
            if (nextQuestionIndex < quiz.questions.Length)
            {
                //save next question
                nextQuestion = quiz.questions[nextQuestionIndex];
            }
            else
            {
                //we've completed the quiz
                print("Completed quiz");

                questionCanvas.SetActive(false);

                scoreText.text += "\nCOMPLETED QUIZ!";
                return;
            }
        }

        ResumeQuiz();
    }

    public void HandleAnswer(bool response)
    {

        print("responded : " + response + " the correct answer: " + nextQuestion.correct);

        //hide the question canvas
        questionCanvas.SetActive(false);

        //increase number of responded question
        numQuestionsResponded++;

        //check if the answer was correct
        if (response == nextQuestion.correct)
        {
            totalCorrect++;
            scoreText.text = "Correct!";
        }
        else
        {
            scoreText.text = "Wrong Answer!";
        }

        scoreText.text += "\nScore: " + totalCorrect + " / " + numQuestionsResponded;

        PrepareNext();


    }

}
