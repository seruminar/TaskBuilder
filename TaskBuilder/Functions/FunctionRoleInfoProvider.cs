using System;
using System.Linq;

using CMS.DataEngine;

namespace TaskBuilder
{    
    /// <summary>
    /// Class providing <see cref="FunctionRoleInfo"/> management.
    /// </summary>
    public partial class FunctionRoleInfoProvider : AbstractInfoProvider<FunctionRoleInfo, FunctionRoleInfoProvider>
    {
        /// <summary>
        /// Returns all <see cref="FunctionRoleInfo"/> bindings.
        /// </summary>
        public static ObjectQuery<FunctionRoleInfo> GetFunctionRoles()
        {
            return ProviderObject.GetObjectQuery();
        }


		/// <summary>
        /// Returns <see cref="FunctionRoleInfo"/> binding structure.
        /// </summary>
        /// <param name="functionId">ObjectType.taskbuilder_function ID.</param>
        /// <param name="roleId">Role ID.</param>  
        public static FunctionRoleInfo GetFunctionRoleInfo(int functionId, int roleId)
        {
            return ProviderObject.GetObjectQuery().TopN(1)
                .WhereEquals("FunctionID", functionId)
                .WhereEquals("RoleID", roleId)
				.FirstOrDefault();
        }


        /// <summary>
        /// Sets specified <see cref="FunctionRoleInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="FunctionRoleInfo"/> to set.</param>
        public static void SetFunctionRoleInfo(FunctionRoleInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }


        /// <summary>
        /// Deletes specified <see cref="FunctionRoleInfo"/> binding.
        /// </summary>
        /// <param name="infoObj"><see cref="FunctionRoleInfo"/> object.</param>
        public static void DeleteFunctionRoleInfo(FunctionRoleInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }


        /// <summary>
        /// Deletes <see cref="FunctionRoleInfo"/> binding.
        /// </summary>
        /// <param name="functionId">ObjectType.taskbuilder_function ID.</param>
        /// <param name="roleId">Role ID.</param>  
        public static void RemoveFunctionFromRole(int functionId, int roleId)
        {
            var infoObj = GetFunctionRoleInfo(functionId, roleId);
			if (infoObj != null) 
			{
				DeleteFunctionRoleInfo(infoObj);
			}
        }


        /// <summary>
        /// Creates <see cref="FunctionRoleInfo"/> binding.
        /// </summary>
        /// <param name="functionId">ObjectType.taskbuilder_function ID.</param>
        /// <param name="roleId">Role ID.</param>   
        public static void AddFunctionToRole(int functionId, int roleId)
        {
            // Create new binding
            var infoObj = new FunctionRoleInfo();
            infoObj.FunctionID = functionId;
			infoObj.RoleID = roleId;

            // Save to the database
            SetFunctionRoleInfo(infoObj);
        }
    }
}