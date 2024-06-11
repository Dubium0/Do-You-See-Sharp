using System;

public class Context
{
	private string _storyText;
	private List<Question> _questions;
	private int _currentQuestionIndex;
	private List<People> _people;

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

    public Dictionary<string, string> GetAllInitialClaimsOfPeople()
    {
        Dictionary<string, string> initialClaims = new Dictionary<string, string>();
        foreach (var person in _people)
        {
            initialClaims.Add(person.GetName(), person.GetInitialClaim());
        }
        return initialClaims;
    }

    public Dictionary<string, string> GetAllInfoOfPeople()
    {
        Dictionary<string, string> initialClaims = new Dictionary<string, string>();
        foreach (var person in _people)
        {
            initialClaims.Add(person.GetName(), person.GetInfo());
        }
        return initialClaims;
    }

    public string GetStory()
	{

		return _storyText; 
	}
	public Question? GetCurrentQuestion()
	{
		if (_questions.Count > 0)
		{
			return _questions[_currentQuestionIndex];

		}
		else
		{
			return null;
		}
	}
    public bool TryToAnswerToCurrentQuestion(string answer)
    {
        // remove case sensitivity
        return answer.ToLower() == _questions[_currentQuestionIndex].GetAnswerText().ToLower();
    }

	//Get answer of the current question

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
