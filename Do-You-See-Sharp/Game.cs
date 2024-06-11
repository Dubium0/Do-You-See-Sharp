using System;
using System.Xml.Linq;

public class Game
{

	private Context _context;
	private List<string> _acquiredHints = new List<string>();
	private int _currentPoint = 100;
	private string _culpritName;
	private string _lastQuestionTxt = "Who is the culprit?";
	public enum PowerUps
	{
		SKIP,
		HALF
	}

	private enum GameState
	{	
		INITIAL_STATE,
		FINAL_STATE,
		FINISH
	}

	private GameState _gameState;	
	public Game()
	{
		_context = new Context ("Olay, tarihi ve sanatsal eserlerle ünlü bir müzede geçmektedir." +
			" Müzenin en değerli tablolarından biri gizemli bir şekilde yok olmuştur. " +
			"Güvenlik kameraları, olay günü altı farklı şüphelinin tablonun bulunduğu galeriden geçtiğini kaydetmiştir. " +
			"Müze kapalıyken gerçekleşen bu olay, galerinin içindeki güvenlik sisteminin de devre dışı bırakıldığı bir zaman diliminde olmuştur. " +
			"Olayın ardından müze çalışanları birbirlerini suçlayarak ifadeler vermişlerdir ve bazı ifadelerde çelişkiler bulunmaktadır.\n");



		_context.AddNewPeople(new People("Emre Aslan",
			"35 yaşında, kısa siyah saçlı ve gözlüklü bir adam", 
			"Müze temizlik personeli. Olay günü gece vardiyasındaydı ve sadece kendi alanlarında çalıştığını iddia ediyor.",
			"Müze yönetimiyle iyi ilişkileri var ve anahtar kart erişimine sahip. Çalışma saatlerinin dışında müzede kaldığına dair güvenlik kayıtlarında bazı belirsizlikler var."));

		_context.AddNewPeople(new People("Leyla Demir",
			"29 yaşında, uzun kıvırcık kızıl saçları ve yeşil gözleri olan bir kadın", 
			"Müze kafesinde barista. Hiçbir zaman sanat galerilerine girmediğini, çünkü sanattan anlamadığını söylüyor.",
			"Müzenin güvenlik şifresini biliyor çünkü geç saatlerde bazen müze içinde kalmak zorunda kalıyor. Eski sevgilisi müzede güvenlik teknolojileri üzerine çalışmış ve ona sistemler hakkında bazı teknik detaylar öğretmiş."));

		_context.AddNewPeople(new People("Ahmet Yıldız", 
			"43 yaşında, kel, uzun boylu ve geniş omuzlu bir adam",
			"Müzenin güvenlik görevlisi. O gece her şeyin normal olduğunu ve herhangi bir şüpheli hareket görmediğini belirtiyor.", 
			"Eski bir polis memuru ve kilitli kapıları açma konusunda uzman. Müze içindeki değerli eserlerin güvenlik protokollerini yakından biliyor ve yeni güvenlik önlemleri alınmasını önermiş."));

		_context.AddNewPeople(new People("Nur Toprak",
			"50 yaşında, gri saçlı ve mavi gözlü zarif bir kadın", 
			"Müzenin küratörü. Olay günü eve erken gittiğini ve hiçbir şeyden haberi olmadığını iddia ediyor.",
			"Müzenin her köşesini çok iyi bilir ve nadiren kullanılan gizli geçitler hakkında bilgi sahibidir. Yakın zamanda yapılan bir sergi düzenlemesi sırasında, eski ve değerli eserlerin konumları hakkında değişiklik yapılmasını istemişti."));

		_context.AddNewPeople(new People("Barış Kaya", 
			"22 yaşında, dalgalı sarı saçlı ve atletik yapıda genç bir adam",
			"Müze rehberi. Tur gruplarına liderlik ettiğini, ancak o gün hiç tur olmadığını belirtiyor.", 
			"Gizli kamera sistemlerinin yerlerini biliyor. Rehberlik yaptığı sırada özellikle değerli sanat eserlerinin bulunduğu alanlarda tur gruplarına ekstra bilgi vermekten kaçınıyor."));

		_context.AddNewPeople(new People("Seda Çınar",
			"27 yaşında, düz siyah saçlı ve kahverengi gözlü bir kadın", 
			"Serbest zamanlı sanatçı ve sık sık müzede çalışıyor. O gece müzede olmadığını iddia ediyor.",
			"Müzede sergilenen eserler hakkında derin bilgilere sahip ve bu eserlerin değerlerini iyi biliyor. Müzede sergilenmeyen ancak depolarda saklanan eski sanat eserleri hakkında bilgi sahibi olduğu, bu eserlere kimseye söylemeden erişim sağladığına dair dedikodular var."));



		_context.AddNewQuestion(new Question("Eseri baskalari da varken calmak cok riskli, herkes ciktiktan sonra calinmis olmali. Olayın gerçekleştiği gün galeriden çıkan son kişi kim olabilir?",
			"Leyla Demir"));

        _context.AddNewQuestion(new Question("Eseri calan kisi, degerini cok iyi biliyor olmali, bu eserin degerini en iyi kim bilebilir?",
			"Nur Toprak"));

        _context.AddNewQuestion(new Question("Eser kilitli bir bolgede tutuldugu halde hirsiz bir sekilde eseri calmayi basarmis, bunu kim yapabilir?",
			"Ahmet Yıldız"));

        _context.AddNewQuestion(new Question("Müzede sergilenen eserler hakkında derin bilgilere sahip olan bir kişinin, değerli bir tablonun kaybolmasında bir çıkarı olabilir. Bu profili hangi şüpheli karşılar?", 
			"Seda Çınar"));

		_culpritName = "Leyla Demir";

		_context.SetLastQuestion(new Question(_lastQuestionTxt, _culpritName));

		DisplayStory();

    }



