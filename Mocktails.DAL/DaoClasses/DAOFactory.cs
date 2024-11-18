using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocktails.DAL.DaoClasses;
public static class DAOFactory
{
    /// <summary>
    /// A factory which can create a specific repository.
    /// This centralizes the creation functionality here (high coupling)
    /// and thereby lowering the coupling in other parts of the code
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connectionstring"></param>
    /// <returns></returns>
    /// 
    public static T CreateRepository<T> (string connectionString) where T : class
    {
        if (typeof(T) == typeof(IMocktailDAO)) return new MocktailDAO(connectionString) as T;

        throw new ArgumentException($"Unknown type {typeof(T).FullName}");
    }
}
