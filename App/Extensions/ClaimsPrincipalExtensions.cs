﻿using System.Security.Claims;

namespace App.Extensions;


// este archivo es todo lo q se necesita p' tener la extension,
// NO hay q dar de alta nada en program.cs ni 
// nada por el estilo, se hace esta clase y ya se puede ocupar
public static class ClaimsPrincipalExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Name)?.Value;
    }

    public static int GetUserId(this ClaimsPrincipal user)
    {
        return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}