	public void UsePowerUp(PowerUps powerUp)
	{
		switch (powerUp)
		{
			case PowerUps.SKIP:
				_useSkipPowerUp();
				break;

			case PowerUps.HALF: 
				_useHalfPowerUp();
				break;

		}

	}

	private List<People> _shufflePeople()
	{
        Random _random = new Random();
        string correctAnswer = _context.GetCurrentQuestion().GetAnswerText();
        List<People> shuffledPeople = _context.GetAllPeople();

        var halfSize = shuffledPeople.Count / 2;

        // Shuffle the list randomly
        shuffledPeople = shuffledPeople.OrderBy(x => _random.Next()).ToList();

        People correctPerson = shuffledPeople.Find(p => p.GetName() == correctAnswer);

        if (shuffledPeople.IndexOf(correctPerson) >= halfSize)
        {
            shuffledPeople.Remove(correctPerson);
            shuffledPeople.Insert(0, correctPerson);
        }

        return shuffledPeople;
    }
	private void _useHalfPowerUp()
	{
		if (_gameState != GameState.FINAL_STATE) {
			Console.WriteLine("Bu guclendırme yalnızca son soruda kullanılabılır!");
			return;
		
		}

		bool hasEnoughMoney = _payIfPossible(80);
		if (hasEnoughMoney)
		{
            
            List<People> shuffledPeople = _shufflePeople();
            List<string> names = shuffledPeople.Take(shuffledPeople.Count / 2).Select(p => p.GetName()).ToList();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Half Option PowerUp is used.\nRemaining Options are : \n");
            Console.ResetColor();

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
		else
		{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You don't have enough points to use Half Option PowerUp!");
            Console.ResetColor();

        }

    }
    private void _useSkipPowerUp()
    {
		if (_gameState != GameState.INITIAL_STATE) {
			Console.WriteLine("Bu guclendırme yalnızca ılk dort soru ıcın kullanılabılır.");
			return ;
		}

		if (_payIfPossible(40))
		{
			var q = _context.GetCurrentQuestion();
			_addHint( q.GetQuestionText() +" : " +q.GetAnswerText());
			_proceedToNextQuestion();

		}
		else {
			System.Console.WriteLine("Soru gecme jokeri kullanacak kadar puanin yok!");
			DisplayPoint();
		
		}

    }

	private bool _payIfPossible(int cost)
	{

		if(_currentPoint > cost)
		{
			_currentPoint -= cost;
			return true;
		}

		return false;

	}

    // soru dogru ıse questıon + answer
    // power up kullandıysa aynı sey
    private void _addHint(string hint)
    {
		_acquiredHints.Add(hint);

    }

    public void ShowMyHints()
	{
        Console.WriteLine("My all hints:");
        foreach (var acquiredHint in _acquiredHints)
        {
            Console.WriteLine("-" + acquiredHint);
        }
    }

	public void DisplayPoint()
	{
		if (_gameState == GameState.FINISH) {
            System.Console.WriteLine("Oyun bitti ama cok istiyorsan : ");
        }

		System.Console.Write("Your Point: ");
		if (_currentPoint < 50)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(_currentPoint);
            Console.ResetColor();
        }
		else {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(_currentPoint);
            Console.ResetColor();
        }
    }
	private void _proceedToNextQuestion()
	{
		var result = _context.MoveToNextQuestionIfAvailable();
		if (!result)
		{
			_gameState = GameState.FINAL_STATE;
		}

	}

