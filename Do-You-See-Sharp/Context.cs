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

	public string GetStory()
	{

		return _storyText; 
	}
	public string GetCurrentQuestion()
	{
		if (_questions.Count > 0)
		{
			return _questions[_currentQuestionIndex].GetQuestionText();

		}
		else
		{
			return "THERE IS NO QUESTION";
		}
	}
	public bool TryToAnswerToCurrentQuestion(string answer)
	{

		return answer == _questions[_currentQuestionIndex].GetAnswerText();

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
