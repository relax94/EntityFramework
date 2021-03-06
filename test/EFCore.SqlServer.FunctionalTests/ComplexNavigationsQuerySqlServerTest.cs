// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests.Utilities;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests
{
    public class ComplexNavigationsQuerySqlServerTest
        : ComplexNavigationsQueryTestBase<SqlServerTestStore, ComplexNavigationsQuerySqlServerFixture>
    {
        public ComplexNavigationsQuerySqlServerTest(
            ComplexNavigationsQuerySqlServerFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }

        protected override void ClearLog() => TestSqlLoggerFactory.Reset();

        private bool SupportsOffset => TestEnvironment.GetFlag(nameof(SqlServerCondition.SupportsOffset)) ?? true;

        public override void Entity_equality_empty()
        {
            base.Entity_equality_empty();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l.OneToOne_Optional_FK].[Id], [l.OneToOne_Optional_FK].[Date], [l.OneToOne_Optional_FK].[Level1_Optional_Id], [l.OneToOne_Optional_FK].[Level1_Required_Id], [l.OneToOne_Optional_FK].[Name], [l.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l]
LEFT JOIN [Level2] AS [l.OneToOne_Optional_FK] ON [l].[Id] = [l.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [l.OneToOne_Optional_FK].[Id] = 0
ORDER BY [l].[Id]",
                Sql);
        }

        public override void Key_equality_when_sentinel_ef_property()
        {
            base.Key_equality_when_sentinel_ef_property();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l.OneToOne_Optional_FK].[Id], [l.OneToOne_Optional_FK].[Date], [l.OneToOne_Optional_FK].[Level1_Optional_Id], [l.OneToOne_Optional_FK].[Level1_Required_Id], [l.OneToOne_Optional_FK].[Name], [l.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l]
LEFT JOIN [Level2] AS [l.OneToOne_Optional_FK] ON [l].[Id] = [l.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [l.OneToOne_Optional_FK].[Id] = 0",
                Sql);
        }

        public override void Key_equality_using_property_method_required()
        {
            base.Key_equality_using_property_method_required();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l.OneToOne_Required_FK].[Id], [l.OneToOne_Required_FK].[Date], [l.OneToOne_Required_FK].[Level1_Optional_Id], [l.OneToOne_Required_FK].[Level1_Required_Id], [l.OneToOne_Required_FK].[Name], [l.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l]
LEFT JOIN [Level2] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [l.OneToOne_Required_FK].[Id] > 7",
                Sql);
        }

        public override void Key_equality_using_property_method_required2()
        {
            base.Key_equality_using_property_method_required2();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
WHERE [l].[Level1_Required_Id] > 7",
                Sql);
        }

        public override void Key_equality_using_property_method_nested()
        {
            base.Key_equality_using_property_method_nested();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l.OneToOne_Required_FK].[Id], [l.OneToOne_Required_FK].[Date], [l.OneToOne_Required_FK].[Level1_Optional_Id], [l.OneToOne_Required_FK].[Level1_Required_Id], [l.OneToOne_Required_FK].[Name], [l.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l]
LEFT JOIN [Level2] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [l.OneToOne_Required_FK].[Id] = 7",
                Sql);
        }

        public override void Key_equality_using_property_method_nested2()
        {
            base.Key_equality_using_property_method_nested2();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
WHERE [l].[Level1_Required_Id] = 7",
                Sql);
        }

        public override void Key_equality_using_property_method_and_member_expression1()
        {
            base.Key_equality_using_property_method_and_member_expression1();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l.OneToOne_Required_FK].[Id], [l.OneToOne_Required_FK].[Date], [l.OneToOne_Required_FK].[Level1_Optional_Id], [l.OneToOne_Required_FK].[Level1_Required_Id], [l.OneToOne_Required_FK].[Name], [l.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l]
LEFT JOIN [Level2] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [l.OneToOne_Required_FK].[Id] = 7",
                Sql);
        }

        public override void Key_equality_using_property_method_and_member_expression2()
        {
            base.Key_equality_using_property_method_and_member_expression2();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l.OneToOne_Required_FK].[Id], [l.OneToOne_Required_FK].[Date], [l.OneToOne_Required_FK].[Level1_Optional_Id], [l.OneToOne_Required_FK].[Level1_Required_Id], [l.OneToOne_Required_FK].[Name], [l.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l]
LEFT JOIN [Level2] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [l.OneToOne_Required_FK].[Id] = 7",
                Sql);
        }

        public override void Key_equality_using_property_method_and_member_expression3()
        {
            base.Key_equality_using_property_method_and_member_expression3();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
WHERE [l].[Level1_Required_Id] = 7",
                Sql);
        }

        public override void Key_equality_navigation_converted_to_FK()
        {
            base.Key_equality_navigation_converted_to_FK();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
WHERE [l].[Level1_Required_Id] = 1",
                Sql);
        }

        public override void Key_equality_two_conditions_on_same_navigation()
        {
            base.Key_equality_two_conditions_on_same_navigation();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l.OneToOne_Required_FK].[Id], [l.OneToOne_Required_FK].[Date], [l.OneToOne_Required_FK].[Level1_Optional_Id], [l.OneToOne_Required_FK].[Level1_Required_Id], [l.OneToOne_Required_FK].[Name], [l.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l]
LEFT JOIN [Level2] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE ([l.OneToOne_Required_FK].[Id] = 1) OR ([l.OneToOne_Required_FK].[Id] = 2)",
                Sql);
        }

        public override void Key_equality_two_conditions_on_same_navigation2()
        {
            base.Key_equality_two_conditions_on_same_navigation2();

            Assert.Equal(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
WHERE ([l].[Level1_Required_Id] = 1) OR ([l].[Level1_Required_Id] = 2)",
                Sql);
        }

        public override void Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql()
        {
            base.Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
ORDER BY [e].[Id]

SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [e]
    WHERE [l].[OneToMany_Optional_InverseId] = [e].[Id])
ORDER BY [l].[OneToMany_Optional_InverseId], [l].[Id]

SELECT [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l0]
INNER JOIN (
    SELECT DISTINCT [l].[OneToMany_Optional_InverseId], [l].[Id]
    FROM [Level2] AS [l]
    WHERE EXISTS (
        SELECT 1
        FROM [Level1] AS [e]
        WHERE [l].[OneToMany_Optional_InverseId] = [e].[Id])
) AS [l1] ON [l0].[OneToMany_Optional_InverseId] = [l1].[Id]
ORDER BY [l1].[OneToMany_Optional_InverseId], [l1].[Id]",
                Sql);
        }

        public override void Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times()
        {
            base.Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
ORDER BY [e].[Id]

SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [e]
    WHERE [l].[OneToMany_Optional_InverseId] = [e].[Id])
ORDER BY [l].[OneToMany_Optional_InverseId], [l].[Id]

SELECT [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l0]
INNER JOIN (
    SELECT DISTINCT [l].[OneToMany_Optional_InverseId], [l].[Id]
    FROM [Level2] AS [l]
    WHERE EXISTS (
        SELECT 1
        FROM [Level1] AS [e]
        WHERE [l].[OneToMany_Optional_InverseId] = [e].[Id])
) AS [l1] ON [l0].[OneToMany_Optional_InverseId] = [l1].[Id]
INNER JOIN [Level2] AS [l2] ON [l0].[OneToMany_Required_InverseId] = [l2].[Id]
ORDER BY [l1].[OneToMany_Optional_InverseId], [l1].[Id], [l2].[Id]

