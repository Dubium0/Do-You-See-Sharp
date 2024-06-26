﻿using System;
using System.Xml.Linq;

public sealed class Game
{

	private static readonly Game _instance = new Game();
	public static Game Instance
	{
		get { return _instance; }
	}

	private Context _context;
	private List<string> _acquiredHints = new List<string>();
	private int _currentPoint = 100;
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
	private Game() 
	{
        _context = new Context("Olay, tarihi ve sanatsal eserlerle ünlü bir müzede geçmektedir." +
            " Müzenin en değerli tablolarından biri gizemli bir şekilde yok olmuştur. " +
            "Güvenlik kameraları, olay günü altı farklı şüphelinin tablonun bulunduğu galeriden geçtiğini kaydetmiştir. " +
            "Müze kapalıyken gerçekleşen bu olay, galerinin içindeki güvenlik sisteminin de devre dışı bırakıldığı bir zaman diliminde olmuştur. " +
            "Olayın ardından müze çalışanları birbirlerini suçlayarak ifadeler vermişlerdir ve bazı ifadelerde çelişkiler bulunmaktadır.",
            "Muze Calisma Kurallari: "+
            "Mesai saatleri siradan calisanlar icin 12.00 - 23.00\r\n" +
            "Guvenlik gorevlileri icin bu 18.00 - 06.00 ve 06.00 - 18.00 olacak sekilde 12 saatlik vardiyalar olarak gerceklesir.\r\n"+
            "Guvenlik ve temizlik gorevlileri haric kamera odasina giris yasaktir."
            ); 

		_context.AddNewPeople(new People("Emre Aslan",
			"35 yaşında, kısa siyah saçlı bir adam",
            "Müze temizlik personeli. Olay günü gece vardiyasındaydı. Eserin olduğu koridoru temizleyip çıktıktan sonra tekrar oraya girmediğini iddia ediyor.",
			"Çalışma saatlerinin dışında müzede kaldığına dair güvenlik kayıtlarında bazı belirsizlikler var."));

		_context.AddNewPeople(new People("Leyla Demir",
			"25 yaşında, kızıl saçları olan bir uzun boylu kadın", 
			"Müze kafesinde barista. Sanattan anlamadığı için hiçbir zaman sanat galerilerine girmediğini. İşlerini bitirdikten sonra fitness'a gittiğini iddia ediyor.",
			"Müzenin güvenlik şifresini biliyor çünkü geç saatlerde bazen müze içinde kalmak zorunda kalıyor. Eski sevgilisi müze güvenliğinden sorumlu teknik ekipteydi."));

		_context.AddNewPeople(new People("Ahmet Yıldız", 
			"43 yaşında, kel, uzun boylu ve geniş omuzlu bir adam",
			"Müzenin güvenlik görevlisi. O gece görevliydi ve her şeyin normal olduğunu ve herhangi bir şüpheli hareket görmediğini belirtiyor.", 
			"Eski bir polis memuru ve kilitli kapıları açma konusunda uzman. Müze içindeki değerli eserler için yeni güvenlik önlemleri alınmasını önermiş."));

		_context.AddNewPeople(new People("Nur Toprak",
			"50 yaşında, gri saçlı zarif bir kadın", 
			"Müzenin küratörü. Olay günü eve erken gittiğini iddia ediyor.\n" +
            "Ayrıca tablonun oldukça ağır olduğunu ve kendisinin onu taşıyabilecek güçte olmadığını kanıt olarak sunuyor.",
            "Müzenin her köşesini çok iyi bilir ve nadiren kullanılan gizli geçitler hakkında bilgi sahibidir. Çok disiplinli bir kadındır."));

		_context.AddNewPeople(new People("Barış Kaya", 
			"29 yaşında, sarı saçlı ve atletik yapıda genç bir adam",
			"Müze rehberi. Tur gruplarına liderlik ettiğini, ancak o gün hiç tur olmadığını belirtiyor.",
            "Nur Toprak çalışanlar arasında ilişki yaşanmasını istemediğinden, Barış müzede olup olmadığı hakkında yalan söyledi."));

		_context.AddNewPeople(new People("Seda Çınar",
			"27 yaşında, düz siyah saçlı bir kadın",
            "Serbest zamanlı sanatçı ve sık sık müzede çalışıyor. " +
            "O gün Barış ile tartıştıktan sonra müzeden geç olmadan çıktığını ifade ediyor.",
			"Müzede sergilenen eserler hakkında derin bilgilere sahip ve bu eserlerin değerlerini iyi biliyor. Depolarda saklanan eski sanat eserlerine kimseye söylemeden eriştiğine dair dedikodular var."));




        _context.AddNewCameraRecord(new CameraRecord("cam1", "Ana giriş çıkış kapısı", "*no records! All off them are deleted"));

        _context.AddNewCameraRecord(new CameraRecord("cam2", "Arka Bahçe",
            "19.30 Nur Toprak bahçede kahve içiyor.\r\n" +
            "21.38 Ahmet Yıldız ile Emre Aslan bahçede sigara ve kahve içiyor.\r\n" +
            "21.45 Emre Aslan içeri geri giriyor.\r\n" +
            "22.15 Ahmet Yıldız binanın içine giriyor.\r\n"));

        _context.AddNewCameraRecord(new CameraRecord("cam3", "Eserin bulunduğu koridor", 
            "19.25 Nur Toprak çantasını alıp odasından çıkıyor.\r\n" +
            "21.27 Emre Aslan eserlerin tozunu alırken görüntüleniyor.\r\n" +
            "21.35 Ahmet yıldız güvenlik odasından çıkıp arka bahçeye doğru gidiyor.\r\n" +
            "21.40 Arka bahçeye çıkan kapının önünde Emre ile Ahmet sohbet ediyor. \r\n" +
            "*no records \r\n" +
            "23.20 Eserin yerinde olmadığı görünüyor.\r\n"));

        _context.AddNewCameraRecord(new CameraRecord("cam4", "Cafe Bölümü", 
            "19.30 Barış ile Seda tartışıyor. Leyla Barış’ın tarafında durup onu geri çekmeye çalışıyor.\r\n" +
            "20.10 Barış ile Leyla el ele tutuşurken görüntüleniyor.\r\n" +
            "21.20 Leyla Barisi kafeden cikariyor.\r\n" +
            "21.30 Leyla Demir’in mutfak bölümünde bardakları dizdiği görünüyor.\r\n" +
            "*no records\r\n" + 
            "22.00 Emre Aslan temizlik arabısını sürükleyerek Cafe den cikisa giden koridora dogru gidiyor\r\n"
            ));



        _context.AddNewQuestion(new Question("Eseri baskalari da varken calmak cok riskli, herkes ciktiktan sonra calinmis olmali. Olayın gerçekleştiği gün galeriden çıkan son kişi kim olabilir?",
			"Leyla Demir"));

        _context.AddNewQuestion(new Question("Birileri müzede olup olmadığı ile ilgili yalan konuşuyor. Bu kim olabilir?",
			"Barış Kaya"));

        _context.AddNewQuestion(new Question("Eser kilitli bir bolgede tutuldugu halde hirsiz bir sekilde eseri calmayi basarmis, bunu kim yapabilir?",
			"Ahmet Yıldız"));

        _context.AddNewQuestion(new Question("Müzede sergilenen eserler hakkında derin bilgilere sahip olan bir kişinin, değerli bir tablonun kaybolmasında bir çıkarı olabilir. Bu profili hangi şüpheli karşılar?", 
			"Seda Çınar"));

		_context.LastQuestion = new Question("Suçlu kim?", "Leyla Demir");

        Help();
    }

