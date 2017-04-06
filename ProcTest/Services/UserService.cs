using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ProcTest.Models;
using ProcTest.Models.Database;
using ProcTest.Models.ViewModels;

namespace ProcTest.Services
{
    public class UserService : BaseService
    {
        public QueryResult<LoginData> Authenticate(string userName, string password)
        {
            var hash = GetHash(password);
            return Authenticate(userName, hash);
        }

        public QueryResult<LoginData> Authenticate(string userName, byte[] hash)
        {
            string password = GetStringFromBytes(hash);
            var p = new DynamicParameters();
            p.Add("@Login", userName, size: 40);
            p.Add("@Exists", direction: ParameterDirection.Output, size: 1);
            p.Add("@Error", direction: ParameterDirection.Output, size: 40);
            p.Add("@Password", password, size: 40);
            var res = Connection.Query<User>("CheckUser", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            var ex = p.Get<dynamic>("@Exists");
            if (p.Get<dynamic>("@Exists") != "0")
            {
                return new QueryResult<LoginData>(
                    new LoginData
                    {
                        Token = CreateToken(res.Login, res.Password),
                        UserName = userName,
                        ID = res.ID
                    }
                    );
            }
            else
            {
                return new QueryResult<LoginData>(p.Get<dynamic>("@Error"));
            }
        }

        private static string GetStringFromBytes(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }

        public QueryResult<User> InsertUser(UserRegisterViewModel userModel)
        {
            var p = new DynamicParameters();
            p.Add("@Login", userModel.Login, size: 40);
            p.Add("@FullName", userModel.FullName, size: 40);
            p.Add("@Password", GetStringFromBytes(GetHash(userModel.Password)), size: 40);
            p.Add("@Error", direction: ParameterDirection.Output, size: 40);
            p.Add("@ErrorCode", direction: ParameterDirection.Output, dbType: DbType.Int32);

            var user = Connection.Query<User>("InsertUser", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            var error = p.Get<string>("@Error");
            var errorCode = p.Get<dynamic>("@ErrorCode");

            if (errorCode == 1)
            {
                return new QueryResult<User>(error);
            }
            else
            {
                return new QueryResult<User>(user);
            }
        }

        private string CreateToken(string username, string password)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(username+":"+password);
            return Convert.ToBase64String(plainTextBytes);
        }
        
        private byte[] GetHash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return checkSum;
        }
    }
}