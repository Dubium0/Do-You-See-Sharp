﻿using System;
using System.Xml.Linq;

public class Game
{
	private Context _context;
	private List<string> _acquiredHints = new List<string>();
	private int _currentPoint = 100;
	private string _culpritName;
	private string _lastQuestionTxt = "Who is the culprit?";


	private bool _powerUpCheck = false;

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
	
	/// <summary>
	/// Constructor of the Game Class
	/// </summary>
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

		Help();

    }

	/// <summary>
	/// This function shuffles the people list so that half option will create a fair half choice set.
	/// </summary>
	/// <returns></returns> a list of people, which their inital locations are shuffled.
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

	/// <summary>
	/// this function allows to use half power up option. Also checks if there are enough points to use it,
	/// if the player is in the last question or not.
	/// </summary>
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
            Console.WriteLine("Yari yariya guclendirmesi kullanildi!.\nGeriye kalan supheliler : \n");
            Console.ResetColor();

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
		else
		{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Yari yariya guclendirmesi kullanacak puaninn yok!");
            Console.ResetColor();
        }
    }

	/// <summary>
	/// This is a private function to use SKIP power up as it is intended. It skips the question and adds it (question : answer ) pair to the hint list.
	/// </summary>
    private void _useSkipPowerUp()
    {
		if (_gameState != GameState.INITIAL_STATE) {
			Console.WriteLine("Bu guclendırme yalnızca ılk dort soru ıcın kullanılabılır.");
			return ;
		}

		if (_powerUpCheck)
		{
            Console.WriteLine("Bu guclendırme yalnızca bir kez kullanilabilir.");
            return;

        }
		if (_payIfPossible(40))
		{
            Console.WriteLine("Soru gecme guclendirmesi kullanildi!");
            var q = _context.GetCurrentQuestion();
			_addHint( q.GetQuestionText() +" : " +q.GetAnswerText());
			_proceedToNextQuestion();
			_powerUpCheck = true;
         

		}
		else {
			System.Console.WriteLine("Soru gecme jokeri kullanacak kadar puanin yok!");
			DisplayPoint();
		
		}

    }

	/// <summary>
	/// this function checks if the player has enough points to pay the cost.
	/// </summary>
	/// <param name="cost"></param> 
	/// <returns></returns> true if points are enough to pay the cost else false
	private bool _payIfPossible(int cost)
	{

		if(_currentPoint > cost)
		{
			_currentPoint -= cost;
			return true;
		}

		return false;

	}


	/// <summary>
	/// This is a private function to add hints acquired to the hint list.
	/// </summary>
	/// <param name="hint"></param>
    private void _addHint(string hint)
    {
		_acquiredHints.Add(hint);

    }

    /// <summary>
    /// This private function enables us to go to the next question available 
    /// </summary>
    private void _proceedToNextQuestion()
    {
        var result = _context.MoveToNextQuestionIfAvailable();
        if (!result)
        {
            _gameState = GameState.FINAL_STATE;
        }
    }


    /// <summary>
    /// This is a private helper function to chech case sensitivity.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private bool _checkInput(string input)
    {
        foreach (var p in _context.GetAllPeople())
        {
            if (p.GetName().ToLower() == input.ToLower())
            {
                return true;
            }

        }
        return false;
    }

    /// <summary>
    /// This function prints the main story of the context.
    /// </summary>
    public void DisplayStory()
    {
        Console.Write(" ");

        foreach (var p in _context.GetStory().Split('.'))
        {
            Console.WriteLine(p + ".");
        }

    }


    /// <summary>
    /// With this function player can use 2 types of powerups "HALF" or "SKIP".
    /// With "HALF" option, half of the suspects will be eliminated indicating that they are not the correct answer.
    /// With the "SKIP" option, player can skip the question and answer of it will be added to hints of the player in case to use this information later.
    /// </summary>
    /// <param name="powerUp"></param> this is an PowerUp enum type, user should choose one of those.
    public void UsePowerUp(PowerUps powerUp)
    {
        if (_gameState == GameState.FINISH)
        {
            Console.WriteLine("Oyun bitti.");
            return;
        }

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


    /// <summary>
    /// This function shows the all hints acquired in a game.
    /// </summary>
    public void ShowMyHints()
	{
        Console.WriteLine("Ip uclarim :");
        foreach (var acquiredHint in _acquiredHints)
        {
            Console.WriteLine("-" + acquiredHint);
        }
    }


	/// <summary>
	/// This function displays current point of the player.
	/// </summary>
	public void DisplayPoint()
	{
		if (_gameState == GameState.FINISH) {
            System.Console.WriteLine("Oyun bitti.");
        }

		System.Console.Write("Toplam Puanin : ");
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


	/// <summary>
	/// This is a function which player can give answer to the current question. Name checks are not case sensitive.
	/// </summary>
	/// <param name="name"></param> name of the suspect as string
	public void Answer(string name)
	{
        if (_gameState == GameState.FINISH)
        {
            System.Console.WriteLine("Oyun bitti.Cevap verecek soru yok.");
            return;
        }

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
                    Console.WriteLine("Oyundan çıkmak için Quit fonskiyonunu çağırın.");

                }
				else
				{
					System.Console.WriteLine("Yanlis cevap! Sucluyu bulamadin!");
					System.Console.WriteLine("Suclunun ismi " + _culpritName + " idi!");
                    _gameState = GameState.FINISH;
                }


				break ;
			case GameState.FINISH:
				System.Console.WriteLine("Oyun bitti. Cevap verecek soru yok.");
				break ;

		}

	}
	

	/// <summary>
	/// This is a function to display the current question in the game. If the game has not finished yet, it will print the current question. 
	/// Else it will indicate that game is over.
	/// </summary>
	public void ShowCurrentQuestion()
	{
		if (_gameState == GameState.FINISH)
		{
			Console.WriteLine("Oyun bitti. Daha fazla soru yok.");
			return;
		}

		Question currentQuestion = _context.GetCurrentQuestion();
		if (currentQuestion != null)
		{
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Soru : \n");
			Console.ResetColor();
			
			Console.WriteLine(currentQuestion.GetQuestionText());
		}
	}

	/// <summary>
	/// This function prints the names of the all suspects in a context.
	/// </summary>
	public void ShowNamesOfAllSuspects()
	{
        var suspects = _context.GetAllPeople();
        foreach (var suspect in suspects)
        {
            Console.WriteLine(suspect.GetName());
        }

    }


    /// <summary>
    /// This function prints the name, information and claims of the specified suspect in a context.
    /// </summary>
    /// <param name="name"></param>
    public void ShowDetailsOfSuspect(string name)
    {
        var suspects = _context.GetAllPeople();
        var suspect = suspects.FirstOrDefault(s => s.GetName() == name);

        if (suspect != null)
        {
            Console.WriteLine($"Isim: {suspect.GetName()}");
            Console.WriteLine($"Kisisel Bilgiler: {suspect.GetInfo()}");
            Console.WriteLine($"Iddiasi : {suspect.GetInitialClaim()}");
        }
        else
        {
            Console.WriteLine($"'{name}' isimli supheli bulunamadi.");
        }
    }


    /// <summary>
    /// This function prints the name, information and claims of the all suspects in a context.
    /// </summary>
    /// <param name="name"></param>
    public void ShowAllDetailsOfSuspects()
	{
        var suspects = _context.GetAllPeople();

        foreach (var person in suspects)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Isim: {person.GetName()}");
			Console.ResetColor();

            Console.WriteLine($"Kisiel Bilgiler: {person.GetInfo()}");
            Console.WriteLine($"Iddiasi: {person.GetInitialClaim()}");
            Console.WriteLine(new string('-', 30)); // Separator
        }
    }


	/// <summary>
    /// This function returns the extra hint attribute information of a suspect with given name.
	/// </summary>
	/// <param name="name"></param>
	public void RequestHintAboutSuspect(string name)
	{

        if (_gameState == GameState.FINISH)
        {
            System.Console.WriteLine("Oyun bitti.");
            return;
        }

        if (!_payIfPossible(20))
		{
            Console.WriteLine("Ip ucu alacak kadar puanin yok!");
            return;
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(name + " hakkinda ipucu " + ":");
			Console.ResetColor();

			Console.WriteLine(_context.GetHintFromSuspect(name));
		}
    }

	/// <summary>
	/// This function prints the help options of the game.
	/// </summary>
    public void Help()
    {
        Console.WriteLine("Do You See Sharp Oyununa Hosgeldin!");
        Console.WriteLine("Oyunun oynayışı hakkında bilgi almak için HowToPlay fonskiyonunu çağırın.");
        Console.WriteLine("Oyunu başlatmak için StartGame fonksiyonunu çağırın.");
        Console.WriteLine("Oyundan çıkmak için Quit fonksiyonunu çağırın.");
        Console.WriteLine("Oyunu oynamak için gerekli tüm fonskiyonları görmek için 'GameObject.' kullanın.");
    }


    /// <summary>
    /// This is a function to print game instructions.
    /// </summary>
    public void HowToPlay()
	{
        Console.WriteLine("Do You See Sharp! Oyununa Hosgeldin!");
        Console.WriteLine("Bu oyun hikaye tabanlı bir dedektiflik oyunudur. Oyun başladığında, olay hakkında bilgi içeren kısa bir paragrafı okuyacaksınız.");
        Console.WriteLine("Olayın şüphelileri hakkında bilgileri ve iddialarını öğrenmek için sağlanan fonksiyonları kullanabilirsiniz.");
        Console.WriteLine("Oyunun ilk aşamasında 4 adet yönlendirici soru alacaksınız. Bazı soruların benzer profilleri olabilir, dikkatli olun!");
        Console.WriteLine("Bir soruyu yanlış cevaplarsanız 15 puan kaybedeceksiniz ve doğru cevabı göremeyeceksiniz, ancak neyin yanlış olduğunu aklınızda tutmak isteyebilirsiniz ;).");
        Console.WriteLine("Doğru cevaplarsanız puanınız sabit kalacak ve soru-cevap ikilisi, oyun boyunca elde ettiğiniz ip envanterine eklenecektir.");
        Console.WriteLine("İlk 4 soruyu doğru cevaplarsanız, şüpheli listesi giderek daralacak ve final sorusunda suçluyu doğru tahmin etme ihtimaliniz artacak şekilde tasarlandı.");
        Console.WriteLine("Ayrıca oyunda çeşitli güçlendirme seçenekleri bulunmaktadır. İlk 4 soru için, 40 puan karşılığında soruyu atlayabilirsiniz. Bu seçeneği kullandığınızda, soru-cevap ikilisi ip envanterine eklenecektir.");
        Console.WriteLine("Final aşamasına geldiğinizde, elinizde 80 puan varsa, 'half' güçlendirme seçeneğini kullanarak yanlış cevapların yarısını elemek için puanınızı kullanabilirsiniz.");
        Console.WriteLine("20 puan karşılığında, belirli bir şüpheliden ekstra ip talep etme imkanınız vardır.");
        Console.WriteLine("Detaylara dikkat etmeyi unutmayın! İyi eğlenceler.");

    }

    /// <summary>
    /// With this function call interactive window will terminate and the game will over.
    /// </summary>
    public void Quit()
    {
        Environment.Exit(0);
    }
}
