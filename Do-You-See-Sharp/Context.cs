using System;

public sealed class Context
{
	private string _storyText;
	private List<Question> _questions;
	private int _currentQuestionIndex;
	private List<People> _people;

	private Question _lastQuestion = new Question("",""); //dumb obj. at the beginning to avoid null ref.

	public Question LastQuestion
	{
		get { return _lastQuestion; }
		set { _lastQuestion = value; }
	}
    public IReadOnlyList<Question> Questions
	{
		get { return _questions; }
	}
    public IReadOnlyList<People> People
    {
        get { return _people; }
    }

    public string StoryText
	{
		get { return _storyText; }
	}


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

    public void AddNewPeople(People people)
	{
		_people.Add(people);
	}

	public string? GetHintFromSuspect(string name)
	{
		var suspect = _people.FirstOrDefault(p => p.Name == name);

		if (suspect == null)
		{
			Console.WriteLine("There is no such a suspect!");
			return null;
		}
		if (suspect.IsHintAcquired)
		{
            Console.WriteLine("Hint is already acquired for this suspect!");
            return null;
		}
		
		return suspect.ExtraHint;
		
		
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
        return answer.ToLower() == _questions[_currentQuestionIndex].QuestionAnswer.ToLower();
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

}
