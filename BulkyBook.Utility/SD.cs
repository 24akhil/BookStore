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

    }
}