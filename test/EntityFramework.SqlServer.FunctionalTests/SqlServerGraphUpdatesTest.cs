// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.SqlServer.FunctionalTests
{
    public class SqlServerGraphUpdatesTest : SqlServerGraphUpdatesTestBase<SqlServerGraphUpdatesTest.SqlServerGraphUpdatesFixture>
    {
        public SqlServerGraphUpdatesTest(SqlServerGraphUpdatesFixture fixture)
            : base(fixture)
        {
        }

        public class SqlServerGraphUpdatesFixture : SqlServerGraphUpdatesFixtureBase
        {
            protected override string DatabaseName => "GraphUpdatesTest";

            public override int IntSentinel => 0;

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.ForSqlServer().UseSequence();
            }
        }
    }
}
