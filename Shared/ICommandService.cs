using System.Threading.Tasks;

namespace Bee
{
    public interface ICommandService
    {
        void Execute(string command);
    }
}