using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product added.";
        public static string ProductGetted = "Product retrieved.";
        public static string ProductNameInvalid = "Product name is invalid.";
        public static string ProductDeleted = "Product deleted.";
        public static string ProductUpdated = "Product updated.";
        public static string MaintenanceTime = "System maintenance time.";
        public static string ProductsListed = "Products listed.";
        public static string CategoryLimitExceeded = "Category limit exceeded.";
        public static string ProductNameAlreadyExists = "A product with this name already exists.";
        public static string CategoryContainsTooManyProduct = "The number of products in this category has reached its limit. No more products can be added.";
        public static string AuthorizationDenied = "You do not have permission for this operation.";
        public static string AccessTokenCreated = "Token created.";
        public static string UserAlreadyExists = "User already exists.";
        public static string SuccessfulLogin = "Login successful.";
        public static string PasswordError = "Incorrect password.";
        public static string UserNotFound = "User not found.";
        public static string UserRegistered = "User registration successful.";
    }
}
