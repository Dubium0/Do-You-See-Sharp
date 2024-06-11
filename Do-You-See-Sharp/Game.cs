using System;

public class Game
{

	private Context _context;
	private List<string> _acquiredHints = new List<string>();
	private int _currentPoint = 100;
	public enum PowerUps
	{
		SKIP,
		HALF
	}

	private enum GameState
	{
		NOT_STARTED,
		INITIAL_STATE,
		FINAL_STATE,
		FINISH
	}

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

    }



	public void UsePowerUp(PowerUps powerUp)
	{
		switch (powerUp)
		{
			case PowerUps.SKIP: break;

			case PowerUps.HALF: break;

		}

	}

	private void _useHalfPowerUp()
	{

	}
    private void _useSkipPowerUp()
    {

    }

	private bool _payIfPossible(int cost)
	{
		return false;

	}

	public void ShowMyHints()
	{

	}

	// soru dogru ıse questıon + answer
	// power up kullandıysa aynı sey
	private void _addHint(string hint)
	{

	}

	public void DisplayPoint()
	{

	}


}
