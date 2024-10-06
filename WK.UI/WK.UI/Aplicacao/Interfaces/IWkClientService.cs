namespace WK.UI.Aplicacao.Interfaces
{
    public interface IWkClientService
    {
        Task<string> GetDataAsync(string metodo);
    }
}