	public void Answer(string name)
	{

		var check = _checkInput(name);
		if (!check)
		{
			System.Console.WriteLine("Hatali isim! Bir daha dene.");

			return;
		}

        switch (_gameState)
		{
			case GameState.INITIAL_STATE:
                if (_context.TryToAnswerToCurrentQuestion(name))
                {
                    var q = _context.GetCurrentQuestion();
                    System.Console.WriteLine("Dogru cevap!");
                    _addHint(q.GetQuestionText() + " : " + q.GetAnswerText());
                    _proceedToNextQuestion();
                }
                else
                {
                    System.Console.WriteLine("Yanlis cevap!");
                    _proceedToNextQuestion();

                }
                break;
			case GameState.FINAL_STATE:

                var result = _culpritName.ToLower() == name.ToLower();

				if (result)
				{
					System.Console.WriteLine("Tebrikler sucluyu buldun!");
					_gameState = GameState.FINISH;

				}
				else
				{
					System.Console.WriteLine("Yanlis cevap! Sucluyu bulamadin!");
					System.Console.WriteLine("Suclunun ismi " + _culpritName + " idi!");
                    _gameState = GameState.FINISH;
                }


				break ;
			case GameState.FINISH:
				System.Console.WriteLine("Oyun bitti eve git.");
				break ;

		}


	}

	private bool _checkInput(string input)
	{

		foreach ( var p in _context.GetPeople()) {
			if (p.GetName().ToLower() == input.ToLower())
			{
				return true;
			}

		} 
		return false;
	
	
	}
	

	public void ShowCurrentQuestion()
	{
		Question currentQuestion = _context.GetCurrentQuestion();
		if (currentQuestion != null)
		{
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Question: \n");
			Console.ResetColor();
			
			Console.WriteLine(currentQuestion.GetQuestionText());
		}
	}

	public void ShowNamesOfAllSuspects()
	{
        var suspects = _context.GetAllPeople();
        foreach (var suspect in suspects)
        {
            Console.WriteLine(suspect.GetName());
        }

    }

    public void ShowDetailsOfSuspect(string name)
    {

        var suspects = _context.GetAllPeople();
        var suspect = suspects.FirstOrDefault(s => s.GetName() == name);

        if (suspect != null)
        {
            Console.WriteLine($"Name: {suspect.GetName()}");
            Console.WriteLine($"Info: {suspect.GetInfo()}");
            Console.WriteLine($"Claim: {suspect.GetInitialClaim()}");
        }
        else
        {
            Console.WriteLine($"Suspect with name '{name}' not found.");
        }

    }

	public void ShowAllDetailsOfSuspects()
	{
        var suspects = _context.GetAllPeople();

        foreach (var person in suspects)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Name: {person.GetName()}");
			Console.ResetColor();

            Console.WriteLine($"Initial Claim: {person.GetInitialClaim()}");
            Console.WriteLine($"Info: {person.GetInfo()}");
            Console.WriteLine(new string('-', 30)); // Separator
        }
    }

	public void RequestHintAboutSuspect(string name)
	{
		if (!_payIfPossible(20))
		{
            Console.WriteLine("You don't have enough points to get hint!");
            return;
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Hint about " + name + ":");
			Console.ResetColor();

			Console.WriteLine(_context.GetHintFromSuspect(name));
		}
    }

    public void Help()
    {

    }

	public void DisplayStory()
	{
		Console.Write(" ");

        foreach (var p in _context.GetStory().Split('.'))
		{
            Console.WriteLine(p + ".");
        }

	}


}