SELECT [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_InverseId], [l3].[OneToMany_Optional_Self_InverseId], [l3].[OneToMany_Required_InverseId], [l3].[OneToMany_Required_Self_InverseId], [l3].[OneToOne_Optional_PK_InverseId], [l3].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN (
    SELECT DISTINCT [l1].[OneToMany_Optional_InverseId], [l1].[Id], [l2].[Id] AS [Id0]
    FROM [Level3] AS [l0]
    INNER JOIN (
        SELECT DISTINCT [l].[OneToMany_Optional_InverseId], [l].[Id]
        FROM [Level2] AS [l]
        WHERE EXISTS (
            SELECT 1
            FROM [Level1] AS [e]
            WHERE [l].[OneToMany_Optional_InverseId] = [e].[Id])
    ) AS [l1] ON [l0].[OneToMany_Optional_InverseId] = [l1].[Id]
    INNER JOIN [Level2] AS [l2] ON [l0].[OneToMany_Required_InverseId] = [l2].[Id]
) AS [l20] ON [l3].[OneToMany_Optional_InverseId] = [l20].[Id0]
ORDER BY [l20].[OneToMany_Optional_InverseId], [l20].[Id], [l20].[Id0]",
                Sql);
        }

        public override void Multi_level_include_with_short_circuiting()
        {
            base.Multi_level_include_with_short_circuiting();

            Assert.Equal(
                @"SELECT [x].[Name], [x].[LabelDefaultText], [x].[PlaceholderDefaultText], [c].[DefaultText], [c2].[DefaultText]
FROM [ComplexNavigationField] AS [x]
LEFT JOIN [ComplexNavigationString] AS [c] ON [x].[LabelDefaultText] = [c].[DefaultText]
LEFT JOIN [ComplexNavigationString] AS [c2] ON [x].[PlaceholderDefaultText] = [c2].[DefaultText]
ORDER BY [c].[DefaultText], [c2].[DefaultText]

SELECT [c0].[Text], [c0].[ComplexNavigationStringDefaultText], [c0].[LanguageName], [c1].[Name], [c1].[CultureString]
FROM [ComplexNavigationGlobalization] AS [c0]
LEFT JOIN [ComplexNavigationLanguage] AS [c1] ON [c0].[LanguageName] = [c1].[Name]
WHERE EXISTS (
    SELECT 1
    FROM [ComplexNavigationField] AS [x]
    LEFT JOIN [ComplexNavigationString] AS [c] ON [x].[LabelDefaultText] = [c].[DefaultText]
    WHERE [c0].[ComplexNavigationStringDefaultText] = [c].[DefaultText])
ORDER BY [c0].[ComplexNavigationStringDefaultText]

SELECT [c3].[Text], [c3].[ComplexNavigationStringDefaultText], [c3].[LanguageName], [c4].[Name], [c4].[CultureString]
FROM [ComplexNavigationGlobalization] AS [c3]
LEFT JOIN [ComplexNavigationLanguage] AS [c4] ON [c3].[LanguageName] = [c4].[Name]
WHERE EXISTS (
    SELECT 1
    FROM [ComplexNavigationField] AS [x]
    LEFT JOIN [ComplexNavigationString] AS [c] ON [x].[LabelDefaultText] = [c].[DefaultText]
    LEFT JOIN [ComplexNavigationString] AS [c2] ON [x].[PlaceholderDefaultText] = [c2].[DefaultText]
    WHERE [c3].[ComplexNavigationStringDefaultText] = [c2].[DefaultText])
ORDER BY [c3].[ComplexNavigationStringDefaultText]",
                Sql);
        }

        public override void Join_navigation_key_access_optional()
        {
            base.Join_navigation_key_access_optional();

            Assert.Equal(
                @"SELECT [e1].[Id], [e2].[Id]
FROM [Level1] AS [e1]
INNER JOIN [Level2] AS [e2] ON [e1].[Id] = (
    SELECT TOP(1) [subQuery0].[Id]
    FROM [Level1] AS [subQuery0]
    WHERE [subQuery0].[Id] = [e2].[Level1_Optional_Id]
)",
                Sql);
        }

        public override void Join_navigation_key_access_required()
        {
            base.Join_navigation_key_access_required();

            Assert.Equal(
                @"SELECT [e1].[Id], [e2].[Id]
FROM [Level1] AS [e1]
INNER JOIN [Level2] AS [e2] ON [e1].[Id] = [e2].[Level1_Required_Id]",
                Sql);
        }

        public override void Navigation_key_access_optional_comparison()
        {
            base.Navigation_key_access_optional_comparison();

            Assert.Equal(
                @"SELECT [e2].[Id], [e2].[Date], [e2].[Level1_Optional_Id], [e2].[Level1_Required_Id], [e2].[Name], [e2].[OneToMany_Optional_InverseId], [e2].[OneToMany_Optional_Self_InverseId], [e2].[OneToMany_Required_InverseId], [e2].[OneToMany_Required_Self_InverseId], [e2].[OneToOne_Optional_PK_InverseId], [e2].[OneToOne_Optional_SelfId], [e2.OneToOne_Optional_PK_Inverse].[Id], [e2.OneToOne_Optional_PK_Inverse].[Date], [e2.OneToOne_Optional_PK_Inverse].[Name], [e2.OneToOne_Optional_PK_Inverse].[OneToMany_Optional_Self_InverseId], [e2.OneToOne_Optional_PK_Inverse].[OneToMany_Required_Self_InverseId], [e2.OneToOne_Optional_PK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level2] AS [e2]
LEFT JOIN [Level1] AS [e2.OneToOne_Optional_PK_Inverse] ON [e2].[OneToOne_Optional_PK_InverseId] = [e2.OneToOne_Optional_PK_Inverse].[Id]
WHERE [e2.OneToOne_Optional_PK_Inverse].[Id] > 5",
                Sql);
        }

        public override void Navigation_key_access_required_comparison()
        {
            base.Navigation_key_access_required_comparison();

            Assert.Equal(
                @"SELECT [e2].[Id]
FROM [Level2] AS [e2]
WHERE [e2].[Id] > 5", Sql);
        }

        public override void Navigation_inside_method_call_translated_to_join()
        {
            base.Navigation_inside_method_call_translated_to_join();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Required_FK].[Id], [e1.OneToOne_Required_FK].[Date], [e1.OneToOne_Required_FK].[Level1_Optional_Id], [e1.OneToOne_Required_FK].[Level1_Required_Id], [e1.OneToOne_Required_FK].[Name], [e1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Required_FK] ON [e1].[Id] = [e1.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [e1.OneToOne_Required_FK].[Name] LIKE N'L' + N'%' AND (CHARINDEX(N'L', [e1.OneToOne_Required_FK].[Name]) = 1)",
                Sql);
        }

        public override void Navigation_inside_method_call_translated_to_join2()
        {
            base.Navigation_inside_method_call_translated_to_join2();

            Assert.Equal(
                @"SELECT [e3].[Id], [e3].[Level2_Optional_Id], [e3].[Level2_Required_Id], [e3].[Name], [e3].[OneToMany_Optional_InverseId], [e3].[OneToMany_Optional_Self_InverseId], [e3].[OneToMany_Required_InverseId], [e3].[OneToMany_Required_Self_InverseId], [e3].[OneToOne_Optional_PK_InverseId], [e3].[OneToOne_Optional_SelfId]
FROM [Level3] AS [e3]
INNER JOIN [Level2] AS [e3.OneToOne_Required_FK_Inverse] ON [e3].[Level2_Required_Id] = [e3.OneToOne_Required_FK_Inverse].[Id]
WHERE [e3.OneToOne_Required_FK_Inverse].[Name] LIKE N'L' + N'%' AND (CHARINDEX(N'L', [e3.OneToOne_Required_FK_Inverse].[Name]) = 1)",
                Sql);
        }

        public override void Optional_navigation_inside_method_call_translated_to_join()
        {
            base.Optional_navigation_inside_method_call_translated_to_join();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Optional_FK].[Id], [e1.OneToOne_Optional_FK].[Date], [e1.OneToOne_Optional_FK].[Level1_Optional_Id], [e1.OneToOne_Optional_FK].[Level1_Required_Id], [e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [e1.OneToOne_Optional_FK].[Name] LIKE N'L' + N'%' AND (CHARINDEX(N'L', [e1.OneToOne_Optional_FK].[Name]) = 1)",
                Sql);
        }

        public override void Optional_navigation_inside_property_method_translated_to_join()
        {
            base.Optional_navigation_inside_property_method_translated_to_join();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Optional_FK].[Id], [e1.OneToOne_Optional_FK].[Date], [e1.OneToOne_Optional_FK].[Level1_Optional_Id], [e1.OneToOne_Optional_FK].[Level1_Required_Id], [e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [e1.OneToOne_Optional_FK].[Name] = N'L2 01'",
                Sql);
        }

        public override void Optional_navigation_inside_nested_method_call_translated_to_join()
        {
            base.Optional_navigation_inside_nested_method_call_translated_to_join();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Optional_FK].[Id], [e1.OneToOne_Optional_FK].[Date], [e1.OneToOne_Optional_FK].[Level1_Optional_Id], [e1.OneToOne_Optional_FK].[Level1_Required_Id], [e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE UPPER([e1.OneToOne_Optional_FK].[Name]) LIKE N'L' + N'%' AND (CHARINDEX(N'L', UPPER([e1.OneToOne_Optional_FK].[Name])) = 1)",
                Sql);
        }

        public override void Method_call_on_optional_navigation_translates_to_null_conditional_properly_for_arguments()
        {
            base.Method_call_on_optional_navigation_translates_to_null_conditional_properly_for_arguments();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Optional_FK].[Id], [e1.OneToOne_Optional_FK].[Date], [e1.OneToOne_Optional_FK].[Level1_Optional_Id], [e1.OneToOne_Optional_FK].[Level1_Required_Id], [e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE ([e1.OneToOne_Optional_FK].[Name] LIKE [e1.OneToOne_Optional_FK].[Name] + N'%' AND (CHARINDEX([e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[Name]) = 1)) OR ([e1.OneToOne_Optional_FK].[Name] = N'')",
                Sql);
        }

        public override void Optional_navigation_inside_method_call_translated_to_join_keeps_original_nullability()
        {
            base.Optional_navigation_inside_method_call_translated_to_join_keeps_original_nullability();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Optional_FK].[Id], [e1.OneToOne_Optional_FK].[Date], [e1.OneToOne_Optional_FK].[Level1_Optional_Id], [e1.OneToOne_Optional_FK].[Level1_Required_Id], [e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE DATEADD(day, 10E0, [e1.OneToOne_Optional_FK].[Date]) > '2000-02-01T00:00:00.000'",
                Sql);
        }

        public override void Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability()
        {
            base.Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Optional_FK].[Id], [e1.OneToOne_Optional_FK].[Date], [e1.OneToOne_Optional_FK].[Level1_Optional_Id], [e1.OneToOne_Optional_FK].[Level1_Required_Id], [e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE DATEADD(month, 2, DATEADD(day, 15E0, DATEADD(day, 10E0, [e1.OneToOne_Optional_FK].[Date]))) > '2002-02-01T00:00:00.000'",
                Sql);
        }

        public override void Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability_also_for_arguments()
        {
            base.Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability_also_for_arguments();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Optional_FK].[Id], [e1.OneToOne_Optional_FK].[Date], [e1.OneToOne_Optional_FK].[Level1_Optional_Id], [e1.OneToOne_Optional_FK].[Level1_Required_Id], [e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE DATEADD(day, [e1.OneToOne_Optional_FK].[Id], DATEADD(day, 15E0, [e1.OneToOne_Optional_FK].[Date])) > '2002-02-01T00:00:00.000'",
                Sql);
        }

        public override void Join_navigation_in_outer_selector_translated_to_extra_join()
        {
            base.Join_navigation_in_outer_selector_translated_to_extra_join();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Optional_FK].[Id], [e1.OneToOne_Optional_FK].[Date], [e1.OneToOne_Optional_FK].[Level1_Optional_Id], [e1.OneToOne_Optional_FK].[Level1_Required_Id], [e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [e2].[Id]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
INNER JOIN [Level2] AS [e2] ON [e1.OneToOne_Optional_FK].[Id] = [e2].[Id]",
                Sql);
        }

        public override void Join_navigation_in_outer_selector_translated_to_extra_join_nested()
        {
            base.Join_navigation_in_outer_selector_translated_to_extra_join_nested();

            Assert.Equal(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [e1.OneToOne_Required_FK].[Id], [e1.OneToOne_Required_FK].[Date], [e1.OneToOne_Required_FK].[Level1_Optional_Id], [e1.OneToOne_Required_FK].[Level1_Required_Id], [e1.OneToOne_Required_FK].[Name], [e1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[Id], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Required_Id], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[Name], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [e3].[Id]
FROM [Level1] AS [e1]
LEFT JOIN [Level2] AS [e1.OneToOne_Required_FK] ON [e1].[Id] = [e1.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [Level3] AS [e1.OneToOne_Required_FK.OneToOne_Optional_FK] ON [e1.OneToOne_Required_FK].[Id] = [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
INNER JOIN [Level3] AS [e3] ON [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] = [e3].[Id]",
                Sql);
        }

        public override void Join_navigation_in_outer_selector_translated_to_extra_join_nested2()
        {
            base.Join_navigation_in_outer_selector_translated_to_extra_join_nested2();

            Assert.Equal(
                @"SELECT [e3].[Id], [e3].[Level2_Optional_Id], [e3].[Level2_Required_Id], [e3].[Name], [e3].[OneToMany_Optional_InverseId], [e3].[OneToMany_Optional_Self_InverseId], [e3].[OneToMany_Required_InverseId], [e3].[OneToMany_Required_Self_InverseId], [e3].[OneToOne_Optional_PK_InverseId], [e3].[OneToOne_Optional_SelfId], [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id], [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Date], [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Name], [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_Self_InverseId], [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[OneToMany_Required_Self_InverseId], [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_SelfId], [e1].[Id]
FROM [Level3] AS [e3]
INNER JOIN [Level2] AS [e3.OneToOne_Required_FK_Inverse] ON [e3].[Level2_Required_Id] = [e3.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [Level1] AS [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse] ON [e3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id] = [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id]
INNER JOIN [Level1] AS [e1] ON [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id] = [e1].[Id]",
                Sql);
        }

        public override void Join_navigation_in_inner_selector_translated_to_subquery()
        {
            base.Join_navigation_in_inner_selector_translated_to_subquery();

            Assert.Equal(
                @"SELECT [e2].[Id], [e1].[Id]
FROM [Level2] AS [e2]
INNER JOIN [Level1] AS [e1] ON [e2].[Id] = (
    SELECT TOP(1) [subQuery0].[Id]
    FROM [Level2] AS [subQuery0]
    WHERE [subQuery0].[Level1_Optional_Id] = [e1].[Id]
)",
                Sql);
        }

        public override void Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision()
        {
            base.Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision();

            Assert.Equal(
                @"SELECT [e2].[Id], [e1].[Id], [e3].[Id]
FROM [Level2] AS [e2]
INNER JOIN [Level1] AS [e1] ON [e2].[Id] = (
    SELECT TOP(1) [subQuery0].[Id]
    FROM [Level2] AS [subQuery0]
    WHERE [subQuery0].[Level1_Optional_Id] = [e1].[Id]
)
INNER JOIN [Level3] AS [e3] ON [e2].[Id] = (
    SELECT TOP(1) [subQuery2].[Id]
    FROM [Level2] AS [subQuery2]
    WHERE [subQuery2].[Id] = [e3].[Level2_Optional_Id]
)",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_non_key_join()
        {
            base.Join_navigation_translated_to_subquery_non_key_join();

            Assert.Equal(
                @"SELECT [e2].[Id], [e2].[Name], [e1].[Id], [e1].[Name]
FROM [Level2] AS [e2]
INNER JOIN [Level1] AS [e1] ON [e2].[Name] = (
    SELECT TOP(1) [subQuery0].[Name]
    FROM [Level2] AS [subQuery0]
    WHERE [subQuery0].[Level1_Optional_Id] = [e1].[Id]
)",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_self_ref()
        {
            base.Join_navigation_translated_to_subquery_self_ref();

            Assert.Equal(
                @"SELECT [e1].[Id], [e2].[Id]
FROM [Level1] AS [e1]
INNER JOIN [Level1] AS [e2] ON [e1].[Id] = (
    SELECT TOP(1) [subQuery0].[Id]
    FROM [Level1] AS [subQuery0]
    WHERE [subQuery0].[Id] = [e2].[OneToMany_Optional_Self_InverseId]
)",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_nested()
        {
            base.Join_navigation_translated_to_subquery_nested();

            // Separate asserts to account for ordering differences on .NETCore

            Assert.Contains(
                @"SELECT [e1].[Id]
FROM [Level1] AS [e1]",
                Sql);

            Assert.Contains(
                @"@_outer_Id: 11

SELECT TOP(1) [subQuery].[Id], [subQuery].[Date], [subQuery].[Level1_Optional_Id], [subQuery].[Level1_Required_Id], [subQuery].[Name], [subQuery].[OneToMany_Optional_InverseId], [subQuery].[OneToMany_Optional_Self_InverseId], [subQuery].[OneToMany_Required_InverseId], [subQuery].[OneToMany_Required_Self_InverseId], [subQuery].[OneToOne_Optional_PK_InverseId], [subQuery].[OneToOne_Optional_SelfId], [subQuery.OneToOne_Optional_FK].[Id], [subQuery.OneToOne_Optional_FK].[Level2_Optional_Id], [subQuery.OneToOne_Optional_FK].[Level2_Required_Id], [subQuery.OneToOne_Optional_FK].[Name], [subQuery.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [subQuery.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [subQuery.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [subQuery.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [subQuery.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [subQuery.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level2] AS [subQuery]
LEFT JOIN [Level3] AS [subQuery.OneToOne_Optional_FK] ON [subQuery].[Id] = [subQuery.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE [subQuery].[Level1_Required_Id] = @_outer_Id",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_deeply_nested_non_key_join()
        {
            base.Join_navigation_translated_to_subquery_deeply_nested_non_key_join();

            Assert.Contains(
                @"SELECT [e1].[Id], [e1].[Name]
FROM [Level1] AS [e1]",
                Sql);

            Assert.Contains(@"@_outer_Id: 13

SELECT TOP(1) [subQuery].[Id], [subQuery].[Date], [subQuery].[Level1_Optional_Id], [subQuery].[Level1_Required_Id], [subQuery].[Name], [subQuery].[OneToMany_Optional_InverseId], [subQuery].[OneToMany_Optional_Self_InverseId], [subQuery].[OneToMany_Required_InverseId], [subQuery].[OneToMany_Required_Self_InverseId], [subQuery].[OneToOne_Optional_PK_InverseId], [subQuery].[OneToOne_Optional_SelfId], [subQuery.OneToOne_Optional_FK].[Id], [subQuery.OneToOne_Optional_FK].[Level2_Optional_Id], [subQuery.OneToOne_Optional_FK].[Level2_Required_Id], [subQuery.OneToOne_Optional_FK].[Name], [subQuery.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [subQuery.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [subQuery.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [subQuery.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [subQuery.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [subQuery.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[Id], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[Level3_Optional_Id], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[Level3_Required_Id], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[Name], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[OneToMany_Optional_InverseId], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[OneToMany_Optional_Self_InverseId], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[OneToMany_Required_InverseId], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[OneToMany_Required_Self_InverseId], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[OneToOne_Optional_PK_InverseId], [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[OneToOne_Optional_SelfId]
FROM [Level2] AS [subQuery]
LEFT JOIN [Level3] AS [subQuery.OneToOne_Optional_FK] ON [subQuery].[Id] = [subQuery.OneToOne_Optional_FK].[Level2_Optional_Id]
LEFT JOIN [Level4] AS [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK] ON [subQuery.OneToOne_Optional_FK].[Id] = [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK].[Id]
WHERE [subQuery].[Level1_Required_Id] = @_outer_Id",
                Sql);

            Assert.Contains(
                @"SELECT [e4].[Name], [e4].[Id]
FROM [Level4] AS [e4]",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_deeply_nested_required()
        {
            base.Join_navigation_translated_to_subquery_deeply_nested_required();

            Assert.Equal(
                @"SELECT [e4].[Id], [e4].[Name], [e1].[Id], [e1].[Name]
FROM [Level1] AS [e1]
INNER JOIN [Level4] AS [e4] ON [e1].[Name] = (
    SELECT TOP(1) [subQuery.OneToOne_Required_FK_Inverse.OneToOne_Required_PK_Inverse0].[Name]
    FROM [Level3] AS [subQuery0]
    INNER JOIN [Level2] AS [subQuery.OneToOne_Required_FK_Inverse0] ON [subQuery0].[Level2_Required_Id] = [subQuery.OneToOne_Required_FK_Inverse0].[Id]
    INNER JOIN [Level1] AS [subQuery.OneToOne_Required_FK_Inverse.OneToOne_Required_PK_Inverse0] ON [subQuery.OneToOne_Required_FK_Inverse0].[Id] = [subQuery.OneToOne_Required_FK_Inverse.OneToOne_Required_PK_Inverse0].[Id]
    WHERE [subQuery0].[Id] = [e4].[Level3_Required_Id]
)",
                Sql);
        }

        public override void Multiple_complex_includes()
        {
            base.Multiple_complex_includes();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_InverseId], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_PK_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [l1] ON [l1].[Level1_Optional_Id] = [e].[Id]
ORDER BY [e].[Id], [l1].[Id]

SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
LEFT JOIN [Level3] AS [l0] ON [l0].[Level2_Optional_Id] = [l].[Id]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [e]
    WHERE [l].[OneToMany_Optional_InverseId] = [e].[Id])
ORDER BY [l].[OneToMany_Optional_InverseId]

SELECT [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l2]
INNER JOIN (
    SELECT DISTINCT [e].[Id], [l1].[Id] AS [Id0]
    FROM [Level1] AS [e]
    LEFT JOIN [Level2] AS [l1] ON [l1].[Level1_Optional_Id] = [e].[Id]
) AS [l10] ON [l2].[OneToMany_Optional_InverseId] = [l10].[Id0]
ORDER BY [l10].[Id], [l10].[Id0]",
                Sql);
        }

        public override void Multiple_complex_includes_self_ref()
        {
            base.Multiple_complex_includes_self_ref();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level1] AS [l1] ON [e].[OneToOne_Optional_SelfId] = [l1].[Id]
ORDER BY [e].[Id], [l1].[Id]

SELECT [l2].[Id], [l2].[Date], [l2].[Name], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l2]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [e]
    LEFT JOIN [Level1] AS [l1] ON [e].[OneToOne_Optional_SelfId] = [l1].[Id]
    WHERE [l2].[OneToMany_Optional_Self_InverseId] = [l1].[Id])
ORDER BY [l2].[OneToMany_Optional_Self_InverseId]

SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l]
LEFT JOIN [Level1] AS [l0] ON [l].[OneToOne_Optional_SelfId] = [l0].[Id]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [e]
    WHERE [l].[OneToMany_Optional_Self_InverseId] = [e].[Id])
ORDER BY [l].[OneToMany_Optional_Self_InverseId]",
                Sql);
        }

        public override void Multiple_complex_include_select()
        {
            base.Multiple_complex_include_select();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_InverseId], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_PK_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [l1] ON [l1].[Level1_Optional_Id] = [e].[Id]
ORDER BY [e].[Id], [l1].[Id]

SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
LEFT JOIN [Level3] AS [l0] ON [l0].[Level2_Optional_Id] = [l].[Id]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [e]
    WHERE [l].[OneToMany_Optional_InverseId] = [e].[Id])
ORDER BY [l].[OneToMany_Optional_InverseId]

SELECT [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l2]
INNER JOIN (
    SELECT DISTINCT [e].[Id], [l1].[Id] AS [Id0]
    FROM [Level1] AS [e]
    LEFT JOIN [Level2] AS [l1] ON [l1].[Level1_Optional_Id] = [e].[Id]
) AS [l10] ON [l2].[OneToMany_Optional_InverseId] = [l10].[Id0]
ORDER BY [l10].[Id], [l10].[Id0]",
                Sql);
        }

        public override void Select_nav_prop_reference_optional1()
        {
            base.Select_nav_prop_reference_optional1();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]",
                Sql);
        }

        public override void Select_nav_prop_reference_optional1_via_DefaultIfEmpty()
        {
            base.Select_nav_prop_reference_optional1_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]",
                Sql);
        }

        public override void Select_nav_prop_reference_optional2()
        {
            base.Select_nav_prop_reference_optional2();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]",
                Sql);
        }

        public override void Select_nav_prop_reference_optional2_via_DefaultIfEmpty()
        {
            base.Select_nav_prop_reference_optional2_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]",
                Sql);
        }

        public override void Select_nav_prop_reference_optional3()
        {
            base.Select_nav_prop_reference_optional3();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Level1_Optional_Id], [e].[Level1_Required_Id], [e].[Name], [e].[OneToMany_Optional_InverseId], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_PK_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK_Inverse].[Id], [e.OneToOne_Optional_FK_Inverse].[Date], [e.OneToOne_Optional_FK_Inverse].[Name], [e.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK_Inverse].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level2] AS [e]
LEFT JOIN [Level1] AS [e.OneToOne_Optional_FK_Inverse] ON [e].[Level1_Optional_Id] = [e.OneToOne_Optional_FK_Inverse].[Id]",
                Sql);
        }

        public override void Where_nav_prop_reference_optional1()
        {
            base.Where_nav_prop_reference_optional1();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [e.OneToOne_Optional_FK].[Name] IN (N'L2 05', N'L2 07')",
                Sql);
        }

        public override void Where_nav_prop_reference_optional1_via_DefaultIfEmpty()
        {
            base.Where_nav_prop_reference_optional1_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2Left].[Id], [l2Left].[Date], [l2Left].[Level1_Optional_Id], [l2Left].[Level1_Required_Id], [l2Left].[Name], [l2Left].[OneToMany_Optional_InverseId], [l2Left].[OneToMany_Optional_Self_InverseId], [l2Left].[OneToMany_Required_InverseId], [l2Left].[OneToMany_Required_Self_InverseId], [l2Left].[OneToOne_Optional_PK_InverseId], [l2Left].[OneToOne_Optional_SelfId], [l2Right].[Id], [l2Right].[Date], [l2Right].[Level1_Optional_Id], [l2Right].[Level1_Required_Id], [l2Right].[Name], [l2Right].[OneToMany_Optional_InverseId], [l2Right].[OneToMany_Optional_Self_InverseId], [l2Right].[OneToMany_Required_InverseId], [l2Right].[OneToMany_Required_Self_InverseId], [l2Right].[OneToOne_Optional_PK_InverseId], [l2Right].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2Left] ON [l1].[Id] = [l2Left].[Level1_Optional_Id]
