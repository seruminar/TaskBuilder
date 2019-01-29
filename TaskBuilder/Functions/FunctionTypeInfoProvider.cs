using System;

using CMS.DataEngine;

namespace TaskBuilder
{
    /// <summary>
    /// Class providing <see cref="FunctionTypeInfo"/> management.
    /// </summary>
    public partial class FunctionTypeInfoProvider : AbstractInfoProvider<FunctionTypeInfo, FunctionTypeInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="FunctionTypeInfoProvider"/>.
        /// </summary>
        public FunctionTypeInfoProvider()
            : base(FunctionTypeInfo.TYPEINFO)
        {
        }

        /// <summary>
        /// Returns a query for all the <see cref="FunctionTypeInfo"/> objects.
        /// </summary>
        public static ObjectQuery<FunctionTypeInfo> GetFunctionTypes()
        {
            return ProviderObject.GetObjectQuery();
        }

        /// <summary>
        /// Returns <see cref="FunctionTypeInfo"/> with specified GUID.
        /// </summary>
        /// <param name="guid"><see cref="FunctionTypeInfo"/> GUID.</param>
        public static FunctionTypeInfo GetFunctionTypeInfo(Guid guid)
        {
            return ProviderObject.GetInfoByGuid(guid);
        }

        /// <summary>
        /// Sets (updates or inserts) specified <see cref="FunctionTypeInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="FunctionTypeInfo"/> to be set.</param>
        public static void SetFunctionTypeInfo(FunctionTypeInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }

        /// <summary>
        /// Deletes specified <see cref="FunctionTypeInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="FunctionTypeInfo"/> to be deleted.</param>
        public static void DeleteFunctionTypeInfo(FunctionTypeInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }

        /// <summary>
        /// Deletes <see cref="FunctionTypeInfo"/> with specified GUID.
        /// </summary>
        /// <param name="guid"><see cref="FunctionTypeInfo"/> GUID.</param>
        public static void DeleteFunctionTypeInfo(Guid guid)
        {
            FunctionTypeInfo infoObj = GetFunctionTypeInfo(guid);
            DeleteFunctionTypeInfo(infoObj);
        }
    }
}