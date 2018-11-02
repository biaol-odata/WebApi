// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreODataSample.Web.Models
{
    public static class EdmModelBuilder
    {
        private static IEdmModel _edmModel;

        public static IEdmModel GetEdmModel()
        {
            if (_edmModel == null)
            {
                var builder = new ODataConventionModelBuilder();
                builder.EntitySet<Movie>("Movies");
                _edmModel = builder.GetEdmModel();
            }

            return _edmModel;
        }

        public static IEdmModel GetCompositeModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Person>("People");
            var type = builder.EntitySet<Person>("Person").EntityType;
            type.HasKey(x => new { x.FirstName, x.LastName });
            return builder.GetEdmModel();
        }

        public static IEdmModel GetCompositeModel1()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<AssetUsage>("AssetUsage");
            return builder.GetEdmModel();
        }
 
        public class AssetUsage
        {
            public int Id { get; set; }
 
            [ForeignKey("Asset")]
            public int? AssetId { get; set; }
            public int YearMonthId { get; set; }
            public int Interactions { get; set; }
            public int UniqueInteractions { get; set; }
            public DateTimeOffset Recency { get; set; }
            public virtual Asset Asset { get; set; }
        }
 
        public class Asset
        {
            public int Id { get; set; }
 
            public string Name { get; set; }
        }
    }
}