LEFT JOIN [Level2] AS [l2Right] ON [l1].[Id] = [l2Right].[Level1_Optional_Id]
WHERE ([l2Left].[Name] = N'L2 05') OR ([l2Right].[Name] = N'L2 07')",
                Sql);
        }

        public override void Where_nav_prop_reference_optional2()
        {
            base.Where_nav_prop_reference_optional2();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE (([e.OneToOne_Optional_FK].[Name] = N'L2 05') AND [e.OneToOne_Optional_FK].[Name] IS NOT NULL) OR (([e.OneToOne_Optional_FK].[Name] <> N'L2 42') OR [e.OneToOne_Optional_FK].[Name] IS NULL)",
                Sql);
        }

        public override void Where_nav_prop_reference_optional2_via_DefaultIfEmpty()
        {
            base.Where_nav_prop_reference_optional2_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2Left].[Id], [l2Left].[Date], [l2Left].[Level1_Optional_Id], [l2Left].[Level1_Required_Id], [l2Left].[Name], [l2Left].[OneToMany_Optional_InverseId], [l2Left].[OneToMany_Optional_Self_InverseId], [l2Left].[OneToMany_Required_InverseId], [l2Left].[OneToMany_Required_Self_InverseId], [l2Left].[OneToOne_Optional_PK_InverseId], [l2Left].[OneToOne_Optional_SelfId], [l2Right].[Id], [l2Right].[Date], [l2Right].[Level1_Optional_Id], [l2Right].[Level1_Required_Id], [l2Right].[Name], [l2Right].[OneToMany_Optional_InverseId], [l2Right].[OneToMany_Optional_Self_InverseId], [l2Right].[OneToMany_Required_InverseId], [l2Right].[OneToMany_Required_Self_InverseId], [l2Right].[OneToOne_Optional_PK_InverseId], [l2Right].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2Left] ON [l1].[Id] = [l2Left].[Level1_Optional_Id]
LEFT JOIN [Level2] AS [l2Right] ON [l1].[Id] = [l2Right].[Level1_Optional_Id]
WHERE (([l2Left].[Name] = N'L2 05') AND [l2Left].[Name] IS NOT NULL) OR (([l2Right].[Name] <> N'L2 42') OR [l2Right].[Name] IS NULL)",
                Sql);
        }

        public override void Select_multiple_nav_prop_reference_optional()
        {
            base.Select_multiple_nav_prop_reference_optional();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Required_Id], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [e.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [e.OneToOne_Optional_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_member_compared_to_value()
        {
            base.Where_multiple_nav_prop_reference_optional_member_compared_to_value();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE ([l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name] <> N'L3 05') OR [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name] IS NULL",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_member_compared_to_null()
        {
            base.Where_multiple_nav_prop_reference_optional_member_compared_to_null();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name] IS NOT NULL",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null1()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null1();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id] IS NULL",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null2()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null2();

            Assert.Equal(
                @"SELECT [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_InverseId], [l3].[OneToMany_Optional_Self_InverseId], [l3].[OneToMany_Required_InverseId], [l3].[OneToMany_Required_Self_InverseId], [l3].[OneToOne_Optional_PK_InverseId], [l3].[OneToOne_Optional_SelfId], [l3.OneToOne_Optional_FK_Inverse].[Id], [l3.OneToOne_Optional_FK_Inverse].[Date], [l3.OneToOne_Optional_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Optional_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Optional_FK_Inverse].[Name], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
LEFT JOIN [Level2] AS [l3.OneToOne_Optional_FK_Inverse] ON [l3].[Level2_Optional_Id] = [l3.OneToOne_Optional_FK_Inverse].[Id]
WHERE [l3.OneToOne_Optional_FK_Inverse].[Level1_Optional_Id] IS NULL",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null3()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null3();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id] IS NOT NULL",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null4()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null4();

            Assert.Equal(
                @"SELECT [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_InverseId], [l3].[OneToMany_Optional_Self_InverseId], [l3].[OneToMany_Required_InverseId], [l3].[OneToMany_Required_Self_InverseId], [l3].[OneToOne_Optional_PK_InverseId], [l3].[OneToOne_Optional_SelfId], [l3.OneToOne_Optional_FK_Inverse].[Id], [l3.OneToOne_Optional_FK_Inverse].[Date], [l3.OneToOne_Optional_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Optional_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Optional_FK_Inverse].[Name], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
LEFT JOIN [Level2] AS [l3.OneToOne_Optional_FK_Inverse] ON [l3].[Level2_Optional_Id] = [l3.OneToOne_Optional_FK_Inverse].[Id]
WHERE [l3.OneToOne_Optional_FK_Inverse].[Level1_Optional_Id] IS NOT NULL",
                Sql);
        }

        public override void Where_multiple_nav_prop_reference_optional_compared_to_null5()
        {
            base.Where_multiple_nav_prop_reference_optional_compared_to_null5();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Optional_Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Name], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Optional_Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Required_Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[Name], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [e.OneToOne_Optional_FK.OneToOne_Required_FK] ON [e.OneToOne_Optional_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id]
LEFT JOIN [Level4] AS [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK] ON [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Required_Id]
WHERE [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[Id] IS NULL",
                Sql);
        }

        public override void Select_multiple_nav_prop_reference_required()
        {
            base.Select_multiple_nav_prop_reference_required();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK].[Id], [e.OneToOne_Required_FK].[Date], [e.OneToOne_Required_FK].[Level1_Optional_Id], [e.OneToOne_Required_FK].[Level1_Required_Id], [e.OneToOne_Required_FK].[Name], [e.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[Id], [e.OneToOne_Required_FK.OneToOne_Required_FK].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToOne_Required_FK].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToOne_Required_FK].[Name], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Required_FK] ON [e].[Id] = [e.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [Level3] AS [e.OneToOne_Required_FK.OneToOne_Required_FK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Required_FK].[Level2_Required_Id]",
                Sql);
        }

        public override void Select_multiple_nav_prop_reference_required2()
        {
            base.Select_multiple_nav_prop_reference_required2();

            Assert.Equal(
                @"SELECT [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
FROM [Level3] AS [e]
INNER JOIN [Level2] AS [e.OneToOne_Required_FK_Inverse] ON [e].[Level2_Required_Id] = [e.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [Level1] AS [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [e.OneToOne_Required_FK_Inverse].[Level1_Required_Id] = [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]",
                Sql);
        }

        public override void Select_multiple_nav_prop_optional_required()
        {
            base.Select_multiple_nav_prop_optional_required();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Required_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id]",
                Sql);
        }

        public override void Where_multiple_nav_prop_optional_required()
        {
            base.Where_multiple_nav_prop_optional_required();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Required_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id]
