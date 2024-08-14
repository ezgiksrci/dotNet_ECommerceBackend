﻿using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductGetted = "Ürün getirildi.";
        public static string ProductNameInvalid = "Ürün ismi geçersizdir.";
        public static string ProductDeleted = "Ürün silindi.";
        public static string ProductUpdated = "Ürün güncellendi.";
        public static string MaintenanceTime = "Sistem bakım zamanı.";
        public static string ProductsListed = "Ürünler listelendi.";
        public static string CategoryLimitExceeded = "Toplam kategori sayısı aşıldı.";
        public static string ProductNameAlreadyExists = "Bu isimde ürün zaten var.";
        public static string CategoryContainsTooManyProduct = "Bu kategorideki ürün sayısı sınırına ulaşıldı. Daha fazla ürün eklenemiyor.";
        public static string AuthorizationDenied = "Bu işleme yetkiniz yok.";
        public static string AccessTokenCreated = "Token oluşturuldu.";
        public static string UserAlreadyExists = "Kullanıcı zaten mevcut.";
        public static string SuccessfulLogin = "Başarıyla giriş yapıldı.";
        public static string PasswordError = "Parola hatalı.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string UserRegistered = "Kullanıcı kaydı başarılı.";
    }
}
