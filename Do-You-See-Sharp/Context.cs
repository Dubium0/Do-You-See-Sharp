using System;

public class Context
{
	private string _storyText;
	private List<Question> _questions;
	private int _currentQuestionIndex;
	private List<People> _people;

	private Question _lastQuestion;

	public Context(string storyText)
	{
		_storyText = storyText;
		_questions = new List<Question>();
		_people = new List<People>();
		_currentQuestionIndex = 0;

    }

	public void AddNewQuestion(Question question)
	{
		_questions.Add(question);
	}
	public void SetLastQuestion(Question lastQuestion) 
	{
        _lastQuestion = lastQuestion;
    }

    public Question GetLastQuestion()
    {
        return _lastQuestion;
    }

    public void AddNewPeople(People people)
	{
		_people.Add(people);
	}

    public List<People> GetAllPeople()
    {
        return _people;
    }
	public string GetHintFromSuspect(string name)
	{
		var suspect = _people.FirstOrDefault(p => p.GetName() == name);
		if (suspect != null && suspect.IsHintAcquired())
		{
			return suspect.GetExtraHint();
		}
		else
		{
			return "No hint available for this suspect.";

		}
	}

    public string GetStory()
	{
		return _storyText; 
	}
	public Question GetCurrentQuestion()
	{
		if (_questions.Count - 1 > _currentQuestionIndex)
		{
			return _questions[_currentQuestionIndex];

		}
		else
		{
			return _lastQuestion;
		}
	}
    public bool TryToAnswerToCurrentQuestion(string answer)
    {
        // remove case sensitivity
        return answer.ToLower() == _questions[_currentQuestionIndex].GetAnswerText().ToLower();
    }

    public bool MoveToNextQuestionIfAvailable()
	{
		
		if (_questions.Count - 1 > _currentQuestionIndex)
		{
			_currentQuestionIndex++;
			return true;

		}
		return false;

	}

	public List<People> GetPeople()
	{

		return _people;
	}

}