WHERE ([l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name] <> N'L3 05') OR [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name] IS NULL",
                Sql);
        }

        public override void SelectMany_navigation_comparison1()
        {
            base.SelectMany_navigation_comparison1();

            Assert.Equal(
                @"SELECT [l11].[Id], [l12].[Id]
FROM [Level1] AS [l11]
CROSS JOIN [Level1] AS [l12]
WHERE [l11].[Id] = [l12].[Id]",
                Sql);
        }

        public override void SelectMany_navigation_comparison2()
        {
            base.SelectMany_navigation_comparison2();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2.OneToOne_Optional_FK_Inverse].[Id], [l2.OneToOne_Optional_FK_Inverse].[Date], [l2.OneToOne_Optional_FK_Inverse].[Name], [l2.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l2.OneToOne_Optional_FK_Inverse].[OneToMany_Required_Self_InverseId], [l2.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_SelfId], [l2].[Id]
FROM [Level1] AS [l1]
CROSS JOIN [Level2] AS [l2]
LEFT JOIN [Level1] AS [l2.OneToOne_Optional_FK_Inverse] ON [l2].[Level1_Optional_Id] = [l2.OneToOne_Optional_FK_Inverse].[Id]
WHERE [l1].[Id] = [l2.OneToOne_Optional_FK_Inverse].[Id]",
                Sql);
        }

        public override void SelectMany_navigation_comparison3()
        {
            base.SelectMany_navigation_comparison3();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l2].[Id]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
CROSS JOIN [Level2] AS [l2]
WHERE [l1.OneToOne_Optional_FK].[Id] = [l2].[Id]",
                Sql);
        }

        public override void Where_complex_predicate_with_with_nav_prop_and_OrElse1()
        {
            base.Where_complex_predicate_with_with_nav_prop_and_OrElse1();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l2].[Id]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
CROSS JOIN [Level2] AS [l2]
INNER JOIN [Level1] AS [l2.OneToOne_Required_FK_Inverse] ON [l2].[Level1_Required_Id] = [l2.OneToOne_Required_FK_Inverse].[Id]
WHERE (([l1.OneToOne_Optional_FK].[Name] = N'L2 01') AND [l1.OneToOne_Optional_FK].[Name] IS NOT NULL) OR (([l2.OneToOne_Required_FK_Inverse].[Name] <> N'Bar') OR [l2.OneToOne_Required_FK_Inverse].[Name] IS NULL)",
                Sql);
        }

        public override void Where_complex_predicate_with_with_nav_prop_and_OrElse2()
        {
            base.Where_complex_predicate_with_with_nav_prop_and_OrElse2();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Required_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id]
WHERE (([l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name] = N'L3 05') AND [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name] IS NOT NULL) OR (([l1.OneToOne_Optional_FK].[Name] <> N'L2 05') OR [l1.OneToOne_Optional_FK].[Name] IS NULL)",
                Sql);
        }

        public override void Where_complex_predicate_with_with_nav_prop_and_OrElse3()
        {
            base.Where_complex_predicate_with_with_nav_prop_and_OrElse3();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[Id], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Required_Id], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[Name], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Required_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Required_FK].[Id] = [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE (([l1.OneToOne_Optional_FK].[Name] <> N'L2 05') OR [l1.OneToOne_Optional_FK].[Name] IS NULL) OR (([l1.OneToOne_Required_FK.OneToOne_Optional_FK].[Name] = N'L3 05') AND [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[Name] IS NOT NULL)",
                Sql);
        }

        public override void Where_complex_predicate_with_with_nav_prop_and_OrElse4()
        {
            base.Where_complex_predicate_with_with_nav_prop_and_OrElse4();

            Assert.Equal(
                @"SELECT [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_InverseId], [l3].[OneToMany_Optional_Self_InverseId], [l3].[OneToMany_Required_InverseId], [l3].[OneToMany_Required_Self_InverseId], [l3].[OneToOne_Optional_PK_InverseId], [l3].[OneToOne_Optional_SelfId], [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_SelfId], [l3.OneToOne_Optional_FK_Inverse].[Id], [l3.OneToOne_Optional_FK_Inverse].[Date], [l3.OneToOne_Optional_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Optional_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Optional_FK_Inverse].[Name], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN [Level2] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [Level1] AS [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse] ON [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id] = [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id]
LEFT JOIN [Level2] AS [l3.OneToOne_Optional_FK_Inverse] ON [l3].[Level2_Optional_Id] = [l3.OneToOne_Optional_FK_Inverse].[Id]
WHERE (([l3.OneToOne_Optional_FK_Inverse].[Name] <> N'L2 05') OR [l3.OneToOne_Optional_FK_Inverse].[Name] IS NULL) OR (([l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Name] = N'L1 05') AND [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Name] IS NOT NULL)",
                Sql);
        }

        public override void Complex_navigations_with_predicate_projected_into_anonymous_type()
        {
            base.Complex_navigations_with_predicate_projected_into_anonymous_type();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK].[Id], [e.OneToOne_Required_FK].[Date], [e.OneToOne_Required_FK].[Level1_Optional_Id], [e.OneToOne_Required_FK].[Level1_Required_Id], [e.OneToOne_Required_FK].[Name], [e.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[Id], [e.OneToOne_Required_FK.OneToOne_Required_FK].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToOne_Required_FK].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToOne_Required_FK].[Name], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Name], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Required_FK] ON [e].[Id] = [e.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [Level3] AS [e.OneToOne_Required_FK.OneToOne_Required_FK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Required_FK].[Level2_Required_Id]
LEFT JOIN [Level3] AS [e.OneToOne_Required_FK.OneToOne_Optional_FK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE ((([e.OneToOne_Required_FK.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id]) AND ([e.OneToOne_Required_FK.OneToOne_Required_FK].[Id] IS NOT NULL AND [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] IS NOT NULL)) OR ([e.OneToOne_Required_FK.OneToOne_Required_FK].[Id] IS NULL AND [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] IS NULL)) AND (([e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] <> 7) OR [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] IS NULL)",
                Sql);
        }

        public override void Complex_navigations_with_predicate_projected_into_anonymous_type2()
        {
            base.Complex_navigations_with_predicate_projected_into_anonymous_type2();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Level2_Optional_Id], [e].[Level2_Required_Id], [e].[Name], [e].[OneToMany_Optional_InverseId], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_PK_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id], [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Date], [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Name], [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level3] AS [e]
INNER JOIN [Level2] AS [e.OneToOne_Required_FK_Inverse] ON [e].[Level2_Required_Id] = [e.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [Level1] AS [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [e.OneToOne_Required_FK_Inverse].[Level1_Required_Id] = [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [Level1] AS [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse] ON [e.OneToOne_Required_FK_Inverse].[Level1_Optional_Id] = [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id]
WHERE (([e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id] = [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id]) AND [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id] IS NOT NULL) AND (([e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id] <> 7) OR [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id] IS NULL)",
                Sql);
        }

        public override void Optional_navigation_projected_into_DTO()
        {
            base.Optional_navigation_projected_into_DTO();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]",
                Sql);
        }

        public override void OrderBy_nav_prop_reference_optional()
        {
            base.OrderBy_nav_prop_reference_optional();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [e.OneToOne_Optional_FK].[Name], [e].[Id]",
                Sql);
        }

        public override void OrderBy_nav_prop_reference_optional_via_DefaultIfEmpty()
        {
            base.OrderBy_nav_prop_reference_optional_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l2].[Name], [l1].[Id]",
                Sql);
        }

        public override void Result_operator_nav_prop_reference_optional()
        {
            base.Result_operator_nav_prop_reference_optional();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]",
                Sql);
        }

        public override void Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty()
        {
            base.Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]",
                Sql);
        }

        public override void Include_with_optional_navigation()
        {
            base.Include_with_optional_navigation();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE ([e.OneToOne_Optional_FK].[Name] <> N'L2 05') OR [e.OneToOne_Optional_FK].[Name] IS NULL",
                Sql);
        }

        public override void Include_nested_with_optional_navigation()
        {
            base.Include_nested_with_optional_navigation();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level2] AS [l] ON [l].[Level1_Optional_Id] = [e].[Id]
WHERE ([e.OneToOne_Optional_FK].[Name] <> N'L2 09') OR [e.OneToOne_Optional_FK].[Name] IS NULL
ORDER BY [l].[Id]

SELECT [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId], [l1].[Id], [l1].[Level3_Optional_Id], [l1].[Level3_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_InverseId], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_PK_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l0]
LEFT JOIN [Level4] AS [l1] ON [l1].[Level3_Required_Id] = [l0].[Id]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [e]
    LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
    LEFT JOIN [Level2] AS [l] ON [l].[Level1_Optional_Id] = [e].[Id]
    WHERE (([e.OneToOne_Optional_FK].[Name] <> N'L2 09') OR [e.OneToOne_Optional_FK].[Name] IS NULL) AND ([l0].[OneToMany_Required_InverseId] = [l].[Id]))
ORDER BY [l0].[OneToMany_Required_InverseId]",
                Sql);
        }

        public override void Include_with_groupjoin_skip_and_take()
        {
            base.Include_with_groupjoin_skip_and_take();

            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_0: 1
@__p_1: 5

SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_InverseId], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_PK_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [l2] ON [e].[Id] = [l2].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1] ON [l1].[Id] = [l2].[Id]
WHERE ([e].[Name] <> N'L1 03') OR [e].[Name] IS NULL
ORDER BY [e].[Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY

@__p_0: 1
@__p_1: 5

SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT [e].[Id]
        FROM [Level1] AS [e]
        LEFT JOIN [Level2] AS [l2] ON [e].[Id] = [l2].[Level1_Optional_Id]
        WHERE ([e].[Name] <> N'L1 03') OR [e].[Name] IS NULL
        ORDER BY [e].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t]
) AS [e0] ON [l].[OneToMany_Optional_InverseId] = [e0].[Id]
LEFT JOIN [Level3] AS [l0] ON [l0].[Level2_Optional_Id] = [l].[Id]
ORDER BY [e0].[Id]",
                    Sql);
            }
        }

        public override void Join_flattening_bug_4539()
        {
            base.Join_flattening_bug_4539();

            Assert.Contains(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1_Optional].[Id], [l1_Optional].[Date], [l1_Optional].[Level1_Optional_Id], [l1_Optional].[Level1_Required_Id], [l1_Optional].[Name], [l1_Optional].[OneToMany_Optional_InverseId], [l1_Optional].[OneToMany_Optional_Self_InverseId], [l1_Optional].[OneToMany_Required_InverseId], [l1_Optional].[OneToMany_Required_Self_InverseId], [l1_Optional].[OneToOne_Optional_PK_InverseId], [l1_Optional].[OneToOne_Optional_SelfId], [l2_Required_Reverse].[Id], [l2_Required_Reverse].[Date], [l2_Required_Reverse].[Name], [l2_Required_Reverse].[OneToMany_Optional_Self_InverseId], [l2_Required_Reverse].[OneToMany_Required_Self_InverseId], [l2_Required_Reverse].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1_Optional] ON [l1].[Id] = [l1_Optional].[Level1_Optional_Id]
