// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.DependencyInjection;
using Moq;
using Xunit;

namespace Microsoft.Data.Entity.Tests.ChangeTracking
{
    public class StateManagerTest
    {
        [Fact]
        public void Can_get_existing_entry_if_entity_is_already_tracked_otherwise_new_entry()
        {
            var stateManager = CreateStateManager(BuildModel());
            var category = new Category { Id = 1 };

            var stateEntry = stateManager.GetOrCreateEntry(category);

            stateManager.StartTracking(stateEntry);

            var stateEntry2 = stateManager.GetOrCreateEntry(category);

            Assert.Same(stateEntry, stateEntry2);
            Assert.Equal(EntityState.Unknown, stateEntry.EntityState);
        }

        [Fact]
        public void StartTracking_throws_if_different_instance_with_same_identity_is_already_tracked()
        {
            var model = BuildModel();
            var categoryType = model.GetEntityType(typeof(Category));
            var stateManager = CreateStateManager(model);

            var valueReader = new ObjectArrayValueReader(new object[] { 77, "Bjork", null });

            stateManager.StartTracking(categoryType, new Category { Id = 77 }, valueReader);

            Assert.Equal(
                Strings.IdentityConflict("Category"),
                Assert.Throws<InvalidOperationException>(
                    () => stateManager.StartTracking(categoryType, new Category { Id = 77 }, valueReader)).Message);
        }

        [Fact]
        public void StartTracking_is_no_op_if_entity_is_already_tracked()
        {
            var model = BuildModel();
            var categoryType = model.GetEntityType(typeof(Category));
            var stateManager = CreateStateManager(model);

            var category = new Category { Id = 77 };
            var valueReader = new ObjectArrayValueReader(new object[] { 77, "Bjork", null });

            var entry = stateManager.StartTracking(categoryType, category, valueReader);

            Assert.Same(entry, stateManager.StartTracking(categoryType, category, valueReader));
        }

        [Fact]
        public void StartTracking_throws_for_null_entity_key_in_value_reader()
        {
            var model = BuildModel();
            var categoryType = model.GetEntityType(typeof(Category));
            var stateManager = CreateStateManager(model);

            var valueReader = new ObjectArrayValueReader(new object[] { null, "Bjork", null });

            Assert.Equal(
                Strings.InvalidPrimaryKey("Category"),
                Assert.Throws<InvalidOperationException>(
                    () => stateManager.StartTracking(categoryType, new Category { Id = 77 }, valueReader)).Message);
        }

        [Fact]
        public void StartTracking_throws_for_sentinel_entity_key_in_value_reader()
        {
            var model = BuildModel();
            var categoryType = model.GetEntityType(typeof(Category));
            var stateManager = CreateStateManager(model);

            var valueReader = new ObjectArrayValueReader(new object[] { 0, "Bjork", null });

            Assert.Equal(
                Strings.InvalidPrimaryKey("Category"),
                Assert.Throws<InvalidOperationException>(
                    () => stateManager.StartTracking(categoryType, new Category { Id = 77 }, valueReader)).Message);
        }


        [Fact]
        public void StartTracking_throws_for_non_default_sentinel_entity_key_in_value_reader()
        {
            var model = BuildModel();
            var categoryType = model.GetEntityType(typeof(Category));
            categoryType.GetProperty("Id").SentinelValue = 7;
            var stateManager = CreateStateManager(model);

            var valueReader = new ObjectArrayValueReader(new object[] { 7, "Bjork", null });

            Assert.Equal(
                Strings.InvalidPrimaryKey("Category"),
                Assert.Throws<InvalidOperationException>(
                    () => stateManager.StartTracking(categoryType, new Category { Id = 77 }, valueReader)).Message);
        }

