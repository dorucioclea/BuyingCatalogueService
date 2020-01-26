using System;

namespace NHSD.BuyingCatalogue.Testing.Data.Entities
{
    public sealed class SupplierEntity : EntityBase
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LegalName { get; set; }

        public string Summary { get; set; }

        public string SupplierUrl { get; set; }

        public string Address { get; set; }

        public string OdsCode { get; set; }

        public Guid? CrmRef { get; set; }

        public bool Deleted { get; set; }

        protected override string InsertSql => $@"
            INSERT INTO [dbo].[Supplier]
            ([Id]
            ,[Name]
            ,[LegalName]
            ,[Summary]
            ,[SupplierUrl]
            ,[Address]
            ,[OdsCode]
            ,[CrmRef]
            ,[Deleted]
            ,[LastUpdated]
            ,[LastUpdatedBy])
            VALUES
            (@Id
            ,@Name
            ,@LegalName
            ,@Summary
            ,@SupplierUrl
            ,@Address
            ,@OdsCode
            ,@CrmRef
            ,@Deleted
            ,@LastUpdated
            ,@LastUpdatedBy
            )
        ";
    }
}