CROSS JOIN [Level2] AS [l2]
INNER JOIN [Level1] AS [l2_Required_Reverse] ON [l2].[Level1_Required_Id] = [l2_Required_Reverse].[Id]",
                Sql);
        }

        public override void Query_source_materialization_bug_4547()
        {
            base.Query_source_materialization_bug_4547();

            // Separate asserts to account for ordering differences on .NETCore

            Assert.Contains(
                @"SELECT [e1].[Id]
FROM [Level1] AS [e1]",
                Sql);

            Assert.Contains(
                @"SELECT TOP(1) [subQuery2].[Id], [subQuery2].[Date], [subQuery2].[Level1_Optional_Id], [subQuery2].[Level1_Required_Id], [subQuery2].[Name], [subQuery2].[OneToMany_Optional_InverseId], [subQuery2].[OneToMany_Optional_Self_InverseId], [subQuery2].[OneToMany_Required_InverseId], [subQuery2].[OneToMany_Required_Self_InverseId], [subQuery2].[OneToOne_Optional_PK_InverseId], [subQuery2].[OneToOne_Optional_SelfId], [subQuery3].[Id], [subQuery3].[Level2_Optional_Id], [subQuery3].[Level2_Required_Id], [subQuery3].[Name], [subQuery3].[OneToMany_Optional_InverseId], [subQuery3].[OneToMany_Optional_Self_InverseId], [subQuery3].[OneToMany_Required_InverseId], [subQuery3].[OneToMany_Required_Self_InverseId], [subQuery3].[OneToOne_Optional_PK_InverseId], [subQuery3].[OneToOne_Optional_SelfId]
FROM [Level2] AS [subQuery2]
LEFT JOIN [Level3] AS [subQuery3] ON [subQuery2].[Id] = [subQuery3].[Level2_Optional_Id]",
                Sql);

            Assert.Contains(
                @"SELECT [e3].[Id]
FROM [Level3] AS [e3]",
                Sql);
        }

        public override void SelectMany_navigation_property()
        {
            base.SelectMany_navigation_property();

            Assert.Equal(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void SelectMany_navigation_property_and_projection()
        {
            base.SelectMany_navigation_property_and_projection();

            Assert.Equal(
                @"SELECT [l1.OneToMany_Optional].[Name]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void SelectMany_navigation_property_and_filter_before()
        {
            base.SelectMany_navigation_property_and_filter_before();

            Assert.Equal(
                @"SELECT [e.OneToMany_Optional].[Id], [e.OneToMany_Optional].[Date], [e.OneToMany_Optional].[Level1_Optional_Id], [e.OneToMany_Optional].[Level1_Required_Id], [e.OneToMany_Optional].[Name], [e.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
INNER JOIN [Level2] AS [e.OneToMany_Optional] ON [e].[Id] = [e.OneToMany_Optional].[OneToMany_Optional_InverseId]
WHERE [e].[Id] = 1",
                Sql);
        }

        public override void SelectMany_navigation_property_and_filter_after()
        {
            base.SelectMany_navigation_property_and_filter_after();

            Assert.Equal(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
WHERE [l1.OneToMany_Optional].[Id] <> 6",
                Sql);
        }

        public override void SelectMany_nested_navigation_property_required()
        {
            base.SelectMany_nested_navigation_property_required();

            Assert.Equal(
                @"SELECT [l1.OneToOne_Required_FK.OneToMany_Optional].[Id], [l1.OneToOne_Required_FK.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToOne_Required_FK.OneToMany_Optional].[Level2_Required_Id], [l1.OneToOne_Required_FK.OneToMany_Optional].[Name], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
INNER JOIN [Level3] AS [l1.OneToOne_Required_FK.OneToMany_Optional] ON [l1.OneToOne_Required_FK].[Id] = [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void SelectMany_nested_navigation_property_optional_and_projection()
        {
            base.SelectMany_nested_navigation_property_optional_and_projection();

            Assert.Equal(
                @"SELECT [l1.OneToOne_Optional_FK.OneToMany_Optional].[Name]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
INNER JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToMany_Optional] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void Multiple_SelectMany_calls()
        {
            base.Multiple_SelectMany_calls();

            Assert.Equal(
                @"SELECT [e.OneToMany_Optional.OneToMany_Optional].[Id], [e.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [e.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [e.OneToMany_Optional.OneToMany_Optional].[Name], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
INNER JOIN [Level2] AS [e.OneToMany_Optional] ON [e].[Id] = [e.OneToMany_Optional].[OneToMany_Optional_InverseId]
INNER JOIN [Level3] AS [e.OneToMany_Optional.OneToMany_Optional] ON [e.OneToMany_Optional].[Id] = [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void SelectMany_navigation_property_with_another_navigation_in_subquery()
        {
            base.SelectMany_navigation_property_with_another_navigation_in_subquery();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[Id], [l1.OneToMany_Optional.OneToOne_Optional_FK].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToOne_Optional_FK].[Level2_Required_Id], [l1.OneToMany_Optional.OneToOne_Optional_FK].[Name], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [Level3] AS [l1.OneToMany_Optional.OneToOne_Optional_FK] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToOne_Optional_FK].[Level2_Optional_Id]",
                Sql);
        }

        [Fact]
        public void Multiple_complex_includes_from_sql()
        {
            using (var context = CreateContext())
            {
                var query = context.LevelOne.FromSql("SELECT * FROM [Level1]")
                    .Include(e => e.OneToOne_Optional_FK)
                    .ThenInclude(e => e.OneToMany_Optional)
                    .Include(e => e.OneToMany_Optional)
                    .ThenInclude(e => e.OneToOne_Optional_FK);

                var results = query.ToList();

                Assert.Equal(13, results.Count);
                Assert.Equal(
                    @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM (
    SELECT * FROM [Level1]
) AS [l]
LEFT JOIN [Level2] AS [l2] ON [l2].[Level1_Optional_Id] = [l].[Id]
ORDER BY [l].[Id], [l2].[Id]

SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_InverseId], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_PK_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l0]
LEFT JOIN [Level3] AS [l1] ON [l1].[Level2_Optional_Id] = [l0].[Id]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM [Level1]
    ) AS [l]
    WHERE [l0].[OneToMany_Optional_InverseId] = [l].[Id])
ORDER BY [l0].[OneToMany_Optional_InverseId]

SELECT [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_InverseId], [l3].[OneToMany_Optional_Self_InverseId], [l3].[OneToMany_Required_InverseId], [l3].[OneToMany_Required_Self_InverseId], [l3].[OneToOne_Optional_PK_InverseId], [l3].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN (
    SELECT DISTINCT [l].[Id], [l2].[Id] AS [Id0]
    FROM (
        SELECT * FROM [Level1]
    ) AS [l]
    LEFT JOIN [Level2] AS [l2] ON [l2].[Level1_Optional_Id] = [l].[Id]
) AS [l20] ON [l3].[OneToMany_Optional_InverseId] = [l20].[Id0]
ORDER BY [l20].[Id], [l20].[Id0]",
                    Sql);
            }
        }

        public override void Where_navigation_property_to_collection()
        {
            base.Where_navigation_property_to_collection();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
WHERE (
    SELECT COUNT(*)
    FROM [Level3] AS [l]
    WHERE [l1.OneToOne_Required_FK].[Id] = [l].[OneToMany_Optional_InverseId]
) > 0",
                Sql);
        }

        public override void Where_navigation_property_to_collection2()
        {
            base.Where_navigation_property_to_collection2();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_InverseId], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_PK_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l1]
INNER JOIN [Level2] AS [l1.OneToOne_Required_FK_Inverse] ON [l1].[Level2_Required_Id] = [l1.OneToOne_Required_FK_Inverse].[Id]
WHERE (
    SELECT COUNT(*)
    FROM [Level3] AS [l]
    WHERE [l1.OneToOne_Required_FK_Inverse].[Id] = [l].[OneToMany_Optional_InverseId]
) > 0",
                Sql);
        }

        public override void Where_navigation_property_to_collection_of_original_entity_type()
        {
            base.Where_navigation_property_to_collection_of_original_entity_type();

            Assert.Equal(
                @"SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l2]
INNER JOIN [Level1] AS [l2.OneToMany_Required_Inverse] ON [l2].[OneToMany_Required_InverseId] = [l2.OneToMany_Required_Inverse].[Id]
WHERE (
    SELECT COUNT(*)
    FROM [Level2] AS [l]
    WHERE [l2.OneToMany_Required_Inverse].[Id] = [l].[OneToMany_Optional_InverseId]
) > 0",
                Sql);
        }

        public override void Complex_multi_include_with_order_by_and_paging()
        {
            base.Complex_multi_include_with_order_by_and_paging();

            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_0: 0
@__p_1: 10

SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [l] ON [l].[Level1_Required_Id] = [e].[Id]
LEFT JOIN [Level2] AS [l2] ON [l2].[Level1_Required_Id] = [e].[Id]
ORDER BY [e].[Name], [l].[Id], [l2].[Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY

@__p_0: 0
@__p_1: 10

SELECT [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_InverseId], [l3].[OneToMany_Optional_Self_InverseId], [l3].[OneToMany_Required_InverseId], [l3].[OneToMany_Required_Self_InverseId], [l3].[OneToOne_Optional_PK_InverseId], [l3].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN (
    SELECT DISTINCT [t0].*
    FROM (
        SELECT [e].[Name], [l].[Id], [l2].[Id] AS [Id0]
        FROM [Level1] AS [e]
        LEFT JOIN [Level2] AS [l] ON [l].[Level1_Required_Id] = [e].[Id]
        LEFT JOIN [Level2] AS [l2] ON [l2].[Level1_Required_Id] = [e].[Id]
        ORDER BY [e].[Name], [l].[Id], [l2].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t0]
) AS [l20] ON [l3].[OneToMany_Required_InverseId] = [l20].[Id0]
ORDER BY [l20].[Name], [l20].[Id], [l20].[Id0]

@__p_0: 0
@__p_1: 10

SELECT [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l0]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT [e].[Name], [l].[Id]
        FROM [Level1] AS [e]
        LEFT JOIN [Level2] AS [l] ON [l].[Level1_Required_Id] = [e].[Id]
        ORDER BY [e].[Name], [l].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t]
) AS [l1] ON [l0].[OneToMany_Optional_InverseId] = [l1].[Id]
ORDER BY [l1].[Name], [l1].[Id]",
                    Sql);
            }
        }

        public override void Complex_multi_include_with_order_by_and_paging_joins_on_correct_key()
        {
            base.Complex_multi_include_with_order_by_and_paging_joins_on_correct_key();

            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_0: 0
@__p_1: 10

SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [l] ON [l].[Level1_Optional_Id] = [e].[Id]
LEFT JOIN [Level2] AS [l2] ON [l2].[Level1_Required_Id] = [e].[Id]
ORDER BY [e].[Name], [l].[Id], [l2].[Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY

@__p_0: 0
@__p_1: 10

SELECT [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_InverseId], [l3].[OneToMany_Optional_Self_InverseId], [l3].[OneToMany_Required_InverseId], [l3].[OneToMany_Required_Self_InverseId], [l3].[OneToOne_Optional_PK_InverseId], [l3].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN (
    SELECT DISTINCT [t0].*
    FROM (
        SELECT [e].[Name], [l].[Id], [l2].[Id] AS [Id0]
        FROM [Level1] AS [e]
        LEFT JOIN [Level2] AS [l] ON [l].[Level1_Optional_Id] = [e].[Id]
        LEFT JOIN [Level2] AS [l2] ON [l2].[Level1_Required_Id] = [e].[Id]
        ORDER BY [e].[Name], [l].[Id], [l2].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t0]
) AS [l20] ON [l3].[OneToMany_Required_InverseId] = [l20].[Id0]
ORDER BY [l20].[Name], [l20].[Id], [l20].[Id0]

@__p_0: 0
@__p_1: 10

SELECT [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l0]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT [e].[Name], [l].[Id]
        FROM [Level1] AS [e]
        LEFT JOIN [Level2] AS [l] ON [l].[Level1_Optional_Id] = [e].[Id]
        ORDER BY [e].[Name], [l].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t]
) AS [l1] ON [l0].[OneToMany_Optional_InverseId] = [l1].[Id]
ORDER BY [l1].[Name], [l1].[Id]",
                    Sql);
            }
        }

        public override void Complex_multi_include_with_order_by_and_paging_joins_on_correct_key2()
        {
            base.Complex_multi_include_with_order_by_and_paging_joins_on_correct_key2();

            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_0: 0
@__p_1: 10

SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [l] ON [l].[Level1_Optional_Id] = [e].[Id]
LEFT JOIN [Level3] AS [l0] ON [l0].[Level2_Required_Id] = [l].[Id]
ORDER BY [e].[Name], [l0].[Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY

@__p_0: 0
@__p_1: 10

SELECT [l1].[Id], [l1].[Level3_Optional_Id], [l1].[Level3_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_InverseId], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_PK_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l1]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT [e].[Name], [l0].[Id]
        FROM [Level1] AS [e]
        LEFT JOIN [Level2] AS [l] ON [l].[Level1_Optional_Id] = [e].[Id]
        LEFT JOIN [Level3] AS [l0] ON [l0].[Level2_Required_Id] = [l].[Id]
        ORDER BY [e].[Name], [l0].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t]
) AS [l00] ON [l1].[OneToMany_Optional_InverseId] = [l00].[Id]
ORDER BY [l00].[Name], [l00].[Id]",
                    Sql);
            }
        }

        public override void Multiple_include_with_multiple_optional_navigations()
        {
            base.Multiple_include_with_multiple_optional_navigations();

            Assert.Equal(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Required_Id], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK].[Id], [e.OneToOne_Required_FK].[Date], [e.OneToOne_Required_FK].[Level1_Optional_Id], [e.OneToOne_Required_FK].[Level1_Required_Id], [e.OneToOne_Required_FK].[Name], [e.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[Id], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[Name], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_PK].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Name], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level1] AS [e]
LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [e.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [e.OneToOne_Optional_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
LEFT JOIN [Level2] AS [e.OneToOne_Required_FK] ON [e].[Id] = [e.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [Level3] AS [e.OneToOne_Required_FK.OneToOne_Optional_PK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
LEFT JOIN [Level3] AS [e.OneToOne_Required_FK.OneToOne_Optional_FK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
LEFT JOIN [Level2] AS [l] ON [l].[Level1_Required_Id] = [e].[Id]
WHERE ([e.OneToOne_Required_FK.OneToOne_Optional_PK].[Name] <> N'Foo') OR [e.OneToOne_Required_FK.OneToOne_Optional_PK].[Name] IS NULL
ORDER BY [e].[Id], [l].[Id]

SELECT [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l0]
INNER JOIN (
    SELECT DISTINCT [e].[Id], [l].[Id] AS [Id0]
    FROM [Level1] AS [e]
    LEFT JOIN [Level2] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
    LEFT JOIN [Level3] AS [e.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [e.OneToOne_Optional_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
    LEFT JOIN [Level2] AS [e.OneToOne_Required_FK] ON [e].[Id] = [e.OneToOne_Required_FK].[Level1_Required_Id]
    LEFT JOIN [Level3] AS [e.OneToOne_Required_FK.OneToOne_Optional_PK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
    LEFT JOIN [Level3] AS [e.OneToOne_Required_FK.OneToOne_Optional_FK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
    LEFT JOIN [Level2] AS [l] ON [l].[Level1_Required_Id] = [e].[Id]
    WHERE ([e.OneToOne_Required_FK.OneToOne_Optional_PK].[Name] <> N'Foo') OR [e.OneToOne_Required_FK.OneToOne_Optional_PK].[Name] IS NULL
) AS [l1] ON [l0].[OneToMany_Optional_InverseId] = [l1].[Id0]
ORDER BY [l1].[Id], [l1].[Id0]",
                Sql);
        }

        public override void Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level()
        {
            base.Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level();

            Assert.Equal(
                @"SELECT DISTINCT [l1].[Name]
FROM [Level1] AS [l1]
WHERE EXISTS (
    SELECT 1
    FROM [Level2] AS [l2]
    WHERE [l2].[Level1_Required_Id] = [l1].[Id])",
                Sql);
        }

        public override void Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level_join()
        {
            base.Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level_join();

            Assert.Equal(
                @"SELECT [e1].[Name], [e2].[Id]
FROM [Level1] AS [e1]
INNER JOIN [Level2] AS [e2] ON [e1].[Id] = (
    SELECT TOP(1) [subQuery0].[Id]
    FROM [Level1] AS [subQuery0]
    WHERE [subQuery0].[Id] = [e2].[Level1_Optional_Id]
)
WHERE EXISTS (
    SELECT 1
    FROM [Level2] AS [l2]
    WHERE [l2].[Level1_Required_Id] = [e1].[Id])",
                Sql);
        }

        public override void Correlated_nested_subquery_doesnt_project_unnecessary_columns_in_top_level()
        {
            base.Correlated_nested_subquery_doesnt_project_unnecessary_columns_in_top_level();

            Assert.Equal(
                @"SELECT DISTINCT [l1].[Name]
FROM [Level1] AS [l1]
WHERE EXISTS (
    SELECT 1
    FROM [Level2] AS [l2]
    WHERE EXISTS (
        SELECT 1
        FROM [Level3] AS [l3]))",
                Sql);
        }

        public override void Correlated_nested_two_levels_up_subquery_doesnt_project_unnecessary_columns_in_top_level()
        {
            base.Correlated_nested_two_levels_up_subquery_doesnt_project_unnecessary_columns_in_top_level();

            Assert.StartsWith(
                @"SELECT DISTINCT [l1].[Name]
FROM [Level1] AS [l1]
WHERE EXISTS (
    SELECT 1
    FROM [Level2] AS [l2]
    WHERE EXISTS (
        SELECT 1
        FROM [Level3] AS [l3]))",
                Sql);
        }

        public override void GroupJoin_on_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected()
        {
            base.GroupJoin_on_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN (
    SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
    FROM [Level2] AS [l2]
    WHERE ([l2].[Name] <> N'L2 01') OR [l2].[Name] IS NULL
) AS [t] ON [l1].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1].[Id]",
                Sql);
        }

        public override void GroupJoin_on_complex_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected()
        {
            base.GroupJoin_on_complex_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected();

            Assert.Contains(
                @"SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM (
    SELECT [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
    FROM [Level1] AS [l10]
    LEFT JOIN [Level2] AS [l1.OneToOne_Required_FK] ON [l10].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
    WHERE ([l10].[Name] <> N'L1 01') OR [l10].[Name] IS NULL
) AS [t]",
                Sql);

            Assert.Contains(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]",
                Sql);
        }

        public override void Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin1()
        {
            base.Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin1();

            Assert.Contains(
                @"SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM (
    SELECT [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
    FROM [Level1] AS [l10]
    LEFT JOIN [Level2] AS [l1.OneToOne_Required_FK] ON [l10].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
) AS [t]",
                Sql);

            Assert.Contains(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]",
                Sql);
        }

        public override void Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin2()
        {
            base.Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin2();

            Assert.Contains(
                @"SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM (
    SELECT [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
    FROM [Level1] AS [l10]
    LEFT JOIN [Level2] AS [l1.OneToOne_Required_FK] ON [l10].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
) AS [t]",
                Sql);

            Assert.Contains(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]",
                Sql);
        }

        public override void Null_protection_logic_work_for_outer_key_access_of_manually_created_GroupJoin()
        {
            base.Null_protection_logic_work_for_outer_key_access_of_manually_created_GroupJoin();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [l10].[Id], [l10].[Date], [l10].[Name], [l10].[OneToMany_Optional_Self_InverseId], [l10].[OneToMany_Required_Self_InverseId], [l10].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [Level1] AS [l10] ON [l1.OneToOne_Required_FK].[Level1_Optional_Id] = [l10].[Id]",
                Sql);
        }

        public override void SelectMany_where_with_subquery()
        {
            base.SelectMany_where_with_subquery();

            Assert.Equal(
                @"SELECT [l1.OneToMany_Required].[Id], [l1.OneToMany_Required].[Date], [l1.OneToMany_Required].[Level1_Optional_Id], [l1.OneToMany_Required].[Level1_Required_Id], [l1.OneToMany_Required].[Name], [l1.OneToMany_Required].[OneToMany_Optional_InverseId], [l1.OneToMany_Required].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Required].[OneToMany_Required_InverseId], [l1.OneToMany_Required].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Required].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Required].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Required] ON [l1].[Id] = [l1.OneToMany_Required].[OneToMany_Required_InverseId]
WHERE EXISTS (
    SELECT 1
    FROM [Level3] AS [l]
    WHERE [l1.OneToMany_Required].[Id] = [l].[OneToMany_Required_InverseId])",
                Sql);
        }


        public override void Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access1()
        {
            base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access1();

            Assert.Equal(
                @"SELECT [l3.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN [Level2] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3.OneToOne_Required_FK_Inverse].[Id]",
                Sql);
        }

        public override void Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access2()
        {
            base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access2();

            Assert.Equal(
                @"SELECT [l3.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN [Level2] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3.OneToOne_Required_FK_Inverse].[Id]",
                Sql);
        }

        public override void Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access3()
        {
            base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access3();

            Assert.Equal(
                @"SELECT [l3.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN [Level2] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3.OneToOne_Required_FK_Inverse].[Id]",
                Sql);
        }

        public override void Order_by_key_of_navigation_similar_to_projected_gets_optimized_into_FK_access()
        {
            base.Order_by_key_of_navigation_similar_to_projected_gets_optimized_into_FK_access();

            Assert.Equal(
                @"SELECT [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN [Level2] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [Level1] AS [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id] = [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3].[Level2_Required_Id]",
                Sql);
        }

        public override void Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access_subquery()
        {
            base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access_subquery();

            Assert.Contains(
                @"SELECT [l2.OneToOne_Required_FK_Inverse].[Id], [l2.OneToOne_Required_FK_Inverse].[Name]
FROM [Level1] AS [l2.OneToOne_Required_FK_Inverse]",
                Sql);

            Assert.Contains(
                @"@__p_0: 10

SELECT [t].[Level1_Required_Id]
FROM (
    SELECT TOP(@__p_0) [l3.OneToOne_Required_FK_Inverse].*
    FROM [Level3] AS [l3]
    INNER JOIN [Level2] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
    ORDER BY [l3.OneToOne_Required_FK_Inverse].[Id]
) AS [t]
ORDER BY [t].[Id]",
                Sql);
        }

        public override void Order_by_key_of_anonymous_type_projected_navigation_doesnt_get_optimized_into_FK_access_subquery()
        {
            base.Order_by_key_of_anonymous_type_projected_navigation_doesnt_get_optimized_into_FK_access_subquery();

            Assert.Equal(
                @"@__p_0: 10

SELECT TOP(@__p_0) [l3.OneToOne_Required_FK_Inverse0].[Id], [l3.OneToOne_Required_FK_Inverse0].[Date], [l3.OneToOne_Required_FK_Inverse0].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse0].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse0].[Name], [l3.OneToOne_Required_FK_Inverse0].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse0].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse0].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse0].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse0].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse0].[OneToOne_Optional_SelfId], [l30].[Name]
FROM [Level3] AS [l30]
INNER JOIN [Level2] AS [l3.OneToOne_Required_FK_Inverse0] ON [l30].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l3.OneToOne_Required_FK_Inverse0].[Id]",
                Sql);
        }

        public override void Optional_navigation_take_optional_navigation()
        {
            base.Optional_navigation_take_optional_navigation();

            Assert.Equal(
                @"",
                Sql);
        }

        public override void Projection_select_correct_table_from_subquery_when_materialization_is_not_required()
        {
            base.Projection_select_correct_table_from_subquery_when_materialization_is_not_required();

            Assert.Equal(
                @"@__p_0: 3

SELECT [t].[Name]
FROM (
    SELECT TOP(@__p_0) [l2].*
    FROM [Level2] AS [l2]
    INNER JOIN [Level1] AS [l2.OneToOne_Required_FK_Inverse] ON [l2].[Level1_Required_Id] = [l2.OneToOne_Required_FK_Inverse].[Id]
    WHERE [l2.OneToOne_Required_FK_Inverse].[Name] = N'L1 03'
) AS [t]",
                Sql);
        }

        public override void Projection_select_correct_table_with_anonymous_projection_in_subquery()
        {
            base.Projection_select_correct_table_with_anonymous_projection_in_subquery();

            Assert.Equal(
                @"@__p_0: 3

SELECT TOP(@__p_0) [l20].[Id], [l20].[Date], [l20].[Level1_Optional_Id], [l20].[Level1_Required_Id], [l20].[Name], [l20].[OneToMany_Optional_InverseId], [l20].[OneToMany_Optional_Self_InverseId], [l20].[OneToMany_Required_InverseId], [l20].[OneToMany_Required_Self_InverseId], [l20].[OneToOne_Optional_PK_InverseId], [l20].[OneToOne_Optional_SelfId], [l10].[Id], [l10].[Date], [l10].[Name], [l10].[OneToMany_Optional_Self_InverseId], [l10].[OneToMany_Required_Self_InverseId], [l10].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l20]
INNER JOIN [Level1] AS [l10] ON [l20].[Level1_Required_Id] = [l10].[Id]
INNER JOIN [Level3] AS [l30] ON [l10].[Id] = [l30].[Level2_Required_Id]
WHERE ([l10].[Name] = N'L1 03') AND ([l30].[Name] = N'L3 08')",
                Sql);
        }

        public override void Projection_select_correct_table_in_subquery_when_materialization_is_not_required_in_multiple_joins()
        {
            base.Projection_select_correct_table_in_subquery_when_materialization_is_not_required_in_multiple_joins();

            Assert.Equal(
                @"@__p_0: 3

SELECT [t].[Name]
FROM (
    SELECT TOP(@__p_0) [l1].*
    FROM [Level2] AS [l2]
    INNER JOIN [Level1] AS [l1] ON [l2].[Level1_Required_Id] = [l1].[Id]
    INNER JOIN [Level3] AS [l3] ON [l1].[Id] = [l3].[Level2_Required_Id]
    WHERE ([l1].[Name] = N'L1 03') AND ([l3].[Name] = N'L3 08')
) AS [t]",
                Sql);
        }

        public override void SelectMany_with_Include1()
        {
            base.SelectMany_with_Include1();

            Assert.Equal(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
ORDER BY [l1.OneToMany_Optional].[Id]

SELECT [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [l1]
    INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
    WHERE [l].[OneToMany_Optional_InverseId] = [l1.OneToMany_Optional].[Id])
ORDER BY [l].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void SelectMany_with_Include2()
        {
            base.SelectMany_with_Include2();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [Level3] AS [l1.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id]",
                Sql);
        }

        public override void SelectMany_with_Include_ThenInclude()
        {
            base.SelectMany_with_Include_ThenInclude();

            Assert.Equal(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [Level3] AS [l] ON [l].[Level2_Required_Id] = [l1.OneToMany_Optional].[Id]
ORDER BY [l].[Id]

SELECT [l0].[Id], [l0].[Level3_Optional_Id], [l0].[Level3_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l0]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [l1]
    INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
    LEFT JOIN [Level3] AS [l] ON [l].[Level2_Required_Id] = [l1.OneToMany_Optional].[Id]
    WHERE [l0].[OneToMany_Optional_InverseId] = [l].[Id])
ORDER BY [l0].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void Multiple_SelectMany_with_Include()
        {
            base.Multiple_SelectMany_with_Include();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToMany_Optional].[Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Name], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
INNER JOIN [Level3] AS [l1.OneToMany_Optional.OneToMany_Optional] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [Level4] AS [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Required_Id]
ORDER BY [l1.OneToMany_Optional.OneToMany_Optional].[Id]

SELECT [l].[Id], [l].[Level3_Optional_Id], [l].[Level3_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [l1]
    INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
    INNER JOIN [Level3] AS [l1.OneToMany_Optional.OneToMany_Optional] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId]
    LEFT JOIN [Level4] AS [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Required_Id]
    WHERE [l].[OneToMany_Optional_InverseId] = [l1.OneToMany_Optional.OneToMany_Optional].[Id])
ORDER BY [l].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void SelectMany_with_string_based_Include1()
        {
            base.SelectMany_with_string_based_Include1();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [Level3] AS [l1.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id]",
                Sql);
        }

        public override void SelectMany_with_string_based_Include2()
        {
            base.SelectMany_with_string_based_Include2();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [Level3] AS [l1.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id]
LEFT JOIN [Level4] AS [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK] ON [l1.OneToMany_Optional.OneToOne_Required_FK].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Required_Id]",
                Sql);
        }

        public override void Multiple_SelectMany_with_string_based_Include()
        {
            base.Multiple_SelectMany_with_string_based_Include();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToMany_Optional].[Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Name], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
