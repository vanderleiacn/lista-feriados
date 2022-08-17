using Feriadosnacionais;

ListaFeriadovalidaData();

static void ListaFeriadovalidaData()
{
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

    var feriados = CalculaFeriados.ObtemFeriadosNacionais(data.Year);

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
}