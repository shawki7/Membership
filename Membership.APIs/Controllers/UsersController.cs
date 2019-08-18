using Membership.IData.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Membership.APIs.Controllers
{
    public class UsersController : Controller
    {
        #region Properties
        public IUsersRepository UsersRepository { get; set; }
        #endregion


        #region Constructor
        public UsersController(IUsersRepository usersRepository)
        {
            UsersRepository = usersRepository;
        }

        #endregion

        #region Methods
        
        #region Get
        public IActionResult GetAllSUsers()
        {
            return Json(UsersRepository.GetAllUsers());
        }

        public IActionResult GetAccount(int id)
        {
            return Json(UsersRepository.GetAccount(id));
        }
        #endregion

        #endregion
    }
}