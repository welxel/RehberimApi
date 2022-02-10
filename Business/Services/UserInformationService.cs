
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
    public class UserInformationService : IUserInformationService {

        private readonly IUsersInformationBaseRepository _usersInformationBaseRepository;
        public UserInformationService(IUsersInformationBaseRepository usersInformationBaseRepository) {
            _usersInformationBaseRepository = usersInformationBaseRepository;
        }

        public Result Add(UserInformationModel model) {
            if (model!=null) {
                var user = _usersInformationBaseRepository.GetEntityQuery().Include(x => x.Users).Where(x => x.UserId == model.UserId).FirstOrDefault();
                if (user == null) {
                    UserInformation info = new UserInformation() {
                        UserId = model.UserId,
                        Email = model.Email,
                        Lang = model.Lang,
                        Lat = model.Lat,
                        TelNo = model.TelNo
                    };
                    _usersInformationBaseRepository.Add(info);
                    return new SuccessResult("İşlem başarıyla gerçekleşmiştir.");
                }
                return new ErrorResult("Bu kişiye ait bilgiler vardır. Update methodunu kullanınız.");
            }
            return new ErrorResult("Request body göndermelisiniz.");  
        }

        public Result Delete(int id) {
            if (id!=0) {
                var user = _usersInformationBaseRepository.GetEntityQuery().Where(x => x.UserId == id).FirstOrDefault();
                if (user != null) {
                    _usersInformationBaseRepository.Delete(user.Id);
                    return new SuccessResult("İşlem başarıyla gerçekleşmiştir.");
                }
                return new ErrorResult("Gönderilen id ile ilşkili bir kişi bilgisi bulunmamaktadır.");
            }
            return new ErrorResult("UserId bilgisi url parametre olarak gönderilmelidir.");
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public Result<IQueryable<UserInformationModel>> GetQuery() {
            throw new NotImplementedException();
        }

        public Result Remove(UserInformationModel model) {
            throw new NotImplementedException();
        }

        public Result Update(UserInformationModel model) {
            throw new NotImplementedException();
        }
    }
}