        [Fact]
        public void Can_explicitly_create_new_entry_for_shadow_state_entity()
        {
            var model = BuildModel();
            var stateManager = CreateStateManager(model);
            var entityType = model.GetEntityType("Location");
            var stateEntry = stateManager.CreateNewEntry(entityType);
            stateEntry[entityType.GetPrimaryKey().Properties.Single()] = 42;

            Assert.Equal(EntityState.Unknown, stateEntry.EntityState);
            Assert.Null(stateEntry.Entity);
            Assert.Equal(0, stateManager.StateEntries.Count());

            stateManager.StartTracking(stateEntry);

            Assert.Equal(1, stateManager.StateEntries.Count());
        }

        [Fact]
        public void Can_explicitly_create_new_entry_for_normal_state_entity()
        {
            var model = BuildModel();
            var stateManager = CreateStateManager(model);

            var stateEntry = stateManager.CreateNewEntry(model.GetEntityType(typeof(Category)));

            Assert.Equal(EntityState.Unknown, stateEntry.EntityState);
            Assert.IsType<Category>(stateEntry.Entity);
            Assert.Equal(0, stateManager.StateEntries.Count());

            ((Category)stateEntry.Entity).Id = 1;
            stateManager.StartTracking(stateEntry);

            Assert.Equal(1, stateManager.StateEntries.Count());
        }

        [Fact]
        public void Can_get_existing_entry_even_if_state_not_yet_set()
        {
            var stateManager = CreateStateManager(BuildModel());
            var category = new Category { Id = 1 };

            var stateEntry = stateManager.GetOrCreateEntry(category);
            var stateEntry2 = stateManager.GetOrCreateEntry(category);

            Assert.Same(stateEntry, stateEntry2);
            Assert.Equal(EntityState.Unknown, stateEntry.EntityState);
        }

        [Fact]
        public void Can_stop_tracking_and_then_start_tracking_again()
        {
            var stateManager = CreateStateManager(BuildModel());
            var category = new Category { Id = 1 };

            var stateEntry = stateManager.GetOrCreateEntry(category);
            stateManager.StartTracking(stateEntry);
            stateManager.StopTracking(stateEntry);
            stateManager.StartTracking(stateEntry);

            var stateEntry2 = stateManager.GetOrCreateEntry(category);
            Assert.Same(stateEntry, stateEntry2);
        }

        [Fact]
        public void Can_stop_tracking_and_then_start_tracking_using_a_new_state_entry()
        {
            var stateManager = CreateStateManager(BuildModel());
            var category = new Category { Id = 1 };

            var stateEntry = stateManager.GetOrCreateEntry(category);
            stateManager.StartTracking(stateEntry);
            stateManager.StopTracking(stateEntry);

            var stateEntry2 = stateManager.GetOrCreateEntry(category);
            Assert.NotSame(stateEntry, stateEntry2);

            stateManager.StartTracking(stateEntry2);
        }

        [Fact]
        public void Throws_on_attempt_to_start_tracking_same_entity_with_two_entries()
        {
            var stateManager = CreateStateManager(BuildModel());
            var category = new Category { Id = 1 };

            var stateEntry = stateManager.GetOrCreateEntry(category);
            stateManager.StartTracking(stateEntry);
            stateManager.StopTracking(stateEntry);

            var stateEntry2 = stateManager.GetOrCreateEntry(category);
            stateManager.StartTracking(stateEntry2);

            Assert.Equal(
                Strings.MultipleStateEntries(typeof(Category).FullName),
                Assert.Throws<InvalidOperationException>(() => stateManager.StartTracking(stateEntry)).Message);
        }

        [Fact]
        public void Throws_on_attempt_to_start_tracking_different_entities_with_same_identity()
        {
            var stateManager = CreateStateManager(BuildModel());
            var category1 = new Category { Id = 7 };
            var category2 = new Category { Id = 7 };

            var stateEntry1 = stateManager.GetOrCreateEntry(category1);
            var stateEntry2 = stateManager.GetOrCreateEntry(category2);

            stateManager.StartTracking(stateEntry1);

            Assert.Equal(
                Strings.IdentityConflict(typeof(Category).FullName),
                Assert.Throws<InvalidOperationException>(() => stateManager.StartTracking(stateEntry2)).Message);
        }

