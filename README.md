      

	  Rehberim Api Dökümantasion
	   
  1. Database Kurulumu

Proje entity framework code-first kullanılarak yazılmıştır. Proje locale aldındıktan sonra DAtaAccess-EntityFramework-Contexts-ETradeContext
sınıfı aldında ConnectionConfig.ConnectionString parametresini kendi localinizde ki mssql connection stringiyle değiştirmemiz gerekiyor.

Sonrasında package-manager console açıyoruz. Default project olarak dataaccess seçilmelidir. add-migration [herhangibirisim] çalıştırılmalıdır.
Sonrasında update-database çalıştırılmalıdır ve databaseimiz oluşmuş olacaktır.

  2. Methodların Kullanımı 

İlk olarak default project olarak RehberimApi seçilerek proje localde çalıştırılır.

[localadresiniz:portnumaranız]=Localde çalışan url adresiniz ve port numaranızdır.
[id]= Sistemde kayıtlı usera ait id bilgisidir. (2.5 de belirtilen method kullanılarak ulaşılabilir.)


2.1 Rehberde Kişi Oluşturma

Method tanımı: Rehbere yeni bir kayıt oluşturmak için kullanılan methoddur. Bu methodda istersek sadece kişi veya
o kişiye ait bilgiler girmek istersek de girebiliriz. Çalışma şekli bu şekildedir.
Method: POST
Url: https://[localadresiniz:portnumaranız]/api/personoperation/addperson

Request Body örnek dizayn:
{
  "ad": "Furkan",
  "soyad": "Bektas",
  "firma": "TestFirma",
  "userInfo":{
  "telNo":"222",
  "email":"test@gmail.com",
  "lat":10.0,
  "lang":10.0,
  }
}

2.2 Rehberde Kişi Kaldırma

Method tanımı: Rehberde belirtilen kişinin listesinde görünmemesi için kullanılan methoddur. Engellenmesini istediğimiz kişinin id'sini rehberenizde ki kişileri 
görmek için olan methoddan bakarak bulabilirsiniz.
Method:PUT

Url: https://[localadresiniz:portnumaranız]/api/personoperation/ignoreperson

Request Body örnek dizayn:
{
  "id":2
}

2.3 Rehberde Kişi Güncelleştirme

Url: https://[localadresiniz:portnumaranız]/api/personoperation/updateperson

Method tanımı: Rehberde var olan kişinin bilgilerini değiştirmek için kullanılır. Bunun için kullanıcın idsi gerekemektedir. Eğer kullanıcın database de ilgili bir bilgisi yoksa
ve requestte bilgilerini dolu göndermişseniz bilgilerini ekleyerek update eder.
Method:PUT
Url:
Request Body örnek dizayn:
{
  "idUser":2,
  "ad": "Furkan",
  "soyad": "Bektas",
  "firma": "TestFirma",
  "userInfo":{
  "telNo":"222",
  "email":"test@gmail.com",
  "lat":10.0,
  "lang":10.0,
  }
}

2.4 Rehberden Kişi Silme

Url:https://localhost:44383/api/personoperation/deleteperson/[id]
Method tanımı: Rehberde kişiyi ve ona ait bilgileri silmek için kullanılan methoddur. Query parametre olarak kullanıcı idsi gönderilmelidir.
Method: DELETE

2.5 Rehberde ki Kişilerin Listelenmesi

Url: https://[localadresiniz:portnumaranız]/api/personoperation/getpersons
Method: GET
Method tanımı: Sistemde tanımlı olan kişileri listeleyen methodtur.

2.6 Rehberde ki Kişiye İletişim Bilgisi Ekleme

URL: https://[localadresiniz:portnumaranız]/api/personinfo/addpersoninfo
Method: POST
Method tanımı: Sistemde olan bir kişiye ait bilgi eklemek içiin kullanılan methoddur. Kullanıcın idsi sistemde kayıtlı olmak zorundadır.
Request Body tanımı:
{
  "userid":6,
  "email":"test",
  "telno":"20222"
}

2.7 Rehberde Bir Kişiye Ait İletişim Bilgisi Getirme

URL: https://[localadresiniz:portnumaranız]/api/personoperation/getuserbyid/[id]
Method:GET
Method tanımı: Sistee kayıtlı olan kişiye ait bilgileri getiren methodtur.

2.8 Rehberde ki Kişilerin Konumlarına Göre Raporlandırılması

URL: https://[localadresiniz:portnumaranız]/api/report/GetReportLocation
Method:GET
Method Tanımı: Rehberde ki kişilerin aynı lat ve long özelliklerine göre raporlandırılarak konumda kaç kişinin olduğunu getiren methottur.

   3. Rehberim Test
   
View menüsünden test explorer açılıp test caseleri çalıştırmamız yeterlidir. Arka planda localde RehberimApi projemizi çalıştırıp otomatik kendi verdiğimiz jsonları gönderip dönen sonuçlara göre
testlerin olumlu olup olmamasını kontrol eder. RequestJsons dosyasının altında örnej jsonlarımız vardır. 