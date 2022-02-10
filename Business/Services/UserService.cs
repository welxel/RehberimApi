using AppCore.Business.Models.Results;
using Business.Models;
using Business.Services.Bases;
using DataAccess.EntityFramework.Repositories.Bases;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services {
    public class UserService : IUserService {

        private readonly IUsersBaseRepository _userRepository;
        IUsersInformationBaseRepository _usersInformationBaseRepository;
        public UserService(IUsersBaseRepository userRepository,IUsersInformationBaseRepository usersInformationBaseRepository) {
            _userRepository = userRepository;
            _usersInformationBaseRepository = usersInformationBaseRepository;
        }

        public Result Add(UsersModel model) {
            try {
                if (model != null) {
                    Users user;
                    string GUID = Guid.NewGuid().ToString();

                    if (model.userInfo != null) {
                        user = new Users {
                            Ad = model.Ad,
                            Firma = model.Firma,
                            Soyad = model.Soyad,
                            GUID = GUID,
                            Status = true,
                            userInfo = new UserInformation {
                                Email = model.userInfo.Email,
                                Lang = model.userInfo.Lang,
                                Lat = model.userInfo.Lat,
                                TelNo = model.userInfo.TelNo,
                            }

                        };
                    } else {
                        user = new Users {
                            Ad = model.Ad,
                            Firma = model.Firma,
                            Soyad = model.Soyad,
                            GUID = GUID,
                            Status = true,
                        };
                    }
                    _userRepository.Add(user);
                    return new SuccessResult<Users>("Rehbere kişi başarıyla eklendi.", user);
                }
                return new ErrorResult("Request Body boş gönderilemez");
            } catch (Exception e) {

                return new SuccessResult<Users>("Birşeyler Yanlış Gitti.");
            }
        }

        public Result Delete(int id) {
            var user = _userRepository.GetEntityQuery().Include(x => x.userInfo).Where(x => x.Id == id).FirstOrDefault();
                if (user != null) {
                  try {
                    _userRepository.Delete(user.Id);
                    return new SuccessResult("İşlem başarıyla gerçekleşmiştir.");
                   } catch (Exception) {
                    return new ErrorResult("Bir şeyler yanlış gitti");
                 }
                }
                return new ErrorResult("Gönderilen guid ile eşleşen bir kayıt bulunmamaktadır.");
        }

        public void Dispose() {
            _userRepository.Dispose();
        }

        public Result<IQueryable<UsersModel>> GetQuery() {
            try {
                var users = _userRepository.GetEntityQuery().Include(x => x.userInfo).Where(x=>x.Status==true).Select(e => new UsersModel(){
                    Ad = e.Ad,
                    Firma = e.Firma,
                    Id = e.Id,
                    Soyad = e.Soyad,
                    Status=e.Status,
                    userInfo = e.userInfo,
                });
                return new SuccessResult<IQueryable<UsersModel>>("İşlem başarıyla gerçekleşmiştir.", data: users);
            } catch (Exception) {

                return new ErrorResult<IQueryable<UsersModel>>("Bir hata oluştu");
            }
         
        }

        public Result Remove(UsersModel model) {
            throw new NotImplementedException();
        }

        public Result Update(UsersModel model) {
            var result = _userRepository.GetEntityQuery().Include(x => x.userInfo).Where(x => x.Id == model.Id).FirstOrDefault();
            if (result != null) {
                result.Soyad = model.Soyad;
                result.Ad = model.Ad;
                result.Firma = model.Firma;
                if (model.userInfo != null) {
                    result.userInfo.Lang = model.userInfo.Lang;
                    result.userInfo.Lat = model.userInfo.Lat;
                    result.userInfo.TelNo = model.userInfo.TelNo;
                    result.userInfo.Email = model.userInfo.Email;
                }
                _userRepository.Update(result);
                return new SuccessResult<Users>("İşlem başarılıdır.", result);
            }
            return new ErrorResult<Users>("Gönderilen id ile eşleşen kayıt bulunmamaktadır.", result);
        }

        public Result IgnoreUser(int id) {
            try {
                var result = _userRepository.GetEntityQuery().Include(x => x.userInfo).Where(x => x.Id == id).FirstOrDefault();
                if (result != null) {
                    result.Status = false;
                    _userRepository.Update(result);
                    return new SuccessResult<Users>("İşlem başarılıdır.", result);
                }
                return new ErrorResult("Gönderilen id ile ilgili bir kayıt bulunmamaktadır.");
            } catch (Exception) {

                return new ErrorResult("Birşeyler yanlış gitti.");
            }
        }

        public Result GetUserById(int id) {
            if (id != 0) {
               var result=_userRepository.GetEntityQuery().Include(x => x.userInfo).Where(x => x.Id == id).FirstOrDefault();
                return new SuccessResult<Users>("İşlem başarılıdır.", result);
            }
            return new ErrorResult("userid değeri url parametre olarak gönderilmelidir.");
        }
    }
}