	/// <summary>
	/// This function shuffles the people list so that half option will create a fair half choice set.
	/// </summary>
	/// <returns></returns> a list of people, which their inital locations are shuffled.
	private List<People> _shufflePeople()
	{
        Random _random = new Random();
        string correctAnswer = _context.GetCurrentQuestion().QuestionAnswer;
        List<People> shuffledPeople = (List<People>)_context.People;

        var halfSize = shuffledPeople.Count / 2;

        // Shuffle the list randomly
        shuffledPeople = shuffledPeople.OrderBy(x => _random.Next()).ToList();

        People correctPerson = shuffledPeople.Find(p => p.Name == correctAnswer);

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
			Console.WriteLine("Bu güçlendirme yalnızca son soruda kullanılabilir!");
			return;
		
		}

		bool hasEnoughMoney = _payIfPossible(80);
		if (hasEnoughMoney)
		{
            
            List<People> shuffledPeople = _shufflePeople();
            List<string> names = shuffledPeople.Take(shuffledPeople.Count / 2).Select(p => p.Name).ToList();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Yarı yarıya güçlendirmesi kullanıldı!\nGeriye kalan şüpheliler : \n");
            Console.ResetColor();

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
		else
		{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Yarı yarıya jokeri kullanacak kadar puanın yok! (80p)");
            Console.ResetColor();
        }
    }

