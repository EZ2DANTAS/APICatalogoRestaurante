namespace APICatalogo.Sevices;

public class MeuServico : IMeuServico
{
    public string Saudacao(string nome)
    {
        return $"Ben vindo {nome} \n\n {DateTime.UtcNow}";
    }
}
