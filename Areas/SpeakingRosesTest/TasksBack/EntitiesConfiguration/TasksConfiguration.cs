using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.Entities;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.EntitiesConfiguration
{
    public class TasksConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> entity)
        {
            try
            {
                //TasksId
                entity.HasKey(e => e.TasksId);
                entity.Property(e => e.TasksId)
                    .ValueGeneratedOnAdd();

                //Active
                entity.Property(e => e.Active)
                    .HasColumnType("tinyint")
                    .IsRequired(true);

                //DateTimeCreation
                entity.Property(e => e.DateTimeCreation)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //DateTimeLastModification
                entity.Property(e => e.DateTimeLastModification)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //UserCreationId
                entity.Property(e => e.UserCreationId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //UserLastModificationId
                entity.Property(e => e.UserLastModificationId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //Title
                entity.Property(e => e.Title)
                    .HasColumnType("varchar(100)")
                    .IsRequired(true);

                //Description
                entity.Property(e => e.Description)
                    .HasColumnType("varchar(MAX)")
                    .IsRequired(true);

                //PriorityId
                entity.Property(e => e.PriorityId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //DueDate
                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //StatusId
                entity.Property(e => e.StatusId)
                    .HasColumnType("int")
                    .IsRequired(true);

                
            }
            catch (Exception) { throw; }
        }
    }
}
