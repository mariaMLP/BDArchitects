namespace BDAProject.Services
{
    using System.Threading.Tasks;

    public interface IContactService
    {
        Task Execute(string name, string email, string subjectt, string message);
    }
}
