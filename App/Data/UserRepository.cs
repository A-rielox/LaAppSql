﻿using AutoMapper;
using CloudinaryDotNet;
using Dapper;
using App.Entities;
using App.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace App.Data;

public class UserRepository : IUserRepository
{
    private IDbConnection db;
    private readonly IMapper _mapper;

    public UserRepository(IConfiguration configuration, IMapper mapper)
    {
        this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        _mapper = mapper;
    }



    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    /// LA OCUPO EN AppUserStore
    public async Task<int> CreateUser(AppUser usuario)  // CrearUsuario / Register
    {   
        // creo user con sp y lo pongo con role 'Member'
        var parameters = new DynamicParameters();

        parameters.Add("@userName", usuario.UserName);
        parameters.Add("@knownAs", usuario.KnownAs);
        //parameters.Add("@gender", usuario.Gender);
        //parameters.Add("@dateOfBirth", usuario.DateOfBirth);
        parameters.Add("@city", usuario.City);
        parameters.Add("@country", usuario.Country);
        parameters.Add("@passwordHash", usuario.PasswordHash);

        parameters.Add("@email", usuario.UserName);
        parameters.Add("@normalizedEmail", usuario.UserName.ToUpper());

        // retorno el userId creado
        var newUserId = await db.QueryAsync<int>("sp_createUser",
                                               parameters,
                                               commandType: CommandType.StoredProcedure);
        return newUserId.FirstOrDefault();
    }

    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public async Task<bool> UpdateUserAsync(AppUser user)
    {
        // si es exitosa me retorna 1 ( la cantidad de cols editadas )
        var parameters = new DynamicParameters();

        parameters.Add("@userId", user.Id);
        parameters.Add("@introduction", user.Introduction);
        //parameters.Add("@lookingFor", user.LookingFor);
        parameters.Add("@interests", user.Interests);
        parameters.Add("@city", user.City);
        parameters.Add("@country", user.Country);
        parameters.Add("@lastActive", user.LastActive);

        var res = await db.QuerySingleAsync<int>("sp_updateUser",
                                            parameters,
                                            commandType: CommandType.StoredProcedure);

        return res == 1 ? true : false;
    }

    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///
    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        var user = await db.QuerySingleAsync<AppUser>("sp_getUserById",
                                    new { userId = id },
                                    commandType: CommandType.StoredProcedure);

        return user;
    }


    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///     LA OCUPO EN AppUserStore - BuscarUsuarioPorEmail
    public async Task<AppUser> GetUserByUserNameStoreAsync(string userName)
    {
        var user = await db.QuerySingleOrDefaultAsync<AppUser>("sp_getUserByUserNameStore",
                                                  new { userName = userName },
                                                  commandType: CommandType.StoredProcedure);

        return user;
    }

    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///                                             UsersController
    public async Task<AppUser> GetUserByUserNameAsync(string userName)
    {
        AppUser user;
        List<Picture> photos;

        using (var lists = await db.QueryMultipleAsync("sp_getUserByUserName",
                                    new { userName = userName },
                                    commandType: CommandType.StoredProcedure))
        {
            user = lists.Read<AppUser>().SingleOrDefault();
            photos = lists.Read<Picture>().ToList();

            if (user is not null && photos.Count > 0)
            {
                user.Pictures = photos;
            }
        }

        return user;
    }

    ////////////////////////////////////////////////
    ////////////////////////////////////////////////
    ///                                             UsersController
    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        List<AppUser> users;
        List<Picture> photos;

        using (var lists = await db.QueryMultipleAsync("sp_getAllUsersAndPictures",
                                    commandType: CommandType.StoredProcedure))
        {
            users = lists.Read<AppUser>().ToList();
            photos = lists.Read<Picture>().ToList();
        }

        users.ForEach(u =>
        {
            u.Pictures = photos.Where(p => p.AppUserId == u.Id)
                             .ToList();
        });

        return users;



        /*          SIN FOTOS 
        using var connection = new SqlConnection(_connectionString);

        var users = await connection.QueryAsync<AppUser>("sp_getAllUsers",
                                    commandType: CommandType.StoredProcedure);

        return users.ToList();
        */
    }


    //public async Task<AppUserPagedList> GetPagedUsersAsync(UserParams userParams)
    //{ // a cambio de GetMembersAsync
    //    var pagedResult = await AppUserPagedList.CreateAsync(db, userParams.PageNumber, userParams.PageSize,
    //                                                         userParams.CurrentUsername, userParams.Gender,
    //                                                         userParams.MinAge, userParams.MaxAge,
    //                                                         userParams.OrderBy);

    //    return pagedResult;
    //}

    //////////////////////////////////////////////////////////////////
    //              PHOTOS
    //////////////////////////////////////////////////////////////////


}