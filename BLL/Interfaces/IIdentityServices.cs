using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
   public interface IIdentityServices
   {
       UserDTO GetUser(int? id);
       IEnumerable<UserDTO> GetAllUsers();
       void SetUser(UserDTO userDTO);
       void UpdateUser(UserDTO userDTO);
       void DeleteUser(int? id);

       RoleDTO GetRole(int? id);
       IEnumerable<RoleDTO> GetAllRoles();
       void SetRole(RoleDTO roleDTO);
       void UpdateRole(RoleDTO roleDTO);
       void DeleteRole(int? id);
    }
}
