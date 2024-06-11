using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Question
{

    private string _questionText;

    private string _questionAnswer;

    public Question(string questionText, string questionAnswer)
    {
        _questionText = questionText;
        _questionAnswer = questionAnswer;
    }

    public string GetQuestionText()
    {

        return _questionText;
    }

    public string GetAnswerText()
    {

        return _questionAnswer;
    }
}
