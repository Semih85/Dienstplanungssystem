using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WM.EczaneNobet.WebApi.Models;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.EczaneNobet.WebApi.MessageHandlers
{
    public class Yetkilendirme
    {
        private IUserService _userService;
        private IUserRoleService _userRoleService;
        public Yetkilendirme(IUserService userService,
                                IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;

        }
        public Yetkilendirme()
        {
          
        }
        public string SHA256(string strGiris)
        {
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Veri Yok");
            }
            else
            {
                SHA256Managed sifre = new SHA256Managed();
                byte[] arySifre = StringToByte(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                var hash = BitConverter.ToString(aryHash);
                return hash.Replace("-", "");
            }
        }

        public static byte[] StringToByte(string deger)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(deger);
        }

        public void YetkiKontrolu(UserApi userApi, out LoginItem loginUser, out User user)
        {
            loginUser = new LoginItem { Email = userApi.Username, Password = SHA256(userApi.Password), RememberMe = true };
            user = _userService.GetByEMailAndPassword(loginUser);
        }

        public void YetkiKontrolu(EczaneNobetDegisimTalepApi eczaneNobetDegisimTalepApi, out LoginItem loginUser, out User user)
        {            
            user = _userService.GetById(eczaneNobetDegisimTalepApi.UserId);
            loginUser = new LoginItem { Email = user.Email, Password = SHA256(user.Password), RememberMe = true };
            //user = _userService.GetByEMailAndPassword(loginUser);
            
        }
        public void YetkiKontrolu(EczaneNobetDegisimArzApi eczaneNobetDegisimTArzApi, out LoginItem loginUser, out User user)
        {
            user = _userService.GetById(eczaneNobetDegisimTArzApi.UserId);
            loginUser = new LoginItem { Email = user.Email, Password = SHA256(user.Password), RememberMe = true };
            //user = _userService.GetByEMailAndPassword(loginUser);

        }
        public void YetkiKontrolu(EczaneNobetDegisimApi eczaneNobetDegisimApi, out LoginItem loginUser, out User user)
        {
            user = _userService.GetById(eczaneNobetDegisimApi.UserId);
            loginUser = new LoginItem { Email = user.Email, Password = SHA256(user.Password), RememberMe = true };
            //user = _userService.GetByEMailAndPassword(loginUser);

        }
        public void YetkiKontrolu(EczaneNobetMazeretApi eczaneNobetMazeretApi, out LoginItem loginUser, out User user)
        {
            user = _userService.GetById(eczaneNobetMazeretApi.UserId);
            loginUser = new LoginItem { Email = user.Email, Password = SHA256(user.Password), RememberMe = true };
            //user = _userService.GetByEMailAndPassword(loginUser);
        }

        public void YetkiKontrolu(EczaneNobetMazeretCokluApi eczaneNobetMazeretCokluApi, out LoginItem loginUser, out User user)
        {
            user = _userService.GetById(eczaneNobetMazeretCokluApi.UserId);
            loginUser = new LoginItem { Email = user.Email, Password = SHA256(user.Password), RememberMe = true }; 
            //user = _userService.GetByEMailAndPassword(loginUser);

        }

        public string GetToken(LoginItem loginUser)
        {
            var jsonString = JsonConvert.SerializeObject(loginUser);
            var token = FTH.Extension.Encrypter.Encrypt(jsonString, loginUser.Password);
            return token;
        }
        public string GetToken2(LoginItem loginUser)
        {
            var token = SHA256(loginUser.ToString());
            return token;
        }
    }
}