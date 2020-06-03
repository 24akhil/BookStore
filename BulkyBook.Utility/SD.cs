using System;

namespace BulkyBook.Utility
{
    public static class SD
    {
        #region Stored Procedures
        public const string ProcCoverTypeGetTypes = "usp_GetCoverTypes";
        public const string ProcCoverTypeGetType = "usp_GetCoverType";
        public const string ProcCoverTypeUpdate = "usp_UpdateCoverType";
        public const string ProcCoverTypeDelete = "usp_DeleteCoverType";
        public const string ProcCoverTypeCreate = "usp_CreateCoverType";
        #endregion

        #region User Roles
        public const string Role_Employee = "Employee";
        public const string Role_Admin = "Admin";
        public const string Role_User_Indi = "Individual Customer";
        public const string Role_User_Comp = "Company Customer";
        #endregion

        public const string ssShoppingCart = "Shoping Cart Session";

        public static double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity < 50)
            {
                return price;
            }
            else
            {
                if (quantity < 100)
                {
                    return price50;
                }
                else
                {
                    return price100;
                }
            }
        }

        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}