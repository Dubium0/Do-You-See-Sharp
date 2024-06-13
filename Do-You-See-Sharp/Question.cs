using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class Question
{

    private string _questionText;

    private string _questionAnswer;

    public string QuestionText
    {

        get => _questionText;
    
    }

    public string QuestionAnswer
    {

        get => _questionAnswer;

    }



    public Question(string questionText, string questionAnswer)
    {
        _questionText = questionText;
        _questionAnswer = questionAnswer;
    }

}