        [Fact]
        public void Throws_on_attempt_to_start_tracking_entity_with_null_key()
        {
            var stateManager = CreateStateManager(BuildModel());
            var entity = new Dogegory();

            var stateEntry = stateManager.GetOrCreateEntry(entity);

            Assert.Equal(
                Strings.InvalidPrimaryKey(typeof(Dogegory).FullName),
                Assert.Throws<InvalidOperationException>(() => stateManager.StartTracking(stateEntry)).Message);
        }

        [Fact]
        public void Throws_on_attempt_to_start_tracking_entity_with_sentinel_key()
        {
            var stateManager = CreateStateManager(BuildModel());
            var entity = new Category();

            var stateEntry = stateManager.GetOrCreateEntry(entity);

            Assert.Equal(
                Strings.InvalidPrimaryKey(typeof(Category).FullName),
                Assert.Throws<InvalidOperationException>(() => stateManager.StartTracking(stateEntry)).Message);
        }

        [Fact]
        public void Throws_on_attempt_to_start_tracking_entity_with_non_default_sentinel_key()
        {
            var model = BuildModel();
            model.GetEntityType(typeof(Category)).GetProperty("Id").SentinelValue = 7;

            var stateManager = CreateStateManager(model);
            var entity = new Category { Id = 7 };

            var stateEntry = stateManager.GetOrCreateEntry(entity);

            Assert.Equal(
                Strings.InvalidPrimaryKey(typeof(Category).FullName),
                Assert.Throws<InvalidOperationException>(() => stateManager.StartTracking(stateEntry)).Message);
        }

        [Fact]
        public void Throws_on_attempt_to_start_tracking_with_wrong_manager()
        {
            var model = BuildModel();
            var stateManager = CreateStateManager(model);
            var stateManager2 = CreateStateManager(model);

            var stateEntry = stateManager.GetOrCreateEntry(new Category());

            Assert.Equal(
                Strings.WrongStateManager(typeof(Category).FullName),
                Assert.Throws<InvalidOperationException>(() => stateManager2.StartTracking(stateEntry)).Message);
        }

        [Fact]
        public void Will_get_new_entry_if_another_entity_with_the_same_key_is_already_tracked()
        {
            var stateManager = CreateStateManager(BuildModel());

            Assert.NotSame(
                stateManager.GetOrCreateEntry(new Category { Id = 77 }),
                stateManager.GetOrCreateEntry(new Category { Id = 77 }));
        }

        [Fact]
        public void Can_get_all_entities()
        {
            var stateManager = CreateStateManager(BuildModel());

            var productId1 = new Guid("984ade3c-2f7b-4651-a351-642e92ab7146");
            var productId2 = new Guid("0edc9136-7eed-463b-9b97-bdb9648ab877");

            stateManager.StartTracking(stateManager.GetOrCreateEntry(new Category { Id = 77 }));
            stateManager.StartTracking(stateManager.GetOrCreateEntry(new Category { Id = 78 }));
            stateManager.StartTracking(stateManager.GetOrCreateEntry(new Product { Id = productId1 }));
            stateManager.StartTracking(stateManager.GetOrCreateEntry(new Product { Id = productId2 }));

            Assert.Equal(4, stateManager.StateEntries.Count());

            Assert.Equal(
                new[] { 77, 78 },
                stateManager.StateEntries
                    .Select(e => e.Entity)
                    .OfType<Category>()
                    .Select(e => e.Id)
                    .OrderBy(k => k)
                    .ToArray());

            Assert.Equal(
                new[] { productId2, productId1 },
                stateManager.StateEntries
                    .Select(e => e.Entity)
                    .OfType<Product>()
                    .Select(e => e.Id)
                    .OrderBy(k => k)
                    .ToArray());
        }

