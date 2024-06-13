using APIGateway.Models;

namespace APIGateway.Services
{
    public class UserService
    {
        private readonly User /*тут будет название твоего сервиса*/ _user;
        
        public UserService(User user)
        {
            _user = user; 
        }
        public void Login(string login, string password)
        {
            //будет вызов метода что у тебя есть с регистрацией
        }
        public void Logout()
        {

        }
        public async Task Registration()
        {

        }
    }
}
