using App.Entities;

namespace App.Interfaces;

public interface IUserRepository
{
    Task<bool> UpdateUserAsync(AppUser user);
    Task<IEnumerable<AppUser>> GetUsersAsync(); //   UsersController
    //Task<AppUserPagedList> GetPagedUsersAsync(UserParams userParams); // reemplaza a la de arriba
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByUserNameAsync(string username);  //   UsersController
    Task<AppUser> GetUserByUserNameStoreAsync(string username);  //  LA OCUPO EN AppUserStore - BuscarUsuarioPorEmail


    Task<int> CreateUser(AppUser usuario); // CrearUsuario


    //Task<int> AddPhotoAsync(Photo photo);
    //Task<bool> UpdatePhotos(SetMainPhoto setMainPhoto);
    //Task<bool> DeletePhoto(int id);
}

// ANTIGUO
//Task<bool> UpdatePhotos(List<Photo> photos);
