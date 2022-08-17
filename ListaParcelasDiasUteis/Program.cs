using Feriadosnacionais;

ListaParcelasValidaData();

static void ListaParcelasValidaData()
{
    Console.WriteLine("Digite uma data válida (yyyy-mm-dd):");
    var strData = Console.ReadLine();

    Console.WriteLine("Digite a quantidade de parcelas:");
    var parcelas = int.Parse(Console.ReadLine());

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

    var listaParcelas = new List<DateTime>();

    for (int i = 1; i <= parcelas; i++)
    {
        listaParcelas.Add(data.AddMonths(i));
    }

    Console.WriteLine("-----------------------------------------------------------------");

    var anoAtual = 0;
    List<Feriado> feriados = new List<Feriado>();
    foreach (var parcela in listaParcelas)
    {
        if (anoAtual != parcela.Year)
        {
            anoAtual = parcela.Year;
            feriados = CalculaFeriados.ObtemFeriadosNacionais(anoAtual);
        }

        var dataParcela = RetornaDiaUtil(parcela);

        var verificaData = feriados.FirstOrDefault(feriado => feriado.Data == dataParcela);
        if (verificaData != null)
        {
            dataParcela = dataParcela.AddDays(1);
            dataParcela = RetornaDiaUtil(dataParcela);
            Console.WriteLine(
                string.Format(
                    "{0} é {1}! - {2} é a próxima data útil!",
                    parcela.ToString("yyyy-MM-dd"),
                    verificaData.Nome,
                    dataParcela.ToString("yyyy-MM-dd")
                )
            );
        }
        else if (parcela != dataParcela)
        {
            Console.WriteLine(
                string.Format(
                    "{0} é {1}! - {2} é a próxima data útil!",
                    parcela.ToString("yyyy-MM-dd"),
                    (int)parcela.DayOfWeek == 0 ? "Domingo" : "Sábado",
                    dataParcela.ToString("yyyy-MM-dd")
                )
            );
        }
        else
        {
            Console.WriteLine(
                string.Format("{0} é uma data útil!", parcela.ToString("yyyy-MM-dd"))
            );
        }
    }
    Console.ReadLine();
}

static DateTime RetornaDiaUtil(DateTime data)
{
    switch ((int)data.DayOfWeek)
    {
        case 6:
            data = data.AddDays(2);
            break;
        case 0:
            data = data.AddDays(1);
            break;
    }

    return data;
}