INNER JOIN [Level3] AS [l1.OneToMany_Optional.OneToMany_Optional] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [Level4] AS [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Required_Id]",
                Sql);
        }

        public override void Required_navigation_with_Include()
        {
            base.Required_navigation_with_Include();

            Assert.Equal(
                @"SELECT [l3.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l3]
INNER JOIN [Level2] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [Level1] AS [l] ON [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId] = [l].[Id]",
                Sql);
        }

        public override void Required_navigation_with_Include_ThenInclude()
        {
            base.Required_navigation_with_Include_ThenInclude();

            Assert.Equal(
                @"SELECT [l4.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse].[Level2_Optional_Id], [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id], [l4.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId], [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l4]
INNER JOIN [Level3] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [Level2] AS [l] ON [l4.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId] = [l].[Id]
LEFT JOIN [Level1] AS [l0] ON [l].[OneToMany_Optional_InverseId] = [l0].[Id]",
                Sql);
        }

        public override void Multiple_required_navigations_with_Include()
        {
            base.Multiple_required_navigations_with_Include();

            Assert.Equal(
                @"SELECT [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l4]
INNER JOIN [Level3] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [Level2] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [Level3] AS [l] ON [l].[Level2_Optional_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]",
                Sql);
        }

        public override void Multiple_required_navigation_using_multiple_selects_with_Include()
        {
            base.Multiple_required_navigation_using_multiple_selects_with_Include();

            Assert.Equal(
                @"SELECT [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l4]
INNER JOIN [Level3] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [Level2] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [Level3] AS [l] ON [l].[Level2_Optional_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]",
                Sql);
        }

        public override void Multiple_required_navigation_with_string_based_Include()
        {
            base.Multiple_required_navigation_with_string_based_Include();

            Assert.Equal(
                @"SELECT [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l4]
INNER JOIN [Level3] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [Level2] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [Level3] AS [l] ON [l].[Level2_Optional_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]",
                Sql);
        }

        public override void Multiple_required_navigation_using_multiple_selects_with_string_based_Include()
        {
            base.Multiple_required_navigation_using_multiple_selects_with_string_based_Include();

            Assert.Equal(
                @"SELECT [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l4]
INNER JOIN [Level3] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [Level2] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [Level3] AS [l] ON [l].[Level2_Optional_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]",
                Sql);
        }

        public override void Optional_navigation_with_Include()
        {
            base.Optional_navigation_with_Include();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l] ON [l].[Level2_Optional_Id] = [l1.OneToOne_Optional_FK].[Id]",
                Sql);
        }

        public override void Optional_navigation_with_Include_ThenInclude()
        {
            base.Optional_navigation_with_Include_ThenInclude();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [l1.OneToOne_Optional_FK].[Id]

SELECT [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId], [l0].[Id], [l0].[Level3_Optional_Id], [l0].[Level3_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level3] AS [l]
LEFT JOIN [Level4] AS [l0] ON [l0].[Level3_Optional_Id] = [l].[Id]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [l1]
    LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
    WHERE [l].[OneToMany_Optional_InverseId] = [l1.OneToOne_Optional_FK].[Id])
ORDER BY [l].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void Multiple_optional_navigation_with_Include()
        {
            base.Multiple_optional_navigation_with_Include();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
ORDER BY [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id]

SELECT [l].[Id], [l].[Level3_Optional_Id], [l].[Level3_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [l1]
    LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
    LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
    WHERE [l].[OneToMany_Optional_InverseId] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id])
ORDER BY [l].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void Multiple_optional_navigation_with_string_based_Include()
        {
            base.Multiple_optional_navigation_with_string_based_Include();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
ORDER BY [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id]

SELECT [l].[Id], [l].[Level3_Optional_Id], [l].[Level3_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [Level4] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [l1]
    LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
    LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
    WHERE [l].[OneToMany_Optional_InverseId] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id])
ORDER BY [l].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void SelectMany_with_navigation_and_explicit_DefaultIfEmpty()
        {
            base.SelectMany_with_navigation_and_explicit_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
WHERE [l1.OneToMany_Optional].[Id] IS NOT NULL",
                Sql);
        }

        public override void SelectMany_with_navigation_and_Distinct()
        {
            base.SelectMany_with_navigation_and_Distinct();

            Assert.Contains(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l.OneToMany_Optional].[Id], [l.OneToMany_Optional].[Date], [l.OneToMany_Optional].[Level1_Optional_Id], [l.OneToMany_Optional].[Level1_Required_Id], [l.OneToMany_Optional].[Name], [l.OneToMany_Optional].[OneToMany_Optional_InverseId], [l.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l.OneToMany_Optional].[OneToMany_Required_InverseId], [l.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l]
LEFT JOIN [Level2] AS [l.OneToMany_Optional] ON [l].[Id] = [l.OneToMany_Optional].[OneToMany_Optional_InverseId]
ORDER BY [l].[Id]",
                Sql);

            Assert.Contains(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_InverseId], [l0].[OneToMany_Optional_Self_InverseId], [l0].[OneToMany_Required_InverseId], [l0].[OneToMany_Required_Self_InverseId], [l0].[OneToOne_Optional_PK_InverseId], [l0].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l0]
WHERE EXISTS (
    SELECT 1
    FROM [Level1] AS [l]
    LEFT JOIN [Level2] AS [l.OneToMany_Optional] ON [l].[Id] = [l.OneToMany_Optional].[OneToMany_Optional_InverseId]
    WHERE [l0].[OneToMany_Optional_InverseId] = [l].[Id])
ORDER BY [l0].[OneToMany_Optional_InverseId]",
                Sql);
        }

        public override void SelectMany_with_navigation_filter_and_explicit_DefaultIfEmpty()
        {
            base.SelectMany_with_navigation_filter_and_explicit_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
ORDER BY [l1].[Id]",
                Sql);
        }

        public override void SelectMany_with_nested_navigation_and_explicit_DefaultIfEmpty()
        {
            base.SelectMany_with_nested_navigation_and_explicit_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Required_FK.OneToMany_Optional].[Id], [l1.OneToOne_Required_FK.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToOne_Required_FK.OneToMany_Optional].[Level2_Required_Id], [l1.OneToOne_Required_FK.OneToMany_Optional].[Name], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Required_FK.OneToMany_Optional] ON [l1.OneToOne_Required_FK].[Id] = [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId]
WHERE [l1.OneToOne_Required_FK.OneToMany_Optional].[Id] IS NOT NULL",
                Sql);
        }

        public override void SelectMany_with_nested_navigation_filter_and_explicit_DefaultIfEmpty()
        {
            base.SelectMany_with_nested_navigation_filter_and_explicit_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Name], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [Level3] AS [l1.OneToOne_Optional_FK.OneToMany_Optional] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId]
ORDER BY [l1.OneToOne_Optional_FK].[Id]",
                Sql);
        }

        public override void Multiple_SelectMany_with_navigation_and_explicit_DefaultIfEmpty()
        {
            base.Multiple_SelectMany_with_navigation_and_explicit_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToMany_Optional].[Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Name], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [Level3] AS [l1.OneToMany_Optional.OneToMany_Optional] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId]
ORDER BY [l1.OneToMany_Optional].[Id]",
                Sql);
        }

        public override void SelectMany_with_navigation_filter_paging_and_explicit_DefautltIfEmpty()
        {
            base.SelectMany_with_navigation_filter_paging_and_explicit_DefautltIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Required].[Id], [l1.OneToMany_Required].[Date], [l1.OneToMany_Required].[Level1_Optional_Id], [l1.OneToMany_Required].[Level1_Required_Id], [l1.OneToMany_Required].[Name], [l1.OneToMany_Required].[OneToMany_Optional_InverseId], [l1.OneToMany_Required].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Required].[OneToMany_Required_InverseId], [l1.OneToMany_Required].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Required].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Required].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToMany_Required] ON [l1].[Id] = [l1.OneToMany_Required].[OneToMany_Required_InverseId]
ORDER BY [l1].[Id]",
                Sql);
        }

        public override void Select_join_subquery_containing_filter_and_distinct()
        {
            base.Select_join_subquery_containing_filter_and_distinct();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN (
    SELECT DISTINCT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
    FROM [Level2] AS [l]
    WHERE [l].[Id] > 2
) AS [t] ON [l1].[Id] = [t].[Level1_Optional_Id]",
                Sql);
        }

        public override void Select_join_with_key_selector_being_a_subquery()
        {
            base.Select_join_with_key_selector_being_a_subquery();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
INNER JOIN [Level2] AS [l2] ON [l1].[Id] = (
    SELECT TOP(1) [l0].[Id]
    FROM [Level2] AS [l0]
    ORDER BY [l0].[Id]
)",
                Sql);
        }

        public override void Contains_with_subquery_optional_navigation_and_constant_item()
        {
            base.Contains_with_subquery_optional_navigation_and_constant_item();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE 1 IN (
    SELECT [t].[Id]
    FROM (
        SELECT DISTINCT [l].*
        FROM [Level3] AS [l]
        WHERE [l1.OneToOne_Optional_FK].[Id] = [l].[OneToMany_Optional_InverseId]
    ) AS [t]
)",
                Sql);
        }

        public override void Complex_query_with_optional_navigations_and_client_side_evaluation()
        {
            base.Complex_query_with_optional_navigations_and_client_side_evaluation();

            Assert.Equal(
                @"",
                Sql);
        }

        public override void GroupJoin_on_left_side_being_a_subquery()
        {
            base.GroupJoin_on_left_side_being_a_subquery();

            Assert.Contains(
                @"SELECT [x.OneToOne_Optional_FK].[Id], [x.OneToOne_Optional_FK].[Date], [x.OneToOne_Optional_FK].[Level1_Optional_Id], [x.OneToOne_Optional_FK].[Level1_Required_Id], [x.OneToOne_Optional_FK].[Name], [x.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [x.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [x.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [x.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [x.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [x.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [Level2] AS [x.OneToOne_Optional_FK]",
                Sql);

            Assert.Contains(
                @"@__p_0: 2

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_SelfId], [t].[c0], [t].[c1], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[c2], [t].[OneToMany_Optional_InverseId], [t].[c3], [t].[OneToMany_Required_InverseId], [t].[c4], [t].[OneToOne_Optional_PK_InverseId], [t].[c5]
FROM (
    SELECT TOP(@__p_0) [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id] AS [c0], [l1.OneToOne_Optional_FK].[Date] AS [c1], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name] AS [c2], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId] AS [c3], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId] AS [c4], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId] AS [c5]
    FROM [Level1] AS [l1]
    LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
    ORDER BY [l1.OneToOne_Optional_FK].[Name]
) AS [t]
ORDER BY [t].[Name]",
                Sql);
        }

        public override void GroupJoin_on_right_side_being_a_subquery()
        {
            base.GroupJoin_on_right_side_being_a_subquery();

            Assert.Contains(
                @"@__p_0: 2

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_SelfId]
FROM (
    SELECT TOP(@__p_0) [x].[Id], [x].[Date], [x].[Name], [x].[OneToMany_Optional_Self_InverseId], [x].[OneToMany_Required_Self_InverseId], [x].[OneToOne_Optional_SelfId]
    FROM [Level1] AS [x]
    LEFT JOIN [Level2] AS [x.OneToOne_Optional_FK] ON [x].[Id] = [x.OneToOne_Optional_FK].[Level1_Optional_Id]
    ORDER BY [x.OneToOne_Optional_FK].[Name]
) AS [t]
ORDER BY [t].[Name]",
                Sql);

            Assert.Contains(
                @"SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level2] AS [l2]",
                Sql);
        }

        public override void GroupJoin_in_subquery_with_client_result_operator()
        {
            base.GroupJoin_in_subquery_with_client_result_operator();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Name]