        [Fact]
        public void Listeners_are_notified_when_entity_states_change()
        {
            var listeners = new[]
                {
                    new Mock<IEntityStateListener>(),
                    new Mock<IEntityStateListener>(),
                    new Mock<IEntityStateListener>()
                };

            var services = new ServiceCollection()
                .AddInstance(listeners[0].Object)
                .AddInstance(listeners[1].Object)
                .AddInstance(listeners[2].Object);

            var contextServices = TestHelpers.Instance.CreateContextServices(services, BuildModel());

            var stateManager = contextServices.GetRequiredService<StateManager>();

            var entry = stateManager.GetOrCreateEntry(new Category { Id = 77 });
            entry.SetEntityState(EntityState.Added);

            foreach (var listener in listeners)
            {
                listener.Verify(m => m.StateChanging(entry, It.IsAny<EntityState>()), Times.Once);
                listener.Verify(m => m.StateChanged(entry, It.IsAny<EntityState>()), Times.Once);

                listener.Verify(m => m.StateChanging(entry, EntityState.Added), Times.Once);
                listener.Verify(m => m.StateChanged(entry, EntityState.Unknown), Times.Once);
            }

            entry.SetEntityState(EntityState.Modified);

            foreach (var listener in listeners)
            {
                listener.Verify(m => m.StateChanging(entry, It.IsAny<EntityState>()), Times.Exactly(2));
                listener.Verify(m => m.StateChanged(entry, It.IsAny<EntityState>()), Times.Exactly(2));

                listener.Verify(m => m.StateChanging(entry, EntityState.Modified), Times.Once);
                listener.Verify(m => m.StateChanged(entry, EntityState.Unknown), Times.Once);
            }
        }

        [Fact]
        public void DetectChanges_is_called_for_all_tracked_entities_and_returns_true_if_any_changes_detected()
        {
            var contextServices = TestHelpers.Instance.CreateContextServices(
                new ServiceCollection().AddScoped<ChangeDetector, ChangeDetectorProxy>(),
                BuildModel());

            var stateManager = contextServices.GetRequiredService<StateManager>();

            var entry1 = stateManager.GetOrCreateEntry(new Category { Id = 77, Name = "Beverages" });
            var entry2 = stateManager.GetOrCreateEntry(new Category { Id = 78, Name = "Foods" });
            var entry3 = stateManager.GetOrCreateEntry(new Category { Id = 79, Name = "Stuff" });

            stateManager.StartTracking(entry1);
            stateManager.StartTracking(entry2);
            stateManager.StartTracking(entry3);

            var changeDetector = (ChangeDetectorProxy)contextServices.GetRequiredService<ChangeDetector>();

            changeDetector.DetectChanges(stateManager);

            Assert.Equal(new[] { 77, 78, 79 }, changeDetector.Entries.Select(e => ((Category)e.Entity).Id).ToArray());

            ((Category)entry2.Entity).Name = "Snacks";

            changeDetector.DetectChanges(stateManager);

            Assert.Equal(new[] { 77, 78, 79, 77, 78, 79 }, changeDetector.Entries.Select(e => ((Category)e.Entity).Id).ToArray());
        }

        internal class ChangeDetectorProxy : ChangeDetector
        {
            public ChangeDetectorProxy([NotNull] DbContextService<IModel> model)
                : base(model)
            {
            }

            public List<StateEntry> Entries { get; } = new List<StateEntry>();

            public override void DetectChanges(StateEntry entry)
            {
                Entries.Add(entry);

                base.DetectChanges(entry);
            }
        }

        [Fact]
        public void SaveChanges_processes_all_tracked_entities()
        {
            var stateManager = CreateStateManager(BuildModel());

            var productId1 = new Guid("984ade3c-2f7b-4651-a351-642e92ab7146");
            var productId2 = new Guid("0edc9136-7eed-463b-9b97-bdb9648ab877");

            var entry1 = stateManager.GetOrCreateEntry(new Category { Id = 77 });
            var entry2 = stateManager.GetOrCreateEntry(new Category { Id = 78 });
            var entry3 = stateManager.GetOrCreateEntry(new Product { Id = productId1 });
            var entry4 = stateManager.GetOrCreateEntry(new Product { Id = productId2 });

            entry1.SetEntityState(EntityState.Added);
            entry2.SetEntityState(EntityState.Modified);
            entry3.SetEntityState(EntityState.Unchanged);
            entry4.SetEntityState(EntityState.Deleted);

            stateManager.SaveChangesAsync().Wait();

            Assert.Equal(3, stateManager.StateEntries.Count());
            Assert.Contains(entry1, stateManager.StateEntries);
            Assert.Contains(entry2, stateManager.StateEntries);
            Assert.Contains(entry3, stateManager.StateEntries);

            Assert.Equal(EntityState.Unchanged, entry1.EntityState);
            Assert.Equal(EntityState.Unchanged, entry2.EntityState);
            Assert.Equal(EntityState.Unchanged, entry3.EntityState);
        }

