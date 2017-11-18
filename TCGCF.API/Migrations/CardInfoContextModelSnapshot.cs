﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using TCGCF.API.Entities;

namespace TCGCF.API.Migrations
{
    [DbContext(typeof(CardInfoContext))]
    partial class CardInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TCGCF.API.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("CMC");

                    b.Property<string>("FlavorText")
                        .HasMaxLength(1000);

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<int>("LinkedCard");

                    b.Property<int>("Loyalty");

                    b.Property<string>("ManaCost")
                        .HasMaxLength(50);

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Power");

                    b.Property<int>("PrintNumber");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("RulesText")
                        .HasMaxLength(1000);

                    b.Property<int>("SetId");

                    b.Property<int>("Toughness");

                    b.HasKey("Id");

                    b.HasIndex("SetId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardLayout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.ToTable("CardLayout");
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardsInDeck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<int>("DeckId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("DeckId");

                    b.ToTable("CardsInDeck");
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardSubType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("CardSubType");
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardSuperType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("CardSuperType");
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("CardType");
                });

            modelBuilder.Entity("TCGCF.API.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("TCGCF.API.Entities.ColorIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("ColorIdentity");
                });

            modelBuilder.Entity("TCGCF.API.Entities.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("TCGCF.API.Entities.Format", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("CopyLimit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<int>("GameId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("NumberOfCards");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Format");
                });

            modelBuilder.Entity("TCGCF.API.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<bool>("AvailableOnConsole");

                    b.Property<bool>("AvailableOnMobile");

                    b.Property<bool>("AvailableOnPC");

                    b.Property<bool>("AvailableOnPaper");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Published");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Website")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("TCGCF.API.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.ToTable("Language");
                });

            modelBuilder.Entity("TCGCF.API.Entities.Legality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Arena");

                    b.Property<int>("CardId");

                    b.Property<bool>("Commander");

                    b.Property<bool>("Frontier");

                    b.Property<bool>("Legacy");

                    b.Property<bool>("Modern");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<bool>("Pauper");

                    b.Property<bool>("Standard");

                    b.Property<bool>("Vintage");

                    b.HasKey("Id");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.ToTable("Legalities");
                });

            modelBuilder.Entity("TCGCF.API.Entities.Rarity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.ToTable("Rarity");
                });

            modelBuilder.Entity("TCGCF.API.Entities.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("GameId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("NumberOfCards");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Story")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Abbreviation")
                        .IsUnique();

                    b.HasIndex("GameId");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("TCGCF.API.Entities.SetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("SetId");

                    b.HasKey("Id");

                    b.HasIndex("SetId")
                        .IsUnique();

                    b.ToTable("SetType");
                });

            modelBuilder.Entity("TCGCF.API.Entities.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.Card", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Set", "Set")
                        .WithMany("Cards")
                        .HasForeignKey("SetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardLayout", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithOne("CardLayout")
                        .HasForeignKey("TCGCF.API.Entities.CardLayout", "CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardsInDeck", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithMany("CardsInDeck")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TCGCF.API.Entities.Deck", "Deck")
                        .WithMany("Cards")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardSubType", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithMany("CardSubType")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardSuperType", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithMany("CardSuperType")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.CardType", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithMany("CardType")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.Color", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithMany("Color")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.ColorIdentity", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithMany("ColorIdentity")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.Format", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Game", "Game")
                        .WithMany("Formats")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.Language", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithOne("Language")
                        .HasForeignKey("TCGCF.API.Entities.Language", "CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.Legality", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithOne("Legality")
                        .HasForeignKey("TCGCF.API.Entities.Legality", "CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.Rarity", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Card", "Card")
                        .WithOne("Rarity")
                        .HasForeignKey("TCGCF.API.Entities.Rarity", "CardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.Set", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Game", "Game")
                        .WithMany("Sets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TCGCF.API.Entities.SetType", b =>
                {
                    b.HasOne("TCGCF.API.Entities.Set", "Set")
                        .WithOne("SetType")
                        .HasForeignKey("TCGCF.API.Entities.SetType", "SetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
