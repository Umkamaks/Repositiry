using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using BLL.Infrastructure;
using DAL.Entities;

namespace BLL.Services
{
    public class IdentityServices : IIdentityServices
    {
        IUnitOfWork Database { get; set; }

        public IdentityServices(IUnitOfWork Database)
        {
            this.Database = Database;
        }
        public void DeleteRole(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id роли", "");
            Database.Role.Delete(id.Value);
        }

        public void DeleteUser(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id юзера", "");
            Database.User.Delete(id.Value);
        }

        public IEnumerable<RoleDTO> GetAllRoles()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Role, RoleDTO>());
            return Mapper.Map<IEnumerable<Role>, List<RoleDTO>>(Database.Role.GetAll());
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            return Mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.User.GetAll());
        }

        public RoleDTO GetRole(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id роли", "");
            var role = Database.Role.Get(id.Value);

            if (role == null)
                throw new ValidationException("Не найдена роль", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Role, RoleDTO>());
            return Mapper.Map<Role, RoleDTO>(role);
        }

        public UserDTO GetUser(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id юзера", "");
            var user = Database.User.Get(id.Value);

            if (user == null)
                throw new ValidationException("Не найден юзер", "");
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            return Mapper.Map<User, UserDTO>(user);
        }

        public void SetRole(RoleDTO roleDTO)
        {
            if (roleDTO == null)
                throw new ValidationException("Пустая роль", "");

            Mapper.Initialize(cfr => cfr.CreateMap<RoleDTO, Role>());

            Role role = Mapper.Map<RoleDTO, Role>(roleDTO);
            Database.Role.Create(role);
        }

        public void SetUser(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ValidationException("Нет такого юзера", "");

            Mapper.Initialize(cfr => cfr.CreateMap<UserDTO, User>());

            User user = Mapper.Map<UserDTO, User>(userDTO);
            Database.User.Create(user);
        }

        public void UpdateRole(RoleDTO roleDTO)
        {
            if (roleDTO == null)
                throw new ValidationException("Пустая роль", "");
            Mapper.Initialize(cfr => cfr.CreateMap<RoleDTO, Role>());
            Role role = Mapper.Map<RoleDTO, Role>(roleDTO);
            Database.Role.Update(role);
        }

        public void UpdateUser(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ValidationException("Пустая роль", "");
            Mapper.Initialize(cfr => cfr.CreateMap<UserDTO, User>());
            User user = Mapper.Map<UserDTO, User>(userDTO);
            Database.User.Update(user);
        }
    }
}
