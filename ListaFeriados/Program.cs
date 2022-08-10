using ListaFeriados;

Console.WriteLine("Digite uma data válida (yyyy-mm-dd):");
var strData = Console.ReadLine();

Console.WriteLine("-----------------------------------------------------------------");

DateTime data;
try
{
    data = DateTime.ParseExact(strData, "yyyy-MM-dd", null);
}
catch (Exception)
{
    Console.WriteLine("Data inválida!");
    return;
}

var feriados = ObtemFeriadosNacionais(data.Year);

foreach (var feriado in feriados)
{
    Console.WriteLine(string.Format("Data:{0} - Feriado: {1}", feriado.Data.ToString("yyyy-MM-dd"), feriado.Nome));
}

Console.WriteLine("-----------------------------------------------------------------");

var verificaData = feriados.FirstOrDefault(feriado => feriado.Data == data);
if (verificaData != null)
    Console.WriteLine(string.Format("{0} é um feriado! - {1}", strData, verificaData.Nome));
else
    Console.WriteLine(string.Format("{0} não é um feriado!", strData));

Console.ReadLine();

static List<Feriado> ObtemFeriadosNacionais(int? ano = null)
{
    var listaDeFeriados = new List<Feriado>();
    var anoCorrente = DateTime.Now.Year;

    if (ano != null)
        anoCorrente = ano.Value;

    listaDeFeriados.Add(new Feriado() { Data = new DateTime(anoCorrente, 1, 1), Nome = "Ano novo" });
    listaDeFeriados.Add(new Feriado() { Data = new DateTime(anoCorrente, 4, 21), Nome = "Tiradentes" });
    listaDeFeriados.Add(new Feriado() { Data = new DateTime(anoCorrente, 5, 1), Nome = "Dia do trabalho" });
    listaDeFeriados.Add(new Feriado() { Data = new DateTime(anoCorrente, 9, 7), Nome = "Dia da Independência do Brasil" });
    listaDeFeriados.Add(new Feriado() { Data = new DateTime(anoCorrente, 10, 12), Nome = "Nossa Senhora Aparecida" });
    listaDeFeriados.Add(new Feriado() { Data = new DateTime(anoCorrente, 11, 2), Nome = "Finados" });
    listaDeFeriados.Add(new Feriado() { Data = new DateTime(anoCorrente, 11, 15), Nome = "Proclamação da República" });
    listaDeFeriados.Add(new Feriado() { Data = new DateTime(anoCorrente, 12, 25), Nome = "Natal" });

    #region FeriadosMóveis

    int x, y;
    int a, b, c, d, e;
    int dia, mes;

    if (anoCorrente >= 1900 & anoCorrente <= 2099)
    {
        x = 24;
        y = 5;
    }
    else
        if (anoCorrente >= 2100 & anoCorrente <= 2199)
    {
        x = 24;
        y = 6;
    }
    else
            if (anoCorrente >= 2200 & anoCorrente <= 2299)
    {
        x = 25;
        y = 7;
    }
    else
    {
        x = 24;
        y = 5;
    }

    a = anoCorrente % 19;
    b = anoCorrente % 4;
    c = anoCorrente % 7;
    d = (19 * a + x) % 30;
    e = (2 * b + 4 * c + 6 * d + y) % 7;

    if ((d + e) > 9)
    {
        dia = (d + e - 9);
        mes = 4;
    }

    else
    {
        dia = (d + e + 22);
        mes = 3;
    }

    var pascoa = new DateTime(anoCorrente, mes, dia);
    var sextaSanta = pascoa.AddDays(-2);
    var carnaval = pascoa.AddDays(-47);
    var corpusChristi = pascoa.AddDays(60);

    listaDeFeriados.Add(new Feriado() { Data = pascoa, Nome = "Pascoa" });
    listaDeFeriados.Add(new Feriado() { Data = sextaSanta, Nome = "Sexta-feira Santa" });
    listaDeFeriados.Add(new Feriado() { Data = carnaval, Nome = "Carnaval" });
    listaDeFeriados.Add(new Feriado() { Data = corpusChristi, Nome = "Corpus Christi" });

    #endregion

    return listaDeFeriados.OrderBy(x => x.Data).ToList();
}