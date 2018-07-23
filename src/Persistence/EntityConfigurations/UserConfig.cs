﻿using System.Data.Entity.ModelConfiguration;

namespace Phony.Persistence.EntityConfigurations
{
    public class UserConfig : EntityTypeConfiguration<Model.User>
    {
        public UserConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(u => u.Pass)
                .IsRequired()
                .HasMaxLength(100);

            HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}