	/// <summary>
	/// This is a private function to use SKIP power up as it is intended. It skips the question and adds it (question : answer ) pair to the hint list.
	/// </summary>
    private void _useSkipPowerUp()
    {
		if (_gameState != GameState.INITIAL_STATE) {
			Console.WriteLine("Bu güçlendirme yalnızca ilk 4 sorudan birisi için kullanılabilir.");
			return ;
		}

		if (_powerUpCheck)
		{
            Console.WriteLine("Bu güçlendirme yalnızca bir kez kullanılabilir.");
            return;

        }
		if (_payIfPossible(40))
		{
            Console.WriteLine("Soruyu atlama jokeri kullanıldı! Cevabı hint olarak listende görebilirsin.");
            var q = _context.GetCurrentQuestion();
			_addHint( q.QuestionText +" : " +q.QuestionAnswer);
			_proceedToNextQuestion();
			_powerUpCheck = true;
         

		}
		else {
			System.Console.WriteLine("Soru geçme jokeri kullanacak kadar puanın yok! (40p)");
			ShowPoint();
		
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
        foreach (var p in _context.People)
        {
            if (p.Name.ToLower() == input.ToLower())
            {
                return true;
            }

        }
        return false;
    }

    private void _fixPointToZero()
    {
        if(_currentPoint < 0)
            _currentPoint = 0;
    }

    /// <summary>
    /// This function prints the main story of the context.
    /// </summary>
    public void ShowStory()
    {
        Console.Write(" ");

        foreach (var p in _context.StoryText.Split('.'))
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
            Console.WriteLine("Oyun bitti. Çıkmak için Quit fonskiyonunu kullan.");
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
        Console.WriteLine("Ip uçlarım :");
        foreach (var acquiredHint in _acquiredHints)
        {
            Console.WriteLine("-" + acquiredHint);
        }
    }


	/// <summary>
	/// This function displays current point of the player.
	/// </summary>
	public void ShowPoint()
	{
		if (_gameState == GameState.FINISH) {
            System.Console.WriteLine("Oyun bitti.");
        }

		System.Console.Write("Toplam Puanın: ");
		if (_currentPoint == 0)
		{
			Console.WriteLine(_currentPoint + " Artık hiçbir güçlendirme kullanamaz veya hint alamazsın.");
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
            Console.WriteLine("Oyun bitti. Çıkmak için Quit fonskiyonunu kullan.");
            return;
        }

        var check = _checkInput(name);
		if (!check)
		{
			System.Console.WriteLine("Hatalı isim! Bir daha dene.");

			return;
		}

        switch (_gameState)
		{
			case GameState.INITIAL_STATE:
                if (_context.TryToAnswerToCurrentQuestion(name))
                {
                    var q = _context.GetCurrentQuestion();
                    System.Console.WriteLine("Doğru cevap!");
                    _addHint(q.QuestionText + " : " + q.QuestionAnswer);
                    _proceedToNextQuestion();
                }
                else
                {
                    System.Console.WriteLine("Yanlış cevap!");
                    _currentPoint = _currentPoint - 15;
                    _fixPointToZero();
                    ShowPoint();
                    _proceedToNextQuestion();

                }
                break;
			case GameState.FINAL_STATE:

                var result = _context.LastQuestion.QuestionAnswer.ToLower() == name.ToLower();

				if (result)
				{
					System.Console.WriteLine("Tebrikler suçluyu buldun!");

                    if(_currentPoint > 80)
                    {
                        System.Console.WriteLine("Seviye : Sherlock Holmes");
                    }else if(_currentPoint > 50)
                    {
                        System.Console.WriteLine("Seviye : Tecrübeli Dedektif");
                    }else if (_currentPoint > 20)
                    {
                        System.Console.WriteLine("Seviye : Stajyer Dedektif");
                    }
                    else
                    {
                        System.Console.WriteLine("Seviye : İşin meraklisi");
                    }

					_gameState = GameState.FINISH;
                    Console.WriteLine("Oyun bitti. Çıkmak için Quit fonskiyonunu kullan.");

                }
				else
				{
					System.Console.WriteLine("Yanlış cevap! Suçluyu bulamadın!");
					System.Console.WriteLine("Suçlunun ismi " + _context.LastQuestion.QuestionAnswer + " idi!");
                    _gameState = GameState.FINISH;
                }


				return ;
			case GameState.FINISH:
                Console.WriteLine("Oyun bitti. Çıkmak için Quit fonskiyonunu kullan.");
                return;

		}

        Console.WriteLine("-------------Sıradaki Soru-------------");
        ShowCurrentQuestion();

	}

	
	/// <summary>
	/// This is a function to display the current question in the game. If the game has not finished yet, it will print the current question. 
	/// Else it will indicate that game is over.
	/// </summary>
	public void ShowCurrentQuestion()
	{
		if (_gameState == GameState.FINISH)
		{
            Console.WriteLine("Oyun bitti. Çıkmak için Quit fonskiyonunu kullan.");
            return;
		}

		Question currentQuestion = _context.GetCurrentQuestion();
		if (currentQuestion != null)
		{
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Soru: ");
			Console.ResetColor();
			
			Console.WriteLine(currentQuestion.QuestionText);
		}
	}

	/// <summary>
	/// This function prints the names of the all suspects in a context.
	/// </summary>
	public void ShowNamesOfAllSuspects()
	{
        var suspects = _context.People;
        foreach (var suspect in suspects)
        {
            Console.WriteLine(suspect.Name);
        }

    }


    /// <summary>
    /// This function prints the name, information and claims of the all suspects in a context.
    /// </summary>
    /// <param name="name"></param>
    public void ShowDetailsOfAllSuspects()
	{
        var suspects = _context.People;

        foreach (var person in suspects)
        {
            Console.WriteLine($"İsim: {person.Name}");
            Console.WriteLine($"Kişisel Bilgiler: {person.Info}");
            Console.WriteLine($"iddiasi: {person.InitialClaim}");
            Console.WriteLine(new string('-', 30)); // Separator
        }
    }

    /// <summary>
    /// This function prints the name, information and claims of the specified suspect in a context.
    /// </summary>
    /// <param name="name"></param>
    public void ShowDetailsOfASuspect(string name)
    {
        var suspects = _context.People;
        var suspect = suspects.FirstOrDefault(s => s.Name == name);

        if (suspect != null)
        {
            Console.WriteLine($"İsim: {suspect.Name}");
            Console.WriteLine($"Kişisel Bilgiler: {suspect.Info}");
            Console.WriteLine($"Iddiası : {suspect.InitialClaim}");
        }
        else
        {
            Console.WriteLine($"'{name}' isimli şüpheli bulunamadı.");
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
            Console.WriteLine("Oyun bitti. Çıkmak için Quit fonskiyonunu kullan.");
            return;
        }
        var result = _context.GetHintFromSuspect(name);
        if (result == null) { 
            return;
        }

        if (!_payIfPossible(20))
		{
            Console.WriteLine("İp ucu alacak kadar puanın yok!");
            return;
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(name + " hakkında ipucu " + ":");
			Console.ResetColor();

            string hint = result;

            _addHint(name + " " + hint);
            Console.WriteLine(hint);            
		}
    }


    /// <summary>
    /// Returns the names of all cameras
    /// </summary>
    public void ShowCameraNames()
    {
        var cameras = _context.CameraRecords;
        foreach (var cam in cameras)
        {
            Console.WriteLine(cam.Name);
        }

    }

    /// <summary>
    /// prints the records of all cameras
    /// </summary>
    public void ShowAllCameraRecords()
    {

        var cameras = _context.CameraRecords;

        foreach (var camera in cameras)
        {
            Console.WriteLine($"Kamera: {camera.Name}");
            Console.WriteLine($"Gördüğü Lokasyon: {camera.Location}");
            Console.WriteLine($"Kayıtlar: \n{camera.Record}");
            Console.WriteLine(new string('-', 30)); // Separator
        }
    }


    /// <summary>
    /// Prints the record of a camera specified by name
    /// </summary>
    /// <param name="name"></param>
    public void ShowCameraRecord(string name)
    {
        var cameras = _context.CameraRecords;
        var camera = cameras.FirstOrDefault(s => s.Name == name);

        if (camera != null)
        {
            Console.WriteLine($"Kamera: {camera.Name}");
            Console.WriteLine($"Gördüğü Lokasyon: {camera.Location}");
            Console.WriteLine($"Kayıtlar: \n{camera.Record}");
        }
        else
        {
            Console.WriteLine($"'{name}' isimli kamera bulunamadı.");
        }

    }
    public void ShowStoryRules()
    {
        Console.WriteLine(_context.StoryRules);

    }
    /// <summary>
    /// This function prints the help options of the game.
    /// </summary>
    public void Help()
    {
        Console.WriteLine("Do You See Sharp Oyununa Hosgeldin!");
        Console.WriteLine("Oyunun oynayışı hakkında bilgi almak için HowToPlay fonskiyonunu çağırın.");
        Console.WriteLine("Oyunun hikayesini görmek için ShowStory fonskiyonunu çağırın.");
        Console.WriteLine("Soruları görmek için ShowCurrentQuestion fonskiyonunu çağırın.");
        Console.WriteLine("Oyundan çıkmak için Quit fonksiyonunu çağırın.");
        Console.WriteLine("Oyunu oynamak için gerekli tüm fonskiyonları görmek için 'Game.Instance' kullanın.");
    }


    /// <summary>
    /// This is a function to print game instructions.
    /// </summary>
    public void HowToPlay()
	{
        Console.WriteLine("Hoşgeldin! Olay hakkında paragraf oku, şüphelileri ve iddiaları incele.");
        Console.WriteLine("İlk aşamada 4 soru alacaksın, yanlış cevap 15 puan kaybettirir, doğru cevap hint listene eklenir.");
        Console.WriteLine("Doğru cevaplar şüpheli listesini daraltır, finalde suçluyu tahmin etme şansını arttırır.");
        Console.WriteLine("İlk 4 soru için 40 puanla soruyu atlamaya, finalde ise 80 puanla yarıya kadar yanlış cevapları elemeye yardımcı olabilirsin.");
        Console.WriteLine("20 puanla bir şüpheliden ekstra hint alabilirsin.");
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