        [Fact]
        public void Can_get_all_dependent_entries()
        {
            var model = BuildModel();
            var stateManager = CreateStateManager(model);

            var categoryEntry1 = stateManager.StartTracking(stateManager.GetOrCreateEntry(new Category { Id = 1, PrincipalId = 77 }));
            var categoryEntry2 = stateManager.StartTracking(stateManager.GetOrCreateEntry(new Category { Id = 2, PrincipalId = 78 }));
            var categoryEntry3 = stateManager.StartTracking(stateManager.GetOrCreateEntry(new Category { Id = 3, PrincipalId = 79 }));
            var categoryEntry4 = stateManager.StartTracking(stateManager.GetOrCreateEntry(new Category { Id = 4, PrincipalId = null }));
            var productEntry1 = stateManager.StartTracking(stateManager.GetOrCreateEntry(new Product { Id = Guid.NewGuid(), DependentId = 77 }));
            var productEntry2 = stateManager.StartTracking(stateManager.GetOrCreateEntry(new Product { Id = Guid.NewGuid(), DependentId = 77 }));
            var productEntry3 = stateManager.StartTracking(stateManager.GetOrCreateEntry(new Product { Id = Guid.NewGuid(), DependentId = 78 }));
            var productEntry4 = stateManager.StartTracking(stateManager.GetOrCreateEntry(new Product { Id = Guid.NewGuid(), DependentId = 78 }));
            var productEntry5 = stateManager.StartTracking(stateManager.GetOrCreateEntry(new Product { Id = Guid.NewGuid(), DependentId = null }));

            var fk = model.GetEntityType(typeof(Product)).ForeignKeys.Single();

            Assert.Equal(
                new[] { productEntry1, productEntry2 },
                stateManager.GetDependents(categoryEntry1, fk).ToArray());

            Assert.Equal(
                new[] { productEntry3, productEntry4 },
                stateManager.GetDependents(categoryEntry2, fk).ToArray());

            Assert.Empty(stateManager.GetDependents(categoryEntry3, fk).ToArray());
            Assert.Empty(stateManager.GetDependents(categoryEntry4, fk).ToArray());
        }

        private static StateManager CreateStateManager(IModel model)
        {
            return TestHelpers.Instance.CreateContextServices(model).GetRequiredService<StateManager>();
        }

        private class Category
        {
            public int Id { get; set; }
            public int? PrincipalId { get; set; }
            public string Name { get; set; }
        }

        private class Product
        {
            public Guid Id { get; set; }
            public int? DependentId { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        private class Dogegory
        {
            public string Id { get; set; }
        }

        private static Model BuildModel()
        {
            var model = new Model();
            var builder = TestHelpers.Instance.CreateConventionBuilder(model);

            builder.Entity<Product>();
            builder.Entity<Category>();
            builder.Entity<Dogegory>();

            var productType = model.GetEntityType(typeof(Product));
            var categoryType = model.GetEntityType(typeof(Category));

            productType.GetOrAddForeignKey(productType.GetProperty("DependentId"), new Key(new[] { categoryType.GetProperty("PrincipalId") }));

            var locationType = model.AddEntityType("Location");
            var idProperty = locationType.GetOrAddProperty("Id", typeof(int), shadowProperty: true);
            locationType.GetOrAddProperty("Planet", typeof(string), shadowProperty: true);
            locationType.GetOrSetPrimaryKey(idProperty);

            return model;
        }
    }
}
