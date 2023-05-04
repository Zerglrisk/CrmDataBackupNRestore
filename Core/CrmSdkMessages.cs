using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class CrmSdkMessages
    {
        /// <summary>
        /// Contains the data that is needed to retrieve entity metadata.
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/api/microsoft.xrm.sdk.messages.retrieveentityrequest?view=dynamics-general-ce-9"/>
        /// <param name="service"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="entityFilters"></param>
        /// <returns></returns>
        public static EntityMetadata RetrieveEntity(IOrganizationService service, string entityLogicalName,
            EntityFilters entityFilters = EntityFilters.Default)
        {
            try
            {
                var response = (RetrieveEntityResponse)service.Execute(new RetrieveEntityRequest()
                {
                    EntityFilters = entityFilters,
                    LogicalName = entityLogicalName
                });
                return response.EntityMetadata;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