FROM [Level1] AS [l1]
WHERE [l1].[Id] < 3

SELECT [l1_inner].[Id], [l1_inner].[Date], [l1_inner].[Name], [l1_inner].[OneToMany_Optional_Self_InverseId], [l1_inner].[OneToMany_Required_Self_InverseId], [l1_inner].[OneToOne_Optional_SelfId], [l2_inner].[Id], [l2_inner].[Date], [l2_inner].[Level1_Optional_Id], [l2_inner].[Level1_Required_Id], [l2_inner].[Name], [l2_inner].[OneToMany_Optional_InverseId], [l2_inner].[OneToMany_Optional_Self_InverseId], [l2_inner].[OneToMany_Required_InverseId], [l2_inner].[OneToMany_Required_Self_InverseId], [l2_inner].[OneToOne_Optional_PK_InverseId], [l2_inner].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1_inner]
LEFT JOIN [Level2] AS [l2_inner] ON [l1_inner].[Id] = [l2_inner].[Level1_Optional_Id]

SELECT [l1_inner].[Id], [l1_inner].[Date], [l1_inner].[Name], [l1_inner].[OneToMany_Optional_Self_InverseId], [l1_inner].[OneToMany_Required_Self_InverseId], [l1_inner].[OneToOne_Optional_SelfId], [l2_inner].[Id], [l2_inner].[Date], [l2_inner].[Level1_Optional_Id], [l2_inner].[Level1_Required_Id], [l2_inner].[Name], [l2_inner].[OneToMany_Optional_InverseId], [l2_inner].[OneToMany_Optional_Self_InverseId], [l2_inner].[OneToMany_Required_InverseId], [l2_inner].[OneToMany_Required_Self_InverseId], [l2_inner].[OneToOne_Optional_PK_InverseId], [l2_inner].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1_inner]
LEFT JOIN [Level2] AS [l2_inner] ON [l1_inner].[Id] = [l2_inner].[Level1_Optional_Id]",
                Sql);
        }

        public override void GroupJoin_in_subquery_with_client_projection()
        {
            base.GroupJoin_in_subquery_with_client_projection();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Name]
FROM [Level1] AS [l1]
WHERE [l1].[Id] < 3

SELECT COUNT(*)
FROM [Level1] AS [l1_inner0]
LEFT JOIN [Level2] AS [l2_inner1] ON [l1_inner0].[Id] = [l2_inner1].[Level1_Optional_Id]

SELECT COUNT(*)
FROM [Level1] AS [l1_inner0]
LEFT JOIN [Level2] AS [l2_inner1] ON [l1_inner0].[Id] = [l2_inner1].[Level1_Optional_Id]",
                Sql);
        }

        public override void GroupJoin_in_subquery_with_client_projection_nested1()
        {
            base.GroupJoin_in_subquery_with_client_projection_nested1();

            Assert.StartsWith(
                @"SELECT [l1_outer].[Id], [l1_outer].[Name]
FROM [Level1] AS [l1_outer]
WHERE [l1_outer].[Id] < 2

SELECT [l1_middle].[Id], [l1_middle].[Date], [l1_middle].[Name], [l1_middle].[OneToMany_Optional_Self_InverseId], [l1_middle].[OneToMany_Required_Self_InverseId], [l1_middle].[OneToOne_Optional_SelfId], [l2_middle].[Id], [l2_middle].[Date], [l2_middle].[Level1_Optional_Id], [l2_middle].[Level1_Required_Id], [l2_middle].[Name], [l2_middle].[OneToMany_Optional_InverseId], [l2_middle].[OneToMany_Optional_Self_InverseId], [l2_middle].[OneToMany_Required_InverseId], [l2_middle].[OneToMany_Required_Self_InverseId], [l2_middle].[OneToOne_Optional_PK_InverseId], [l2_middle].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1_middle]
LEFT JOIN [Level2] AS [l2_middle] ON [l1_middle].[Id] = [l2_middle].[Level1_Optional_Id]

SELECT COUNT(*)
FROM [Level1] AS [l1_inner0]
LEFT JOIN [Level2] AS [l2_inner1] ON [l1_inner0].[Id] = [l2_inner1].[Level1_Optional_Id]

SELECT COUNT(*)
FROM [Level1] AS [l1_inner0]
LEFT JOIN [Level2] AS [l2_inner1] ON [l1_inner0].[Id] = [l2_inner1].[Level1_Optional_Id]",
                Sql);
        }

        public override void GroupJoin_in_subquery_with_client_projection_nested2()
        {
            base.GroupJoin_in_subquery_with_client_projection_nested2();

            Assert.Equal(
                @"SELECT [l1_outer].[Id], [l1_outer].[Name]
FROM [Level1] AS [l1_outer]
WHERE [l1_outer].[Id] < 2

SELECT COUNT(*)
FROM [Level1] AS [l1_middle0]
LEFT JOIN [Level2] AS [l2_middle1] ON [l1_middle0].[Id] = [l2_middle1].[Level1_Optional_Id]
WHERE (
    SELECT COUNT(*)
    FROM [Level1] AS [l1_inner0]
    LEFT JOIN [Level2] AS [l2_inner1] ON [l1_inner0].[Id] = [l2_inner1].[Level1_Optional_Id]
) > 7",
                Sql);
        }

        public override void GroupJoin_reference_to_group_in_OrderBy()
        {
            base.GroupJoin_reference_to_group_in_OrderBy();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]",
                Sql);
        }

        public override void GroupJoin_client_method_on_outer()
        {
            base.GroupJoin_client_method_on_outer();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]",
                Sql);
        }

        public override void GroupJoin_client_method_in_OrderBy()
        {
            base.GroupJoin_client_method_in_OrderBy();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]",
                Sql);
        }

        public override void GroupJoin_without_DefaultIfEmpty()
        {
            base.GroupJoin_without_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]",
                Sql);
        }

        public override void GroupJoin_with_subquery_on_inner()
        {
            base.GroupJoin_with_subquery_on_inner();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]",
                Sql);
        }

        public override void GroupJoin_with_subquery_on_inner_and_no_DefaultIfEmpty()
        {
            base.GroupJoin_with_subquery_on_inner_and_no_DefaultIfEmpty();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]",
                Sql);
        }

        public override void Optional_navigation_in_subquery_with_unrelated_projection()
        {
            base.Optional_navigation_in_subquery_with_unrelated_projection();

            Assert.Equal(
                @"@__p_0: 15

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_SelfId], [t].[c0], [t].[c1], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[c2], [t].[OneToMany_Optional_InverseId], [t].[c3], [t].[OneToMany_Required_InverseId], [t].[c4], [t].[OneToOne_Optional_PK_InverseId], [t].[c5]
FROM (
    SELECT TOP(@__p_0) [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK].[Id] AS [c0], [l1.OneToOne_Optional_FK].[Date] AS [c1], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name] AS [c2], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId] AS [c3], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId] AS [c4], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId] AS [c5]
    FROM [Level1] AS [l1]
    LEFT JOIN [Level2] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
    WHERE ([l1.OneToOne_Optional_FK].[Name] <> N'Foo') OR [l1.OneToOne_Optional_FK].[Name] IS NULL
) AS [t]",
                Sql);
        }

        public override void Explicit_GroupJoin_in_subquery_with_unrelated_projection()
        {
            base.Explicit_GroupJoin_in_subquery_with_unrelated_projection();

            Assert.Equal(
                @"@__p_0: 15

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_SelfId], [t].[c0], [t].[c1], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[c2], [t].[OneToMany_Optional_InverseId], [t].[c3], [t].[OneToMany_Required_InverseId], [t].[c4], [t].[OneToOne_Optional_PK_InverseId], [t].[c5]
FROM (
    SELECT TOP(@__p_0) [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id] AS [c0], [l2].[Date] AS [c1], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name] AS [c2], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId] AS [c3], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId] AS [c4], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId] AS [c5]
    FROM [Level1] AS [l1]
    LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
    WHERE ([l2].[Name] <> N'Foo') OR [l2].[Name] IS NULL
) AS [t]",
                Sql);
        }

        public override void Explicit_GroupJoin_in_subquery_with_unrelated_projection2()
        {
            base.Explicit_GroupJoin_in_subquery_with_unrelated_projection2();

            Assert.Equal(
                @"SELECT [l10].[Id], [l10].[Date], [l10].[Name], [l10].[OneToMany_Optional_Self_InverseId], [l10].[OneToMany_Required_Self_InverseId], [l10].[OneToOne_Optional_SelfId], [l21].[Id], [l21].[Date], [l21].[Level1_Optional_Id], [l21].[Level1_Required_Id], [l21].[Name], [l21].[OneToMany_Optional_InverseId], [l21].[OneToMany_Optional_Self_InverseId], [l21].[OneToMany_Required_InverseId], [l21].[OneToMany_Required_Self_InverseId], [l21].[OneToOne_Optional_PK_InverseId], [l21].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l10]
LEFT JOIN [Level2] AS [l21] ON [l10].[Id] = [l21].[Level1_Optional_Id]
WHERE ([l21].[Name] <> N'Foo') OR [l21].[Name] IS NULL",
                Sql);
        }

        public override void Explicit_GroupJoin_in_subquery_with_unrelated_projection3()
        {
            base.Explicit_GroupJoin_in_subquery_with_unrelated_projection3();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
WHERE ([l2].[Name] <> N'Foo') OR [l2].[Name] IS NULL",
                Sql);
        }

        public override void Explicit_GroupJoin_in_subquery_with_unrelated_projection4()
        {
            base.Explicit_GroupJoin_in_subquery_with_unrelated_projection4();

            Assert.Equal(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]
LEFT JOIN [Level2] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
WHERE ([l2].[Name] <> N'Foo') OR [l2].[Name] IS NULL",
                Sql);
        }

        public override void Explicit_GroupJoin_in_subquery_with_scalar_result_operator()
        {
            base.Explicit_GroupJoin_in_subquery_with_scalar_result_operator();

            Assert.StartsWith(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]

SELECT COUNT(*)
FROM [Level1] AS [l1_inner]
LEFT JOIN [Level2] AS [l2] ON [l1_inner].[Id] = [l2].[Level1_Optional_Id]

SELECT COUNT(*)
FROM [Level1] AS [l1_inner]
LEFT JOIN [Level2] AS [l2] ON [l1_inner].[Id] = [l2].[Level1_Optional_Id]",
                Sql);
        }

        public override void Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause()
        {
            base.Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause();

            Assert.StartsWith(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1]

SELECT [l1_inner].[Id], [l1_inner].[Date], [l1_inner].[Name], [l1_inner].[OneToMany_Optional_Self_InverseId], [l1_inner].[OneToMany_Required_Self_InverseId], [l1_inner].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1_inner]
LEFT JOIN [Level2] AS [l2] ON [l1_inner].[Id] = [l2].[Level1_Optional_Id]

SELECT [l1_inner].[Id], [l1_inner].[Date], [l1_inner].[Name], [l1_inner].[OneToMany_Optional_Self_InverseId], [l1_inner].[OneToMany_Required_Self_InverseId], [l1_inner].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [Level1] AS [l1_inner]
LEFT JOIN [Level2] AS [l2] ON [l1_inner].[Id] = [l2].[Level1_Optional_Id]",
                Sql);
        }

        public override void Where_on_multilevel_reference_in_subquery_with_outer_projection()
        {
            base.Where_on_multilevel_reference_in_subquery_with_outer_projection();

            Assert.Equal(
                @"@__p_0: 0
@__p_1: 10

SELECT [t].[Name]
FROM (
    SELECT [l3].*
    FROM [Level3] AS [l3]
    INNER JOIN [Level2] AS [l3.OneToMany_Required_Inverse] ON [l3].[OneToMany_Required_InverseId] = [l3.OneToMany_Required_Inverse].[Id]
    INNER JOIN [Level1] AS [l3.OneToMany_Required_Inverse.OneToOne_Required_FK_Inverse] ON [l3.OneToMany_Required_Inverse].[Level1_Required_Id] = [l3.OneToMany_Required_Inverse.OneToOne_Required_FK_Inverse].[Id]
    WHERE [l3.OneToMany_Required_Inverse.OneToOne_Required_FK_Inverse].[Name] = N'L1 03'
    ORDER BY [l3].[Level2_Required_Id]
    OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
) AS [t]
ORDER BY [t].[Level2_Required_Id]",
                Sql);
        }

        private const string FileLineEnding = @"
";

        private static string Sql => TestSqlLoggerFactory.Sql.Replace(Environment.NewLine, FileLineEnding);
    }
}
