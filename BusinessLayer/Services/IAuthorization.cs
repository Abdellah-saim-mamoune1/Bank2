using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;

namespace bankApI.BusinessLayer.Services
{
    public interface IAuthorization
    {

        public Task<DGetEmployee> SignInAsync(DSignIn signIn);


    }
}
