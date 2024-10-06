namespace WK.UI.Aplicacao.Interfaces
{
    public interface IWkClientService
    {
        Task<string> GetDataAsync(string metodo);
        Task<bool> PostDataAsync<T>(string metodo, T dados);
        Task<bool> PutDataAsync<T>(string metodo, T dados);
        Task<bool> DeleteDataAsync(string metodo);
    }
}
